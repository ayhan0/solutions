using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace PrimeWinForms
{
    public class Form1 : Form
    {
        private TextBox txt1, txt2;
        private Button btn1, btn2;
        private ListBox lb1, lb2;

        public Form1()
        {
            Text = "Prime Calculator (Thread)";
            Width = 650;
            Height = 320;
            StartPosition = FormStartPosition.CenterScreen;

            txt1 = new TextBox { Left = 20, Top = 20, Width = 140 };
            btn1 = new Button { Left = 170, Top = 18, Width = 110, Text = "Calculate 1" };
            lb1  = new ListBox { Left = 20, Top = 60, Width = 260, Height = 200 };

            txt2 = new TextBox { Left = 340, Top = 20, Width = 140 };
            btn2 = new Button { Left = 490, Top = 18, Width = 110, Text = "Calculate 2" };
            lb2  = new ListBox { Left = 340, Top = 60, Width = 260, Height = 200 };

            Controls.AddRange(new Control[] { txt1, btn1, lb1, txt2, btn2, lb2 });

            btn1.Click += (s, e) => StartCalc(txt1.Text, lb1, btn1);
            btn2.Click += (s, e) => StartCalc(txt2.Text, lb2, btn2);
        }

        private void StartCalc(string input, ListBox targetList, Button relatedButton)
        {
            if (!int.TryParse(input, out int n) || n < 2)
            {
                MessageBox.Show("Please enter a valid integer >= 2.");
                return;
            }

            relatedButton.Enabled = false;
            targetList.Items.Clear();

            var worker = new Thread(() =>
            {
                List<int> primes = GetPrimesUpTo(n);

                // UI update must be marshaled to UI thread
                if (!IsDisposed && targetList.IsHandleCreated)
                {
                    BeginInvoke(new Action(() =>
                    {
                        foreach (var p in primes)
                            targetList.Items.Add(p);

                        relatedButton.Enabled = true;
                    }));
                }
            });

            worker.IsBackground = true;
            worker.Start();
        }

        private static List<int> GetPrimesUpTo(int n)
        {
            var primes = new List<int>();
            for (int i = 2; i <= n; i++)
            {
                if (IsPrime(i)) primes.Add(i);
            }
            return primes;
        }

        private static bool IsPrime(int x)
        {
            if (x < 2) return false;
            if (x == 2) return true;
            if (x % 2 == 0) return false;

            int r = (int)Math.Sqrt(x);
            for (int i = 3; i <= r; i += 2)
                if (x % i == 0) return false;

            return true;
        }
    }
}

using System;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("NativeMathDll.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int Multiply(int a, int b);

    static void Main()
    {
        Console.Write("Enter first number: ");
        int a = int.Parse(Console.ReadLine()!);

        Console.Write("Enter second number: ");
        int b = int.Parse(Console.ReadLine()!);

        Console.WriteLine($"Result from C++ DLL: {Multiply(a, b)}");
        Console.ReadKey();
    }
}

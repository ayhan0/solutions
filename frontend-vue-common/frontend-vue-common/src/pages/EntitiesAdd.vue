<template>
  <div class="card">
    <h2>Add Record</h2>

    <form class="form" @submit.prevent="submit">
      <div class="grid">
        <div class="field">
          <label>Name *</label>
          <input v-model.trim="form.name" />
          <small v-if="errors.name">{{ errors.name }}</small>
        </div>

        <div class="field">
          <label>Surname *</label>
          <input v-model.trim="form.surname" />
          <small v-if="errors.surname">{{ errors.surname }}</small>
        </div>

        <div class="field">
          <label>Age *</label>
          <input v-model.trim="form.age" />
          <small v-if="errors.age">{{ errors.age }}</small>
        </div>

        <div class="field">
          <label>Email *</label>
          <input v-model.trim="form.email" />
          <small v-if="errors.email">{{ errors.email }}</small>
        </div>

        <div class="field">
          <label>Phone *</label>
          <input v-model.trim="form.phone" />
          <small v-if="errors.phone">{{ errors.phone }}</small>
        </div>
      </div>

      <div class="actions">
        <button class="btn primary" type="submit" :disabled="loading">
          {{ loading ? "Saving..." : "Add" }}
        </button>
        <button class="btn" type="button" @click="goList">Go to List</button>
      </div>
    </form>
  </div>
</template>

<script setup>
import { reactive, ref } from "vue";
import { useRouter } from "vue-router";
import { addEntity } from "@/services/entityService";
import { showToast } from "@/stores/toast";

const router = useRouter();
const loading = ref(false);

const form = reactive({
  name: "",
  surname: "",
  age: "",
  email: "",
  phone: "",
});

const errors = reactive({
  name: "",
  surname: "",
  age: "",
  email: "",
  phone: "",
});

function clearErrors() {
  errors.name = errors.surname = errors.age = errors.email = errors.phone = "";
}

function validate() {
  clearErrors();

  if (!form.name) errors.name = "Required";
  if (!form.surname) errors.surname = "Required";
  if (!form.age) errors.age = "Required";
  if (!form.email) errors.email = "Required";
  if (!form.phone) errors.phone = "Required";

  // age numeric
  if (form.age && !/^\d+$/.test(form.age)) errors.age = "Age must be numeric";

  // simple email regex
  if (form.email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email)) {
    errors.email = "Invalid email";
  }

  return !errors.name && !errors.surname && !errors.age && !errors.email && !errors.phone;
}

async function submit() {
  if (!validate()) return;

  loading.value = true;
  try {
    await addEntity({
      name: form.name,
      surname: form.surname,
      age: Number(form.age),
      email: form.email,
      phone: form.phone,
    });

    showToast("Record added", "success");

    // Add sonrası list refresh: route ile list'e git
    router.push("/entities/list");
  } finally {
    loading.value = false;
  }
}

function goList() {
  router.push("/entities/list");
}
</script>

<style scoped>
.card { display: grid; gap: 12px; max-width: 880px; }
.form { background: #fff; border: 1px solid #e2e8f0; border-radius: 12px; padding: 14px; }
.grid { display: grid; grid-template-columns: repeat(2, 1fr); gap: 12px; }
.field { display: grid; gap: 6px; }
label { font-size: 13px; color: #334155; }
input {
  padding: 10px 10px; border-radius: 10px; border: 1px solid #cbd5e1; outline: none;
}
small { color: #dc2626; }
.actions { display: flex; gap: 10px; margin-top: 12px; }
.btn {
  padding: 10px 12px; border-radius: 10px; border: 1px solid #cbd5e1;
  background: #fff; cursor: pointer;
}
.primary { background: #0f172a; color: #fff; border-color: #0f172a; }
.btn:disabled { opacity: .6; cursor: not-allowed; }
@media (max-width: 820px) { .grid { grid-template-columns: 1fr; } }
</style>

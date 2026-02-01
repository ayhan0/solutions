<template>
  <div class="card">
    <h2>Delete Record</h2>

    <div class="box">
      <div class="field">
        <label>Key (Name or Phone) *</label>
        <input v-model.trim="key" placeholder="e.g. John or +905..." />
        <small v-if="error">{{ error }}</small>
      </div>

      <div class="actions">
        <button class="btn danger" @click="del" :disabled="loading">
          {{ loading ? "Deleting..." : "Delete" }}
        </button>
        <button class="btn" @click="goList">Go to List</button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { deleteEntity } from "@/services/entityService";
import { showToast } from "@/stores/toast";

const router = useRouter();

const key = ref("");
const error = ref("");
const loading = ref(false);

async function del() {
  error.value = "";
  if (!key.value) {
    error.value = "Required";
    return;
  }

  loading.value = true;
  try {
    await deleteEntity(key.value);
    showToast("Record deleted", "success");
    router.push("/entities/list"); // refresh list by navigating
  } finally {
    loading.value = false;
  }
}

function goList() {
  router.push("/entities/list");
}
</script>

<style scoped>
.card { display: grid; gap: 12px; max-width: 700px; }
.box { background: #fff; border: 1px solid #e2e8f0; border-radius: 12px; padding: 14px; display: grid; gap: 12px; }
.field { display: grid; gap: 6px; }
label { font-size: 13px; color: #334155; }
input { padding: 10px 10px; border-radius: 10px; border: 1px solid #cbd5e1; }
small { color: #dc2626; }
.actions { display: flex; gap: 10px; }
.btn {
  padding: 10px 12px; border-radius: 10px; border: 1px solid #cbd5e1;
  background: #fff; cursor: pointer;
}
.danger { background: #dc2626; border-color: #dc2626; color: #fff; }
.btn:disabled { opacity: .6; cursor: not-allowed; }
</style>

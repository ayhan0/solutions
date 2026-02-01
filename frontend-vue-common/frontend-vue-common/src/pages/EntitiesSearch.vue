<template>
  <div class="card">
    <div class="row">
      <h2>Search Records</h2>
    </div>

    <div class="box">
      <div class="field">
        <label>Key (Name or Phone) *</label>
        <input v-model.trim="key" placeholder="e.g. John or +905..." />
        <small v-if="error">{{ error }}</small>
      </div>

      <div class="actions">
        <button class="btn primary" @click="search" :disabled="loading">
          {{ loading ? "Searching..." : "Search" }}
        </button>
        <button class="btn" @click="clear">Clear</button>
      </div>

      <EntityTable :rows="rows" />
    </div>
  </div>
</template>

<script setup>
import { ref } from "vue";
import EntityTable from "@/components/EntityTable.vue";
import { searchEntities } from "@/services/entityService";

const key = ref("");
const error = ref("");
const loading = ref(false);
const rows = ref([]);

async function search() {
  error.value = "";
  if (!key.value) {
    error.value = "Required";
    return;
  }

  loading.value = true;
  try {
    const data = await searchEntities(key.value);
    rows.value = Array.isArray(data) ? data : [];
  } finally {
    loading.value = false;
  }
}

function clear() {
  key.value = "";
  error.value = "";
  rows.value = [];
}
</script>

<style scoped>
.card { display: grid; gap: 12px; }
.row { display: flex; align-items: center; justify-content: space-between; }
.box { background: #fff; border: 1px solid #e2e8f0; border-radius: 12px; padding: 14px; display: grid; gap: 12px; }
.field { display: grid; gap: 6px; max-width: 520px; }
label { font-size: 13px; color: #334155; }
input { padding: 10px 10px; border-radius: 10px; border: 1px solid #cbd5e1; }
small { color: #dc2626; }
.actions { display: flex; gap: 10px; }
.btn { padding: 10px 12px; border-radius: 10px; border: 1px solid #cbd5e1; background: #fff; cursor: pointer; }
.primary { background: #0f172a; color: #fff; border-color: #0f172a; }
.btn:disabled { opacity: .6; cursor: not-allowed; }
</style>

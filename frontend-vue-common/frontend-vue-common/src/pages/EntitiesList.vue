<template>
  <div class="card">
    <div class="row">
      <h2>List Records</h2>
      <button class="btn" @click="load" :disabled="loading">
        {{ loading ? "Loading..." : "Refresh" }}
      </button>
    </div>

    <EntityTable :rows="rows" />
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import EntityTable from "@/components/EntityTable.vue";
import { listEntities } from "@/services/entityService";

const rows = ref([]);
const loading = ref(false);

async function load() {
  loading.value = true;
  try {
    const data = await listEntities();
    rows.value = Array.isArray(data) ? data : [];
  } finally {
    loading.value = false;
  }
}

onMounted(load);
</script>

<style scoped>
.card { display: grid; gap: 12px; }
.row { display: flex; align-items: center; justify-content: space-between; }
.btn {
  padding: 8px 12px; border-radius: 10px; border: 1px solid #cbd5e1;
  background: #fff; cursor: pointer;
}
.btn:disabled { opacity: .6; cursor: not-allowed; }
</style>

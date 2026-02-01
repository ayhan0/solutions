<template>
  <div class="app">
    <aside class="sidebar">
      <Sidebar />
    </aside>

    <main class="content">
      <header class="topbar">
        <div class="title">Interview Case - Phonebook</div>

        <div class="backend-switch">
          <label>Backend:</label>
          <select v-model="backend" @change="onBackendChange">
            <option value="dotnet">ASP.NET</option>
            <option value="spring">Spring</option>
          </select>
          <span class="hint">{{ baseUrl }}</span>
        </div>
      </header>

      <section class="page">
        <router-view />
      </section>
    </main>

    <Toast />
  </div>
</template>

<script setup>
import { computed, ref } from "vue";
import Sidebar from "@/components/Sidebar.vue";
import Toast from "@/components/Toast.vue";

import { getBackend, setBackend, getBaseUrlByBackend } from "@/services/backendSwitcher";
import { showToast } from "@/stores/toast";

const backend = ref(getBackend());
const baseUrl = computed(() => getBaseUrlByBackend(backend.value));

function onBackendChange() {
  setBackend(backend.value);
  showToast(`Switched to ${backend.value.toUpperCase()} (${baseUrl.value})`, "info");
}
</script>

<style scoped>
.app {
  display: grid;
  grid-template-columns: 240px 1fr;
  min-height: 100vh;
  font-family: system-ui, -apple-system, Segoe UI, Roboto, Arial, sans-serif;
}
.sidebar {
  background: #0f172a;
  color: #fff;
}
.content {
  background: #f8fafc;
}
.topbar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 14px 18px;
  background: #fff;
  border-bottom: 1px solid #e2e8f0;
}
.title {
  font-weight: 700;
}
.backend-switch {
  display: flex;
  align-items: center;
  gap: 10px;
}
.backend-switch select {
  padding: 6px 8px;
  border-radius: 8px;
  border: 1px solid #cbd5e1;
  background: #fff;
}
.hint {
  font-size: 12px;
  color: #64748b;
}
.page {
  padding: 18px;
}
</style>

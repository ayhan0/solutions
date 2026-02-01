<template>
  <div class="wrap">
    <div class="brand">Phonebook</div>

    <div class="section">Entity Management</div>
    <nav class="nav">
      <RouterLink to="/entities/add" class="link" active-class="active">Add Record</RouterLink>
      <RouterLink to="/entities/delete" class="link" active-class="active">Delete Record</RouterLink>
      <RouterLink to="/entities/list" class="link" active-class="active">List Records</RouterLink>
      <RouterLink to="/entities/search" class="link" active-class="active">Search Records</RouterLink>
    </nav>

    <div class="section">Web Service Operations</div>
    <nav class="nav">
      <RouterLink to="/ops" class="link" active-class="active">Operations</RouterLink>
    </nav>
  </div>
</template>

<script setup>
import { RouterLink } from "vue-router";
</script>

<style scoped>
.wrap { padding: 16px; }
.brand { font-weight: 800; font-size: 18px; margin-bottom: 16px; }
.section {
  margin-top: 14px;
  font-size: 12px;
  letter-spacing: .06em;
  text-transform: uppercase;
  color: #94a3b8;
}
.nav { display: grid; gap: 6px; margin-top: 10px; }
.link {
  padding: 10px 10px;
  border-radius: 10px;
  color: #e2e8f0;
  text-decoration: none;
}
.link:hover { background: rgba(255,255,255,.08); }
.active { background: rgba(255,255,255,.14); color: #fff; }
</style>

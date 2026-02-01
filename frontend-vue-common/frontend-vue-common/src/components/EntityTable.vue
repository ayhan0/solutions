<template>
  <div class="table-wrap">
    <table class="t">
      <thead>
      <tr>
        <th>Name</th>
        <th>Surname</th>
        <th>Age</th>
        <th>Email</th>
        <th>Phone</th>
      </tr>
      </thead>

      <tbody>
      <tr v-for="(r, idx) in rows" :key="idx">
        <td>{{ r.name }}</td>
        <td>{{ r.surname }}</td>
        <td>{{ r.age }}</td>
        <td>{{ r.email }}</td>
        <td>{{ r.phone }}</td>
      </tr>

      <tr v-if="!rows || rows.length === 0">
        <td colspan="5" class="empty">No records</td>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
defineProps({
  rows: { type: Array, default: () => [] },
});
</script>

<style scoped>
.table-wrap { background: #fff; border: 1px solid #e2e8f0; border-radius: 12px; overflow: hidden; }
.t { width: 100%; border-collapse: collapse; }
th, td { text-align: left; padding: 10px 12px; border-bottom: 1px solid #eef2f7; }
th { background: #f1f5f9; font-size: 13px; }
.empty { color: #64748b; text-align: center; padding: 18px; }
</style>

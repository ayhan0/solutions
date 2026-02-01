<template>
  <div v-if="toastState.show" class="toast" :class="toastState.type">
    {{ toastState.message }}
  </div>
</template>

<script setup>
import { toastState } from "@/stores/toast";
</script>

<style scoped>
.toast {
  position: fixed;
  top: 16px;
  right: 16px;
  padding: 10px 12px;
  border-radius: 8px;
  color: #fff;
  font-size: 14px;
  z-index: 9999;
  box-shadow: 0 8px 30px rgba(0,0,0,.15);
}
.toast.info { background: #334155; }
.toast.success { background: #16a34a; }
.toast.error { background: #dc2626; }
</style>

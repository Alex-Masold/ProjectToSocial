<template>
  <v-navigation-drawer rail permanent>
    <v-list nav density="compact">
      <v-list-item nav prepend-icon="mdi-view-dashboard" to="/user/home" />
      <v-list-item nav prepend-icon="mdi-pencil-box" to="/user/tasks" />
    </v-list>
    <v-divider></v-divider>
  </v-navigation-drawer>
  <router-view />
</template>

<script setup lang="ts">
</script>

<style scoped></style>

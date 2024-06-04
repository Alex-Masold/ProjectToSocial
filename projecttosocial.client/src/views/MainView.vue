<template>
  <div>
    <v-navigation-drawer permanent>
      <v-card-item title="Личные сообщения" />
      <v-divider></v-divider>
      <v-list nav density="compact">
        <v-list-item
          nav
          prepend-icon="mdi-chat"
          title="user 1"
          to="/user/home/chat"
        />
      </v-list>
    </v-navigation-drawer>
    <router-view />
  </div>
</template>

<script setup lang="ts"></script>

<style scoped>
.subtitle {
  padding: 1em;
}
</style>

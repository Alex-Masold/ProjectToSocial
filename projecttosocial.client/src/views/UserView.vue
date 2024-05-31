<template>
  <v-navigation-drawer permanent >
    <v-list nav>
      <v-list-item title="Home" to="/">
        <template v-slot:prepend>
          <v-icon icon="mdi-view-dashboard" size="large"></v-icon>
        </template>
      </v-list-item>
      <v-list-item title="Tasks" to="/Tasks">
        <template v-slot:prepend>
          <v-icon icon="mdi-pencil-box" size="large"></v-icon>
        </template>
      </v-list-item>
      <v-divider></v-divider>
    </v-list>
  </v-navigation-drawer>
  <router-view></router-view>
</template>

<script setup lang="ts"></script>

<style scoped></style>

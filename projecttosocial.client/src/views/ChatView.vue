<template>
  <v-row align="start" class="ma-0">
    <v-card elevation="0" class="w-100">
      <v-card-item title="Chat" subtitle="Chat with your friends">
        <template v-slot:append>
          <v-btn color="grey-lighten-1" icon="mdi-magnify" variant="text"> </v-btn>
        </template>
      </v-card-item>
    </v-card>
  </v-row>
  <v-divider></v-divider>
  <ChatWindow />
</template>

<script setup lang="ts">
import ChatWindow from '@/components/ChatWindow.vue';
</script>

<style scoped>
.header {
  background-color: #424242;
}
</style>

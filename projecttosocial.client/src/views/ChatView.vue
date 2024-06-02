<template>
  <v-card class="h-screen">
    <v-card-item class="mb-2" title="Chat" subtitle="Chat with your friends">
      <template v-slot:append>
        <v-btn 
          color="grey-lighten-1" 
          icon="mdi-magnify" 
          variant="text"> 
        </v-btn>
      </template>
    </v-card-item>
    <v-row>
      <v-divider></v-divider>
    </v-row>
    <ChatWindow />
  </v-card>
</template>

<script setup lang="ts">
import ChatWindow from '@/components/ChatWindow.vue';
</script>

<style scoped>
.header {
  background-color: #424242;
}
</style>

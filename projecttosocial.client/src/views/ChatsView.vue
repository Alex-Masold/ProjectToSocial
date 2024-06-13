<template>
  <div>
    <v-navigation-drawer permanent>
      <v-card-item title="Личные сообщения" />
      <v-divider></v-divider>
      <v-list nav density="compact">
        <v-list-item
          v-for="chat in chats"
          :key="chat.id"
          :title="chat.secondUser.firstName"
          :to="{ name: 'Chat', params: { chatId: chat.id } }"
          prepend-icon="mdi-chat"
          nav
        />
      </v-list>
    </v-navigation-drawer>
    <router-view />
  </div>
</template>

<script setup lang="ts">
import type { Chat } from '@/models/Chats';
import { inject, type Ref } from 'vue';

const chats = inject<Ref<Array<Chat>>>('userChats');

</script>

<style scoped>
.subtitle {
  padding: 1em;
}
</style>

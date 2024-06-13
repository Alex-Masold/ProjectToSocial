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
  <ChatWindow :messages="messages" />
</template>

<script setup lang="ts">
import ChatWindow from '@/components/ChatWindow.vue';
import type { Chat } from '@/models/Chats';
import { Message } from '@/models/Message';
import axios from 'axios';
import { onMounted, ref } from 'vue';
import { onBeforeRouteUpdate, useRoute } from 'vue-router';

const route = useRoute();
const chatData = ref<Chat>();
const messages = ref(Array<Message>());

onBeforeRouteUpdate(async (to) => {
  const data = await getChat(Number(to.params.chatId));
  if (data){
    chatData.value = data;
    messages.value = data.messages;
  }
})

const getChat = async (id: number) => {
  try {
    const response = await axios.get(`api/chats/${id}`);

  if (response.status === 200) {
    const data = response.data;
    // console.log(data);
    return data;
  } 
  else{
    const error = response.data;
    console.log(error);
  }
  } catch (error) {
    console.log(error);
  }
}

const loadChatData = async () => {
  const chatId = Number(route.params.chatId);
  if (!isNaN(chatId)) {
    const data = await getChat(chatId);
    if (data) {
      chatData.value = data;
      messages.value = data.messages || [];
    }
  }
}

onMounted(loadChatData);
</script>

<style scoped>
.header {
  background-color: #424242;
}
</style>

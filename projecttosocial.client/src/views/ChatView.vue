<template>
  <v-row align="start" class="ma-0">
    <v-card elevation="0" class="w-100">
      <v-card-item
        :title="`${interlocutor?.firstName} ${interlocutor?.lastName} ${interlocutor?.family}`"
        :subtitle="`${interlocutor?.email }`"
      >
        <template v-slot:append>
          <v-btn color="grey-lighten-1" icon="mdi-magnify" variant="text"> </v-btn>
        </template>
      </v-card-item>
    </v-card>
  </v-row>
  <v-divider></v-divider>
  <MessagesList :messages="messages" />
  <v-row class="ma-0">
    <v-sheet class="w-100">
      <v-text-field
        variant="solo"
        density="default"
        placeholder="Write a message..."
        v-model="messageValue"
        :append-inner-icon="messageValue && !messageValue?.startsWith(' ') ? 'mdi-send' : ''"
        @click:append-inner="sendMessage"
      >
      </v-text-field>
    </v-sheet>
  </v-row>
</template>

<script setup lang="ts">
import MessagesList from '@/components/MessagesList.vue';
import type { Chat } from '@/models/Chat';
import { Message } from '@/models/Message';
import type { User } from '@/models/User';
import axios from 'axios';
import { inject, onMounted, ref, type Ref } from 'vue';
import { onBeforeRouteUpdate, useRoute } from 'vue-router';

const route = useRoute();
const chatData = ref<Chat>();
const messages = ref(Array<Message>());
const interlocutor = ref<User>();
const messageValue = ref<string>();
const userId = inject<Ref<number>>('userId');

const sendMessage = async () => {
  try {
    const response = await axios.post(
      'https://localhost:7229/api/messages',
      {
        idUser: userId?.value,
        idChat: chatData.value?.id,
        content: messageValue.value,
        date: new Date()
      },
      {
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json'
        }
      }
    );
    if (response.status === 200) {
      const data = response.data;
      console.log(data);
      messages.value.push(data);
      messageValue.value = '';
    }
  } catch (error) {
    console.error('Error logging in:', error);
  }
};

const getChat = async (id: number) => {
  try {
    const response = await axios.get(`https://localhost:7229/api/chats/${id}`);

    if (response.status === 200) {
      console.log(333, response.data);
      const data: Chat = response.data;
      return data;
    } else {
      const error = response.data;
      console.log(error);
    }
  } catch (error) {
    console.log(error);
  }
};

const loadChatData = async () => {
  const chatId = Number(route.params.chatId);
  if (!isNaN(chatId)) {
    const data = await getChat(chatId);
    if (data) {
      console.log(222, route.params.chatId);

      console.log(data + ' ' + 'from loadChatData');
      chatData.value = data;
      messages.value = data.messages;
      interlocutor.value = data.users.find((user: User) => user.id !== userId?.value);
    }
  }
};

onBeforeRouteUpdate(async (to) => {
  console.log(111, to.params.chatId);

  const data = await getChat(Number(to.params.chatId));
  if (data) {
    chatData.value = data;
    messages.value = data.messages;
  }
});

onMounted(loadChatData);
</script>

<style scoped>
.header {
  background-color: #424242;
}
</style>

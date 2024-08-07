<template>
  <v-container fluid class="ma-0 pa-0 w-100 d-flex flex-column" style="height: 100vh">
    <v-row class="ma-0 flex-grow-0">
      <v-col class="ma-0 pa-0">
        <v-card
          elevation="0"
          :title="`${interlocutor?.firstName} ${interlocutor?.lastName} ${interlocutor?.family}`"
          :subtitle="`${interlocutor?.email}`"
        >
          <template v-slot:append>
            <v-btn color="grey-lighten-1" icon="mdi-magnify" variant="text"> </v-btn>
          </template>
        </v-card>
      </v-col>
    </v-row>
    <v-sheet>
      <v-divider></v-divider>
    </v-sheet>
    <v-row class="ma-0" style="height: 70vh">
      <messages-list
        class="position-relative d-flex flex-column h-100 w-100"
        :messages="messages"
        @message-delete="deleteMessageBehavior"
        @message-edit="editMessageBehavior"
      />
    </v-row>

    <v-row class="ma-0 flex-grow-0">
      <v-col class="pa-0 w-100">
        <v-card
          v-if="isEdit"
          rounded="0"
          elevation="0"
          density="compact"
          :text="prevMessageValue"
        />
        <v-text-field
          rounded="0"
          variant="solo-filled"
          hide-details
          placeholder="Write a message..."
          v-model="messageValue"
        >
          <template v-slot:append-inner v-if="messageValue && !messageValue?.startsWith(' ')">
            <v-icon
              :icon="isEdit === true ? 'mdi-check' : 'mdi-send'"
              @click="isEdit === true ? successEditMessageBehavior() : sendMessage()"
              @keyup.enter="isEdit === true ? () => {} : sendMessage()"
            />
          </template>
        </v-text-field>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import MessagesList from '@/components/MessagesList.vue';
import type { Chat } from '@/models/Chat';
import type { Message } from '@/models/Message';
import type { User } from '@/models/User';
import axios from 'axios';
import { inject, onMounted, ref, type Ref } from 'vue';
import { onBeforeRouteUpdate, useRoute } from 'vue-router';

const route = useRoute();

const chatData = ref<Chat>();
const messages = ref(Array<Message>());
const interlocutor = ref<User>();

const isEdit = ref<boolean>(false);
const messageId = ref<number>();
const messageValue = ref<string>();
const prevMessageValue = ref<string>();

const userId = inject<Ref<number>>('userId');

const sendMessage = async () => {
  try {
    const response = await axios.post(
      'https://localhost:7229/api/messages',
      {
        idUser: userId?.value,
        idChat: chatData.value?.id,
        content: messageValue.value,
        date: new Date().getTime()
      },
      {
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json'
        }
      }
    );
    if (response.status === 200) {
      const data: Message = response.data;
      data.date = new Date(data.date)
      console.log(data);

      messages.value.push(data);
      messageValue.value = '';
    }
  } catch (error) {
    console.error('Error logging in:', error);
  }
};

const editMessageBehavior = async (Id: number, Content: string) => {
  isEdit.value = true;

  messageId.value = Id;
  prevMessageValue.value = Content;
  messageValue.value = prevMessageValue.value;
};

const successEditMessageBehavior = async () => {
  try {
    const response = await axios.put(
      'https://localhost:7229/api/messages',
      {
        id: messageId.value,
        idUser: userId?.value,
        idChat: chatData.value?.id,
        content: messageValue.value,
        date: new Date().getTime(),
        isEdit: true
      },
      {
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json'
        }
      }
    );
    if (response.status === 200) {
      const data: Message = response.data;
      console.log(data);
      let editedMessage: Message = messages.value.find(
        (message) => message.id == data.id
      ) as Message;
      editedMessage.content = data.content;
      editedMessage.isEdit = data.isEdit;
      editedMessage.date = new Date(data.date);

      isEdit.value = false;
      prevMessageValue.value = '';
      messageValue.value = '';
    }
  } catch (error) {
    console.error('Error logging in:', error);
  }
};

const deleteMessageBehavior = async (messageId: number) => {
  try {
    const response = await axios.delete(`https://localhost:7229/api/messages/${messageId}`, {
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
      }
    });
    if (response.status === 200) {
      const data: number = response.data;
      console.log(data);
      const index = messages.value.findIndex((message) => message.id === data);
      if (index !== -1) {
        messages.value.splice(index, 1);
      }
    }
  } catch (error) {
    console.error('Error logging in:', error);
  }
};

const getChat = async (id: number) => {
  try {
    const response = await axios.get(`https://localhost:7229/api/chats/${id}`);

    if (response.status === 200) {
      const data: Chat = response.data;
      data.messages.map(message => message.date = new Date(message.date))
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
      console.log(data + ' ' + 'from loadChatData');
      data.messages.map(message => message.date = new Date(message.date))

      chatData.value = data;
      messages.value = chatData.value.messages;
      interlocutor.value = data.users.find((user: User) => user.id !== userId?.value);
    }
  }
};

onBeforeRouteUpdate(async (to) => {
  const data = await getChat(Number(to.params.chatId));
  if (data) {
    console.log(data + ' ' + 'load from onBeforeRouteUpdate');
    chatData.value = data;
    messages.value = chatData.value.messages;
    interlocutor.value = chatData.value.users.find((user: User) => user.id !== userId?.value);
  }
});

onMounted(loadChatData);
</script>

<style scoped>
.chat-view {
  display: flex;
  flex-direction: column;
  height: 100vh;
}
</style>

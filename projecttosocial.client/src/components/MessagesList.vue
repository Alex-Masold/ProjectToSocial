<template>
  <v-row class="ma-0">
    <v-sheet class="w-100 messages">
        <v-list nav>
          <v-list-item
            :class="{
              'your-message-container': message.idUser === id,
              'interlocutor-message-container': message.idUser !== id
            }"
            v-for="message in messages"
            :key="message.id"
          >          
            <v-card class="content">
              <div>{{ message.content }}</div>
            </v-card>
          </v-list-item>
        </v-list>
    </v-sheet>
  </v-row>
  
</template>

<script setup lang="ts">
import { Message } from '@/models/Message';
import { inject, type Ref } from 'vue';

const id = inject<Ref<number>>('userId');

const props = defineProps({
  messages: {
    type: Array<Message>,
    required: true
  }
});
</script>

<style scoped>
.messages {
  overflow-y: scroll;
  height: 83vh;
}
.content {
  padding: 0.5em;
}
.your-message-container {
  display: flex;
  justify-content: end;
  padding-bottom: 0.5ex;
}

.your-message-container .content {
  background-color: #3c3c3c;
}
.interlocutor-message-container {
  display: flex;
  padding-bottom: 0.5ex;
}
.interlocutor-message-container .content {
  background-color: #303030;
}
</style>

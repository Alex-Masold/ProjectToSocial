<template>
    <v-sheet class="messages-container">
      <v-list nav>
        <v-list-item
          :class="{
            'your-message-container': message.idUser === userId,
            'interlocutor-message-container': message.idUser !== userId
          }"
          v-for="message in messages"
          :key="message.id"
        >
          <VSpeedDial location="end" transition="fade-transition">
            <template v-slot:activator="{ props: activatorProps }">
              <v-card v-bind="activatorProps" class="content">
                <div>{{ message.content }}</div>
              </v-card>
            </template>
            <v-btn
              key="1"
              v-if="message.idUser === userId"
              icon="mdi-pencil"
              class="bg-grey-darken-3"
              @click="editMessage(message.id, message.content)"
            ></v-btn>
            <v-btn
              key="2"
              icon="mdi-delete"
              class="bg-grey-darken-3"
              @click="deleteMessage(message.id)"
            ></v-btn>
          </VSpeedDial>
        </v-list-item>
      </v-list>
    </v-sheet>
</template>

<script setup lang="ts">
import { Message } from '@/models/Message';
import { inject, type Ref } from 'vue';
import { VSpeedDial } from 'vuetify/components';

const emit = defineEmits(['messageDelete', 'messageEdit']);

const userId = inject<Ref<number>>('userId');

const props = defineProps({
  messages: {
    type: Array<Message>,
    required: true
  }
});

const deleteMessage = (messageId: number) => {
  emit('messageDelete', messageId);
};

const editMessage = (messageId: number, messageContent: string) => {
  emit('messageEdit', messageId, messageContent);
};
</script>

<style scoped>
.messages-container{
  overflow: hidden;
}
.messages-container:hover
{
  overflow-y: auto;
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

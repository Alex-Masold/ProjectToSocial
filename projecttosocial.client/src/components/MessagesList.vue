<template>
  <v-list nav class="messages-container">
    <v-list-item
      :class="{
        'your-message-container': message.idUser === userId,
        'interlocutor-message-container': message.idUser !== userId
      }"
      v-for="message in messages"
      :key="message.id"
      :id="message.id"
    >
      <v-hover v-slot="{ isHovering, props }">
        <v-speed-dial location="end" transition="fade-transition">
          <template v-slot:activator="{ props: activatorProps }">
            <v-card
              class="pa-2"
              :class="{
                'bg-grey-darken-1': message.idUser === userId,
                'bg-grey-darken-3': message.idUser !== userId
              }"
              :max-width="400"
              elevation="0"
              v-bind="activatorProps"
            >
              <v-list-item-title class="text-body-1" style="white-space: normal">
                {{ message.content }}
              </v-list-item-title>
              <v-list-item-subtitle v-bind="props" class="d-flex" style="white-space: normal">
                <span v-if="message.isEdit">Изменнено&nbsp;</span>
                {{ message.date.getHours() }}:{{ message.date.getMinutes() }}
                <v-fade-transition>
                  <v-card variant="text" v-show="isHovering">
                    &nbsp;
                    {{
                      message.date.getDate().toString().length > 1
                        ? message.date.getDate()
                        : `0${message.date.getDate()}`
                    }}.{{
                      message.date.getMonth().toString().length > 1
                        ? message.date.getMonth()
                        : `0${message.date.getMonth()}`
                    }}.{{ message.date.getFullYear() }}
                  </v-card>
                </v-fade-transition>
              </v-list-item-subtitle>
            </v-card>
          </template>
          <v-btn
            key="1"
            v-if="
              message.idUser === userId &&
              message.date.getDate() === new Date().getDate() &&
              message.date.getMonth() === new Date().getMonth() &&
              message.date.getFullYear() === new Date().getFullYear()
            "
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
        </v-speed-dial>
      </v-hover>
    </v-list-item>
  </v-list>
</template>

<script setup lang="ts">
import { Message } from '@/models/Message';
import { inject, type Ref } from 'vue';
import type { VSpeedDial } from 'vuetify/components';

const emit = defineEmits(['messageDelete', 'messageEdit']);

const userId = inject<Ref<number>>('userId');

defineProps({
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
.messages-container {
  scrollbar-width: none;
  overflow: hidden;
}
.messages-container:hover {
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

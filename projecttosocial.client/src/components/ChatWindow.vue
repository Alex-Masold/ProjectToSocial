<template>
  <v-row class="ma-0">
    <v-sheet class="w-100 messages">
      <v-container class="pt-0 pb-0">
        <v-list nav class="d-flex flex-column" density="compact">
          <div class="your-message-container">
            <v-list-item class="content">
              <p>
                Lorem ipsum dolor, sit amet consectetur adipisicing elit. Distinctio earum modi
                voluptas deleniti quo delectus esse illum quod pariatur! Saepe explicabo sed
                aspernatur laudantium inventore repudiandae quibusdam cupiditate corporis molestiae.
              </p>
            </v-list-item>
          </div>
          <div class="your-message-container">
            <v-list-item class="content">
              <p>
                Lorem ipsum dolor, sit amet consectetur adipisicing elit. Distinctio earum modi
                voluptas deleniti quo delectus esse illum quod pariatur! Saepe explicabo sed
                aspernatur laudantium inventore repudiandae quibusdam cupiditate corporis molestiae.
              </p>
            </v-list-item>
          </div>
          <div class="your-message-container">
            <v-list-item class="content">
              <p>
                Lorem ipsum dolor, sit amet consectetur adipisicing elit. Distinctio earum modi
                voluptas deleniti quo delectus esse illum quod pariatur! Saepe explicabo sed
                aspernatur laudantium inventore repudiandae quibusdam cupiditate corporis molestiae.
              </p>
            </v-list-item>
          </div>
          <div class="your-message-container">
            <v-list-item class="content">
              <p>
                Lorem ipsum dolor, sit amet consectetur adipisicing elit. Distinctio earum modi
                voluptas deleniti quo delectus esse illum quod pariatur! Saepe explicabo sed
                aspernatur laudantium inventore repudiandae quibusdam cupiditate corporis molestiae.
              </p>
            </v-list-item>
          </div>
          <div class="your-message-container">
            <v-list-item class="content">
              <p>
                Lorem ipsum dolor, sit amet consectetur adipisicing elit. Distinctio earum modi
                voluptas deleniti quo delectus esse illum quod pariatur! Saepe explicabo sed
                aspernatur laudantium inventore repudiandae quibusdam cupiditate corporis molestiae.
              </p>
            </v-list-item>
          </div>
          <div class="interlocutor-message-container">
            <v-list-item class="content">
              <p>
                Lorem ipsum dolor, sit amet consectetur adipisicing elit. Distinctio earum modi
                voluptas deleniti quo delectus esse illum quod pariatur! Saepe explicabo sed
                aspernatur laudantium inventore repudiandae quibusdam cupiditate corporis molestiae.
              </p>
            </v-list-item>
          </div>
        </v-list>
      </v-container>
    </v-sheet>
  </v-row>
  <v-row class="ma-0">
    <v-sheet class="w-100">
      <v-text-field density="compact" hide-details="auto" append-inner-icon="mdi-send"/>
    </v-sheet>
  </v-row>
</template>

<script setup lang="ts">
import { computed } from 'vue';

const TypeUser = computed(() => ({}));
</script>

<style scoped>
.messages {
  overflow-y: scroll;
  height: 85vh;
}
.content {
  max-width: 25em;
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

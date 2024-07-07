<template>
  <v-navigation-drawer rail permanent>
    <v-list nav density="compact">
      <v-list-item
        nav
        prepend-icon="mdi-view-dashboard"
        :to="{ name: 'Search', params: { userId: userData?.id } }"
      />
      <v-list-item nav prepend-icon="mdi-pencil-box" />
    </v-list>
    <v-divider></v-divider>
    <v-list nav density="compact">
      <v-list-item
        v-for="project in userData?.projects"
        :key="project.id"
        prepend-icon="mdi-account"
        nav
      />
    </v-list>
  </v-navigation-drawer>
  <router-view />
</template>

<script setup lang="ts">
import type { Chat } from '@/models/Chat';
import type { User } from '@/models/User';
import axios from 'axios';
import { onMounted, provide, ref } from 'vue';
import { onBeforeRouteUpdate, useRoute } from 'vue-router';

const route = useRoute();

const dialogIconSize: number = 32;

const id = ref(0);
const dialog = ref<boolean>(false);

const firstName = ref<string>();
const lastName = ref<string>();
const family = ref<string>();

const userData = ref<User>();
const chats = ref<Array<Chat>>([]);

provide('userChats', chats);
provide('userId', id);



const getUser = async (id: number) => {
  try {
    const response = await axios.get(`https://localhost:7229/api/users/${id}`);

    if (response.status === 200) {
      const data: User = response.data;
      return data;
    } else {
      const error = response.data;
      console.log(error);
    }
  } catch (error) {
    console.log(error);
  }
};



const loadUserData = async () => {
  const userId = Number(route.params.userId);
  if (!isNaN(userId)) {
    const data = await getUser(userId);
    if (data) {
      console.log(data + ' ' + 'from loadUserData');
      userData.value = data;
      id.value = data.id || 0;
      chats.value = data.chats || [];
    }
  }
};

onBeforeRouteUpdate(async (to) => {
  const data = await getUser(Number(to.params.userId));
  if (data) {
    console.log(data + ' ' + 'from onBeforeRouteUpdate');
    userData.value = data;
    id.value = data.id || 0;
    chats.value = data.chats || [];
  }
});

onMounted(loadUserData);
</script>

<style scoped></style>

<template>
  <v-navigation-drawer rail permanent>
    <v-list nav density="compact">
      <v-list-item nav prepend-icon="mdi-view-dashboard" :to="{name: 'Search', params: {userId: userData?.id}}"/>
      <v-list-item nav prepend-icon="mdi-pencil-box" to="/user/tasks" />
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
import { Chat } from '@/models/Chats';
import type { User } from '@/models/User';
import axios from 'axios';
import { onMounted, provide, ref } from 'vue';
import { onBeforeRouteUpdate, useRoute } from 'vue-router';

const route = useRoute();
const userData = ref<User>();
const chats = ref<Array<Chat>>([]);
  provide("userChats", chats);

onBeforeRouteUpdate(async (to) => {
  const data = await getUser(Number(to.params.userId));
  if (data) {
    userData.value = data;
    chats.value = data.chatFirstUser || [];
  }
});

const getUser = async (id: number) => {
  try {
    const response = await axios.get(`api/users/${id}`);

    if (response.status === 200) {
      const data = response.data;
      // console.log(data);
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
  const userId = Number(route.params.id);
  if (!isNaN(userId)) {
    const data = await getUser(userId);
    if (data) {
      userData.value = data;
      chats.value = data.chatFirstUser || [];
    }
  }
};

onMounted(loadUserData);
</script>

<style scoped></style>

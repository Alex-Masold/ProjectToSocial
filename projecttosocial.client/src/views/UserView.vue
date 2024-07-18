<template>
  <v-navigation-drawer rail permanent>
    <v-list nav density="compact">
      <v-list-item nav prepend-icon="mdi-account" to="" @click="dialog = true"> </v-list-item>
    </v-list>
    <v-divider></v-divider>
    <v-list nav density="compact">
      <v-list-item
        nav
        prepend-icon="mdi-view-dashboard"
        :to="{ name: 'Search', params: { userId: id } }"
      />
      <v-list-item nav prepend-icon="mdi-pencil-box" />
    </v-list>
    <v-divider></v-divider>
    <v-list nav density="compact">
      <v-list-item v-for="project in projects" :key="project.id" prepend-icon="mdi-account" nav />
    </v-list>
  </v-navigation-drawer>

  <v-dialog v-model="dialog" width="auto">
    <v-card title="Информация об аккаунте" max-width="400">
      <v-card-text>
        <VContainer class="ma-0 pa-0">
          <v-row>
            <v-col>
              <NameDialog
                :first-name="firstName"
                :last-name="lastName"
                :family="family"
                @edit-user-name="editUserNameBehavior"
              />
            </v-col>

            <v-col>
              <email-dialog 
                :email="email" 
                @edit-user-email="editUserEmailBehavior" />
            </v-col>

            <v-col>
              <PasswordDialog
                :error-message="passwordMessage"
                @edit-user-password="editUserPasswordBehavior"
              />
            </v-col>
          </v-row>
        </VContainer>
      </v-card-text>
    </v-card>
  </v-dialog>

  <router-view />
</template>

<script setup lang="ts">
import EmailDialog from '@/components/EmailDialog.vue';
import NameDialog from '@/components/NameDialog.vue';
import PasswordDialog from '@/components/PasswordDialog.vue';
import type { Chat } from '@/models/Chat';
import type { Project } from '@/models/Project';
import type { User } from '@/models/User';
import axios from 'axios';
import { onMounted, provide, ref } from 'vue';
import { onBeforeRouteUpdate, useRoute } from 'vue-router';

const route = useRoute();

const id = ref<number>(0);
const firstName = ref<string>();
const lastName = ref<string>();
const family = ref<string>();
const email = ref<string>();
const password = ref<string>();

const chats = ref<Array<Chat>>();
const projects = ref<Array<Project>>();

const emailMessage = ref<string>();
const passwordMessage = ref<string>();

const dialog = ref<boolean>(false);

provide('userChats', chats);
provide('userId', id);

const getUser = async (id: number) => {
  try {
    const response = await axios.get(`https://localhost:7229/api/user/${id}`);

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

const editUserNameBehavior = async (
  newFirstName: string,
  newLastName: string,
  newFamily: string
) => {
  try {
    const response = await axios.put(
      'https://localhost:7229/api/user',
      {
        id: id.value,
        firstName: newFirstName,
        lastName: newLastName,
        family: newFamily
      },
      {
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json'
        }
      }
    );
    if (response.status === 200) {
      const data: Map<string, string> = new Map<string, string>(Object.entries(response.data));
      // console.log(data);
      if (data.has('FirstName')) {
        firstName.value = data.get('FirstName');
      }
      if (data.has('LastName')) {
        lastName.value = data.get('LastName');
      }
      if (data.has('Family')) {
        family.value = data.get('Family');
      }
    }
  } catch (error: any) {
    console.error('Error editing user name in:', error);
  }
};

const editUserEmailBehavior = async (newEmail: string) => {
  try {
    const response = await axios.put(
      'https://localhost:7229/api/user',
      {
        id: id.value,
        email: newEmail
      },
      {
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json'
        }
      }
    );
    if (response.status === 200) {
      const data: Map<string, string> = new Map<string, string>(Object.entries(response.data));
      // console.log(data);

      if (data.has('Email')) {
        email.value = data.get('Email') as string;
      }
    }
  } catch (error) {
    console.error('Error editing user Email in:', error);
  }
};

const editUserPasswordBehavior = async (newPassword: string) =>
  await axios
    .put(
      'https://localhost:7229/api/user',
      {
        id: id.value,
        password: newPassword
      },
      {
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json'
        }
      }
    )
    .then((response) => {
      if (response.status === 200) {
        const data: Map<string, string> = new Map<string, string>(Object.entries(response.data));
        // console.log(data);
        if (data.has('password')) {
          password.value = data.get('password') as string;
        }
      }
    })
    .catch((error) => {
      if (error.response) {
        if (error.response.status === 400) {
          let errorMessage = error.response.data.errors[0];
          console;
          console.log(errorMessage);
        }
      }
    });

const fillUser = (userData: User): void => {
  id.value = userData.id;

  firstName.value = userData.firstName || '';
  lastName.value = userData.lastName || '';
  family.value = userData.family || '';
  email.value = userData.email || '';
  password.value = userData.password || '';

  chats.value = userData.chats || [];
  projects.value = userData.projects || [];
};

const loadUserData = async () => {
  const userId = Number(route.params.userId);
  if (!isNaN(userId)) {
    const data = await getUser(userId);
    if (data) {
      console.log(data + ' ' + 'from loadUserData');
      fillUser(data);
    }
  }
};

onBeforeRouteUpdate(async (to) => {
  const data = await getUser(Number(to.params.userId));
  if (data) {
    console.log(data + ' ' + 'from onBeforeRouteUpdate');
    fillUser(data);
  }
});

onMounted(loadUserData);
</script>

<style>
.v-btn__content {
  overflow: auto;
}
.v-btn__content .text-overflow {
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>

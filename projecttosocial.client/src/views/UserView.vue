<template>
  <v-navigation-drawer rail permanent>
    <v-list nav density="compact">
      <v-dialog width="auto">
        <template v-slot:activator="{ props: activatorProps }">
          <v-list-item nav prepend-icon="mdi-account" v-bind="activatorProps"> </v-list-item>
        </template>
        <template v-slot:default>
          <v-card title="Информация об аккаунте" max-width="400">
            <v-card-text>
              <VContainer class="ma-0 pa-0">
                <v-row>
                  <v-col>
                    <NameDialog
                      v-model:is-valid="isValid"
                      v-model:is-active="isActiveNameDialog"
                      :first-name="firstName"
                      :last-name="lastName"
                      :family="family"
                      :error-messages="nameMessages"
                      @edit-user-name="editUserNameBehavior"
                      @reset-dialog="resetDialogBehavior"
                    />
                  </v-col>

                  <v-col>
                    <email-dialog
                      v-model:is-valid="isValid"
                      v-model:is-active="isActiveEmailDialog"
                      :email="email"
                      :error-messages="emailMessage"
                      @edit-user-email="editUserEmailBehavior"
                      @reset-dialog="resetDialogBehavior"
                    />
                  </v-col>

                  <v-col>
                    <PasswordDialog
                      v-model:is-valid="isValid"
                      v-model:is-active="isActivePasswordDialog"
                      :error-messages="passwordMessage"
                      @edit-user-password="editUserPasswordBehavior"
                      @reset-dialog="resetDialogBehavior"
                    />
                  </v-col>
                </v-row>
              </VContainer>
            </v-card-text>
          </v-card>
        </template>
      </v-dialog>
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

const nameMessages = ref<Map<string, string>>(new Map<string, string>());
const emailMessage = ref<string>('');
const passwordMessage = ref<string>('');

const isValid = ref<boolean>(false);
const isActiveNameDialog = ref<boolean>(false);
const isActiveEmailDialog = ref<boolean>(false);
const isActivePasswordDialog = ref<boolean>(false);

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
  await axios
    .put(
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
    )
    .then((response) => {
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
      isValid.value = true;
      isActiveNameDialog.value = false;
      nameMessages.value = new Map<string, string>();
    })
    .catch((error) => {
      if (error.response) {
        if (error.response.status === 400) {
          const errors = error.response.data?.errors;
          if (errors) {
            const errorMessages: Map<string, Array<string>> = new Map<string, Array<string>>(
              Object.entries(errors)
            );
            if (errorMessages.has('')) {
              let errorMessage: string = errorMessages.get('')?.[0] as string;
              nameMessages.value?.set('FirstName', errorMessage);
              nameMessages.value?.set('LastName', errorMessage);
              nameMessages.value?.set('Family', errorMessage);
            }
            isValid.value = false;
            isActiveNameDialog.value = true;
            console.log(nameMessages.value);
          }
        }
      }
    });
};

const resetDialogBehavior = (typeDialog: string) => {
  switch (typeDialog) {
    case 'NameDialog':
      nameMessages.value = new Map<string, string>();
      isActiveNameDialog.value = false;
      break;
    case 'EmailDialog':
      emailMessage.value = '';
      isActiveEmailDialog.value = false;
      break;
    case 'PasswordDialog':
      passwordMessage.value = '';
      isActivePasswordDialog.value = false;
      break;
  }
  isValid.value = false;
};
const editUserEmailBehavior = async (newEmail: string) => {
  await axios
    .put(
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
    )
    .then((response) => {
      const data: Map<string, string> = new Map<string, string>(Object.entries(response.data));
      // console.log(data);
      if (data.has('Email')) {
        email.value = data.get('Email');
      }
      isValid.value = true;
      isActiveEmailDialog.value = false;
      emailMessage.value = '';
    })
    .catch((error) => {
      if (error.response) {
        if (error.response.status === 400) {
          const errors = error.response.data?.errors;
          if (errors) {
            const errorMessages: Map<string, Array<string>> = new Map<string, Array<string>>(
              Object.entries(errors)
            );
            if (errorMessages.has('Email')) {
              let errorMessage: string = errorMessages.get('Email')?.[0] as string;
              emailMessage.value = errorMessage;
            }
            isValid.value = false;
            isActiveEmailDialog.value = true;
            console.log(emailMessage.value);
          }
        }
      }
    });
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

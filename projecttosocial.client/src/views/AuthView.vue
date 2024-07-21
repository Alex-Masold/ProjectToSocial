<template>
  <v-container class="fill-height">
    <v-card class="mx-auto container px-6 py-8">
      <v-form @submit.prevent="login">
        <v-text-field
          class="mb-2"
          label="Email"
          placeholder="Enter your email"
          v-model="email"
          clearable
        ></v-text-field>

        <v-text-field
          label="Password"
          placeholder="Enter your password"
          v-model="password"
          clearable
        ></v-text-field>

        <br />

        <v-btn block color="success" size="large" type="submit" variant="elevated"> Sign In </v-btn>
      </v-form>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
import axios from 'axios';
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const id = ref<number>(0);
const email = ref<string>('admin@gmail.com');
const password = ref<string>('JustAdmin');
const router = useRouter();

const login = async () => {
  try {
    const response = await axios.post(
      'https://localhost:7229/api/authentication',
      {
        email: email.value,
        password: password.value
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
      id.value = data.id;
      router.push({ name: 'Search', params: { userId: data.id } });
    } else {
      const error = response.data;
      console.log(error);
    }
  } catch (error) {
    console.error('Error logging in:', error);
  }
};
</script>

<style scoped>
.container {
  width: 100%;
  max-width: 500px;
  min-width: 400px;
  margin-right: 1rem;
  margin-left: 1rem;
  padding: 1.5em 2em;
}
</style>

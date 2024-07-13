<template>
  <v-dialog width="auto" persistent>
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn block v-bind="activatorProps" variant="text" style="justify-content: space-between">
        <template v-slot:prepend>
          <v-icon color="grey-darken-1" icon="mdi-email" :size="$props.iconSize" />
          <span class="text-grey-darken-1">&nbsp;Email</span>
        </template>
        <template v-slot:default>
          <span class="text-overflow">
            {{ $props.email }}
          </span>
        </template>
      </v-btn>
    </template>

    <template v-slot:default="{ isActive }">
      <v-form ref="form" v-model="valid">
        <v-card title="Редактирование email">
          <v-card-text>
            <v-row no-gutters>
              <v-col>
                <v-text-field
                  v-model="email"
                  :rules="emailRules"
                  variant="underlined"
                  label="email"
                >
                </v-text-field>
              </v-col>
            </v-row>
          </v-card-text>
          <v-card-actions>
            <v-btn text="Отмена" variant="text" @click="isActive.value = !isActive.value"></v-btn>
            <v-btn
              :disabled="!valid"
              text="изменить email"
              variant="text"
              @click="() => (editUserEmail(email), (isActive.value = !isActive.value))">
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-form>
    </template>
  </v-dialog>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const props = defineProps({
  email: {
    type: String,
    requare: true
  },
  iconSize: {
    type: Number,
    default: 32
  }
});

const emit = defineEmits(['editUserEmail']);

const email = ref<string>(props.email as string);

const valid = ref(false);

const editUserEmail = (email: string) => {
  emit('editUserEmail', email);
};

const emailRules = [
  (value: string) => !!value || 'Поле обезаательно для заполнения',
  (value: string) => {
    const pattern =
      /^([a-zA-Z0-9]+(\.[a-zA-Z0-9]+)*)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return pattern.test(value) || 'Неверный email';
  }
];
</script>

<style scoped></style>

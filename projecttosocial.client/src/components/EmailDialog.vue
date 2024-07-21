<template>
  <v-dialog v-model="isActive" width="auto" persistent>
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

    <template v-slot:default>
      <v-form v-model="isValid">
        <v-card title="Редактирование email">
          <v-card-text>
            <v-row no-gutters>
              <v-col>
                <v-text-field
                  v-model="newEmail"
                  variant="underlined"
                  label="email"
                  :rules="emailRules"
                  :messages="$props.errorMessages ? $props.errorMessages : ''"
                >
                </v-text-field>
              </v-col>
            </v-row>
          </v-card-text>
          <v-card-actions>
            <v-btn text="Отмена" variant="text" @click="resetDialog"> </v-btn>
            <v-btn
              text="изменить email"
              type="submit"
              variant="text"
              :disabled="!isValid"
              @click="editUserEmail(newEmail)"
            ></v-btn>
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
  errorMessages: {
    type: String,
    requare: true
  },
  iconSize: {
    type: Number,
    default: 32
  }
});

const emit = defineEmits(['editUserEmail', 'resetDialog']);

const isValid = defineModel<boolean>('isValid', { required: true });
const isActive = defineModel<boolean>('isActive', { required: true });

const newEmail = ref<string>('');

const emailRules = [
  (value: string) => !!value || 'Поле обезаательно для заполнения',
  (value: string) => value !== props.email || 'Это текущий email',
  (value: string) => {
    const pattern =
      /^([a-zA-Z0-9]+(\.[a-zA-Z0-9]+)*)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return pattern.test(value) || 'Неверный email';
  }
];

const editUserEmail = (email: string) => {
  emit('editUserEmail', email);
};

const resetDialog = (): void => {
  emit('resetDialog', 'EmailDialog');
  newEmail.value = '';
};
</script>

<style scoped></style>

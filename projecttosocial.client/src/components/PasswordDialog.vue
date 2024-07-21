<template>
  <v-dialog v-model="isActive" width="auto" persistent>
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn block v-bind="activatorProps" variant="text" style="justify-content: space-between">
        <template v-slot:prepend>
          <v-icon color="grey-darken-1" icon="mdi-lock" :size="$props.IconSize" />
          <span class="text-grey-darken-1">&nbsp;Password</span>
        </template>
        <template v-slot:default>
          <span class="text-overflow"> поменять пароль </span>
        </template>
      </v-btn>
    </template>

    <template v-slot:default>
      <v-form v-model="isValid">
        <v-card title="Редактирование пароля">
          <v-card-text>
            <v-row no-gutters>
              <v-col>
                <v-text-field
                  v-model="newPassword"
                  variant="underlined"
                  label="Пароль"
                  :rules="newPasswordRules"
                  :messages="$props.errorMessages ? $props.errorMessages : ''"
                  :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                  :type="showPassword ? 'text' : 'password'"
                  @click:append="showPassword = !showPassword"
                >
                </v-text-field>
              </v-col>
            </v-row>
            <v-row no-gutters>
              <v-col>
                <v-text-field
                  v-model="passwordConfirm"
                  variant="underlined"
                  label="Повторите пароль"
                  :rules="passwordConfirmRules"
                  :messages="$props.errorMessages ? $props.errorMessages : ''"
                  :append-icon="showConfirm ? 'mdi-eye' : 'mdi-eye-off'"
                  :type="showConfirm ? 'text' : 'password'"
                  @click:append="showConfirm = !showConfirm"
                />
              </v-col>
            </v-row>
          </v-card-text>
          <v-card-actions>
            <v-btn text="Отмена" variant="text" @click="resetDialog"></v-btn>
            <v-btn
              text="Потвердить"
              type="submit"
              variant="text"
              :disabled="!isValid"
              @click="editUserPassword(newPassword, passwordConfirm)"
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
  errorMessages: {
    type: String,
    requare: true
  },
  IconSize: {
    type: Number,
    default: 32
  }
});

const emit = defineEmits(['editUserPassword', 'resetDialog']);

const isValid = defineModel<boolean>('isValid', { required: true });
const isActive = defineModel<boolean>('isActive', { required: true });

const newPassword = ref<string>('');
const passwordConfirm = ref<string>('');

const showPassword = ref<boolean>(false);
const showConfirm = ref<boolean>(false);

const newPasswordRules = [
  (value: string) => !!value || 'Поле обезаательно для заполнения',
  (value: string) => value.length >= 8 || 'Минимальная длина 8 символа',
  (value: string) => !/\s/.test(value) || 'Нельзя использовать пробелы в поле',
  (value: string) => !/\p{P}/u.test(value) || 'Нельзя использовать знаки пунктуации в поле',
  (value: string) => !/\p{S}/u.test(value) || 'Нельзя использовать математические знаки в поле'
];

const passwordConfirmRules = [
  (value: string) => !!value || 'Поле обезаательно для заполнения',
  (value: string) => value === newPassword.value || 'Пароли не совпадают'
];
const editUserPassword = (password: string, passwordConfirm: string) => {
  emit('editUserPassword', password, passwordConfirm);
};

const resetDialog = (): void => {
  emit('resetDialog', 'PasswordDialog');
  newPassword.value = '';
  passwordConfirm.value = '';
  showPassword.value = false;
  showConfirm.value = false;
};
</script>

<style scoped></style>

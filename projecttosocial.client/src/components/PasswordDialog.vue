<template>
  <v-dialog width="auto" persistent>
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
    <template v-slot:default="{ isActive }">
      <v-form ref="form" v-model="valid">
        <v-card title="Редактирование пароля">
          <v-card-text>
            <VRow no-gutters>
              <VCol>
                <VTextField
                  v-model="newPassword"
                  variant="underlined"
                  label="Пароль"
                  :rules="newPasswordRules"
                  :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                  :type="showPassword ? 'text' : 'password'"
                  @click:append="showPassword = !showPassword"
                >
                <template v-slot:message="{ message }">
                  <span>{{ message || props.errorMessage }}</span>
                </template>
              </VTextField>
              </VCol>
            </VRow>
            <VRow no-gutters>
              <VCol>
                <VTextField
                  v-model="passwordConfirm"
                  variant="underlined"
                  label="Повторите пароль"
                  :rules="passwordConfirmRules"
                  :append-icon="showConfirm ? 'mdi-eye' : 'mdi-eye-off'"
                  :type="showConfirm ? 'text' : 'password'"
                  @click:append="showConfirm = !showConfirm"
                />
              </VCol>
            </VRow>
          </v-card-text>
          <VCardActions>
            <VBtn
              text="Отмена"
              variant="text"
              @click="reset(), (isActive.value = !isActive.value)"
            ></VBtn>
            <VBtn
              :disabled="!valid"
              text="Потвердить" 
              type="submit"
              variant="text"
              @click="
                editUserPassword(newPassword, passwordConfirm),
                  reset(),
                  (isActive.value = !isActive.value)
              "
            ></VBtn>
          </VCardActions>
        </v-card>
      </v-form>
    </template>
  </v-dialog>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const props = defineProps({
  errorMessage: {
    type: String,
    requare: true
  },
  IconSize: {
    type: Number,
    default: 32
  }
});

const emit = defineEmits(['editUserPassword']);

const newPassword = ref<string>('');
const passwordConfirm = ref<string>('');

const valid = ref<boolean>(false);
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

const reset = (): void => {
  newPassword.value = '';
  passwordConfirm.value = '';
  showPassword.value = false;
};
</script>

<style scoped></style>

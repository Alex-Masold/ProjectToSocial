<template>
  <v-dialog v-model="isActive" width="auto" persistent>
    <template v-slot:activator="{ props: activatorProps }">
      <v-btn block v-bind="activatorProps" variant="text" style="justify-content: space-between">
        <template v-slot:prepend>
          <v-icon color="grey-darken-1" icon="mdi-account" :size="$props.iconSize" />
          <span class="text-grey-darken-1">&nbsp;Name</span>
        </template>
        <template v-slot:default>
          <span class="text-overflow">
            {{ `${props.firstName} ${props.lastName} ${props.family}` }}
          </span>
        </template>
      </v-btn>
    </template>

    <template v-slot:default>
      <v-form v-model="isValid">
        <v-card title="Редактирование имени">
          <v-card-text>
            <v-row no-gutters>
              <v-col>
                <v-text-field
                  v-model="firstName"
                  variant="underlined"
                  label="Имя"
                  :rules="nameRules"
                  :messages="$props.errorMessages ? $props.errorMessages?.get('FirstName') : ''"
                >
                </v-text-field>
              </v-col>
            </v-row>
            <v-row no-gutters>
              <v-col>
                <v-text-field
                  v-model="lastName"
                  variant="underlined"
                  label="Отчество/Второе имя"
                  :rules="nameRules"
                  :messages="$props.errorMessages ? $props.errorMessages?.get('LastName') : ''"
                >
                </v-text-field>
              </v-col>
            </v-row>
            <v-row no-gutters>
              <v-col>
                <v-text-field
                  v-model="family"
                  variant="underlined"
                  label="Фамилия"
                  :rules="nameRules"
                  :messages="$props.errorMessages ? $props.errorMessages?.get('Family') : ''"
                >
                </v-text-field>
              </v-col>
            </v-row>
          </v-card-text>
          <v-card-actions>
            <v-btn text="Отмена" variant="text" @click="resetDialog"> </v-btn>
            <v-btn
              text="Сохранить"
              variant="text"
              type="submit"
              :disabled="!isValid"
              @click="editUserName(firstName, lastName, family)"
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
  firstName: {
    type: String,
    requare: true
  },
  lastName: {
    type: String,
    requare: true
  },
  family: {
    type: String,
    requare: true
  },
  errorMessages: {
    type: Map<string, string>,
    requare: true
  },
  iconSize: {
    type: Number,
    default: 32
  }
});

const emit = defineEmits(['editUserName', 'resetDialog']);

const isValid = defineModel<boolean>('isValid', { required: true });
const isActive = defineModel<boolean>('isActive', { required: true });

const firstName = ref<string>(props.firstName as string);
const lastName = ref<string>(props.lastName as string);
const family = ref<string>(props.family as string);

const nameRules = [
  (value: string) => !!value || 'Поле обезаательно для заполнения',
  (value: string) => value.length >= 2 || 'Минимальная поля длина 2 символа',
  (value: string) =>
    /^(?!.*'{2})(?!.*’{2})([\p{L}]+(['’][\p{L}]+)*)$/u.test(value) ||
    'В поле должны быть только буквы'
];

const editUserName = (firstName: string, lastName: string, family: string): void => {
  emit('editUserName', firstName, lastName, family);
};

const resetDialog = (): void => {
  emit('resetDialog', 'NameDialog');
  firstName.value = props.firstName as string;
  lastName.value = props.lastName as string;
  family.value = props.family as string;
};
</script>

<style></style>

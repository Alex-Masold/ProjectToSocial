<template>
  <v-dialog width="auto" persistent>
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

    <template v-slot:default="{ isActive }">
      <v-form ref="form" v-model="valid">
        <v-card title="Редактирование имени">
          <v-card-text>
            <v-container class="ma-0 pa-0">
              <v-row no-gutters>
                <v-col>
                  <v-text-field
                    v-model="firstName"
                    :rules="nameRules"
                    variant="underlined"
                    label="Имя"
                  >
                    <template v-slot:message="{ message }">
                      <span>{{ message }}</span>
                    </template>
                  </v-text-field>
                </v-col>
              </v-row>
              <v-row no-gutters>
                <v-col>
                  <v-text-field
                    v-model="lastName"
                    :rules="nameRules"
                    variant="underlined"
                    label="Отчество/Второе имя"
                  >
                    <template v-slot:message="{ message }">
                      <span>{{ message }}</span>
                    </template>
                  </v-text-field>
                </v-col>
              </v-row>
              <v-row no-gutters>
                <v-col>
                  <v-text-field
                    v-model="family"
                    :rules="nameRules"
                    variant="underlined"
                    label="Фамилия"
                  >
                    <template v-slot:message="{ message }">
                      <span>{{ message }}</span>
                    </template>
                  </v-text-field>
                </v-col>
              </v-row>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-btn
              text="Отмена"
              variant="text"
              @click="reset(), (isActive.value = !isActive.value)"
            ></v-btn>
            <v-btn
              text="Сохранить"
              variant="text"
              @click="editUserName(firstName, lastName, family)"
              :disabled="!valid"
            ></v-btn>
          </v-card-actions>
        </v-card>
      </v-form>
    </template>
  </v-dialog>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';

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
  iconSize: {
    type: Number,
    default: 32
  }
});

const emit = defineEmits(['editUserName']);

const form = ref<HTMLFormElement>();

const firstName = ref<string>(props.firstName as string);
const lastName = ref<string>(props.lastName as string);
const family = ref<string>(props.family as string);

const valid = ref<boolean>();

const nameRules = [
  (value: string) => !!value || 'Поле обезаательно для заполнения',
  (value: string) => value.length >= 2 || 'Минимальная длина 2 символа',
  (value: string) =>
    /^(?!.*'{2})(?!.*’{2})([\p{L}]+(['’][\p{L}]+)*)$/u.test(value) ||
    'В имени должны быть только буквы',
  () =>
    firstName.value + lastName.value + family.value !==
      `${props.firstName}${props.lastName}${props.family}` || 'Измнения не были внесены'
];

watch([valid, firstName, lastName, family], () => {
  validate();
});

const editUserName = (firstName: string, lastName: string, family: string): void => {
  emit('editUserName', firstName, lastName, family);
};

const reset = (): void => {
  firstName.value = props.firstName as string;
  lastName.value = props.lastName as string;
  family.value = props.family as string;
};

const validate = (): void => {
  form.value?.validate();
};
</script>

<style></style>
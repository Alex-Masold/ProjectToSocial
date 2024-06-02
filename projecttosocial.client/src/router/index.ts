import AuthView from '@/views/AuthView.vue';
import UserView from '@/views/UserView.vue';
import TasksView from '@/views/TasksView.vue';

import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '@/views/HomeView.vue';
import ChatView from '@/views/ChatView.vue';
import MainView from '@/views/MainView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Auth',
      component: AuthView
    },
    {
      path: '/user',
      name: 'User',
      component: UserView,
      children: [
        {
          path: 'home',
          components: {
            default: MainView
          },
          children: [
            {
                path: '',
                name: 'Home',
                component: HomeView
            },
            {
            path: 'chat',
            name: 'Chat',
            component: ChatView
          }]
        },
        {
          path: 'tasks',
          name: 'Tasks',
          component: TasksView
        }
      ]
    }
  ]
});

export default router;

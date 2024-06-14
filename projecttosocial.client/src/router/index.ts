import AuthView from '@/views/AuthView.vue';
import UserView from '@/views/UserView.vue';
import TasksView from '@/views/TasksView.vue';

import { createRouter, createWebHistory } from 'vue-router';
import ChatView from '@/views/ChatView.vue';
import SearchView from '@/views/SearchView.vue';
import ChatsView from '@/views/ChatsView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Auth',
      component: AuthView
    },
    {
      path: '/:userId',
      component: UserView,
      children: [
        {
          path: 'chats',
          name: 'Chats',
          component: ChatsView,
          children: [
            {
                path: 'search',
                name: 'Search',
                component: SearchView
            },
            {
            path: ':chatId',
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

import { createRouter, createWebHistory } from 'vue-router';



const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Auth',
      component: () => import('@/views/AuthView.vue'),
    },
    {
      path: '/:userId',
      name: 'User',
      component: () => import('@/views/UserView.vue'),
      children: [
        {
          path: '',
          name : 'Chats',
          component: () => import('@/views/ChatsView.vue'),
          children: [
            {
                path: 'search',
                name: 'Search',
                component: () => import('@/views/SearchView.vue'),
            },
            {
            path: ':chatId',
            name: 'Chat',  
            component: () => import('@/views/ChatView.vue'),
          }]
        },
        {
          path: 'tasks',
          name: 'Tasks',
          component: () => import('@/views/TasksView.vue'),
        }
      ]
    }
  ]
});

export default router;

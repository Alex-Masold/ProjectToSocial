import AuthView from '@/views/AuthView.vue';
import TasksView from '@/views/TasksView.vue';

import { createRouter, createWebHistory } from 'vue-router';

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            name: 'Auth',
            component: AuthView
        },
        {
            path: '/Tasks',
            name: 'Tasks',
            component: TasksView
            }
    ]
});

export default router;

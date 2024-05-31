// Styles
import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'
import './assets/main.css'

import { createApp } from 'vue'

// Vuetify
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

import App from './App.vue'
import router from './router'


const vuetify = createVuetify({
    theme: {
        defaultTheme: 'dark'
      },
    components,
    directives,
  })

createApp(App).use(router).use(vuetify).mount('#app')

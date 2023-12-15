import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import store from '../store';
import chat from  '../chat'
import Toast from 'vue-toastification'
import 'vue-toastification/dist/index.css'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.bundle'
import '../src/styles.css';
import '@fortawesome/fontawesome-free/css/all.css'
createApp(App).use(store).use(chat).use(Toast).use(router).mount('#app');
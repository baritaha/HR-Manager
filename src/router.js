import { createRouter, createWebHashHistory } from 'vue-router';
import homePage from './views/homePage';
import EmployeePage from './views/EmployeePage.vue';
import DepartmentPage from './views/DepartmentPage.vue';
import RegistPage from './views/RegistPage.vue';
import LoginPage from './views/LoginPage.vue';
import EmployeesPageComponent from './views/EmployeesPage.vue';
import LoginManager from './views/LoginManager';
import ManagersPage from './views/ManagersPage';
import BackgroundLayout from './views/BackgroundLayout.vue';

const routes = [
 
  {
    
    path: '/',
    name: 'homePage',
    component: BackgroundLayout,
    children: [
      { path: '', component: homePage },
    ],
  },
 
  {
    path: '/homePage',
    name: 'homePage',
    component: homePage,
  },
  {
    path: '/EmployeePage',
    name: 'EmployeePage',
    component: EmployeePage,
    beforeEnter: (to, from, next) => {
      // Check username and password here
      const username = prompt('Enter Username:');
      const password = prompt('Enter Password:');

      // Replace 'Admin' and '0000' with your actual username and password
      if (username === 'Admin' && password === '0000') {
        // If the username and password are correct, allow access
        next();
      } else {
        // If the username and password are incorrect, prevent access
        alert('Authentication failed. Please enter valid credentials.');
        next(false);
      }
    },
  },
  {
    path: '/DepartmentPage',
    name: 'DepartmentPage',
    component: DepartmentPage,
    beforeEnter: (to, from, next) => {
      // Check username and password here
      const username = prompt('Enter Username:');
      const password = prompt('Enter Password:');

      // Replace 'Admin' and '0000' with your actual username and password
      if (username === 'Admin' && password === '0000') {
        // If the username and password are correct, allow access
        next();
      } else {
        // If the username and password are incorrect, prevent access
        alert('Authentication failed. Please enter valid credentials.');
        next(false);
      }
    },
  },
  {
    path: '/ManagersPage',
    name: 'ManagersPage',
    component: ManagersPage,
    meta: { requiresAuth: true },
 
  },
  {
    path: '/RegistPage',
    name: 'RegistPage',
    component: RegistPage,
  },
  {
    path: '/LoginManager',
    name: 'LoginManager',
    component: LoginManager,
  },
  {
    path: '/LoginPage',
    name: 'LoginPage',
    component: LoginPage,
  },
  {
    path: '/EmployeesPage',
    name: 'EmployeesPage',
    component: EmployeesPageComponent,
   
  },
 
];

const router = createRouter({
  history:  createWebHashHistory(),
  routes,
});

export default router;

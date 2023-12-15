<template>
    <div class="login">
      <h1>Login Manager</h1>
      
 
      <form @submit.prevent="loginManager">
        <div class="form-group">
          <div class="input-group">
            <div class="input-group-prepend">
              <span class="input-group-text"><i class="bi bi-person"></i></span>
            </div>
            <label for="username" class="sr-only"></label>
            <input
              type="text"
              id="username"
              v-model="DepartmentPart"
              class="form-control"
              placeholder="Username"
              required
            />
          </div>
        </div>
        <div class="form-group">
          <div class="input-group">
            <div class="input-group-prepend">
              <span class="input-group-text"><i class="bi bi-lock"></i></span>
            </div>
            <label for="password" class="sr-only"></label>
            <input
              type="password"
              id="password"
              v-model="DepPassword"
              class="form-control"
              placeholder="Password"
              required
            />
          </div>
        </div>
        <button type="submit" class="btn btn-primary">Login</button>
      </form>
    </div>
  </template>
 <script>
 const URL_API='http://localhost:5242/api/';
 import axios from 'axios';
 import { useToast } from "vue-toastification";
 import 'bootstrap-icons/font/bootstrap-icons.css';
 export default {
   data() {
     return {
       DepartmentPart: '',
       DepPassword: '',
       DepartmentId:0
     };
   },
   methods: {
    showWelcomeToast() {
      const toast = useToast();
      const toastOptions = {
        position: "top-right",
        timeout: 2000,
        containerClassNames: ["my-toast-container"],
        toastClassNames: ["my-toast"],
      };
      toast.success('Login Successfully Manager , '+this.DepartmentPart.toUpperCase(), toastOptions);
    },
    loginManager() {
  const Loginmanager = {
    DepartmentName: this.DepartmentPart,
    DepPassword: this.DepPassword,
  };
  axios
    .post(URL_API + 'Department/CheckLogin', Loginmanager)
    .then((response) => {
      const data = response.data;
      if (data.message === 'Login Successful') {
        localStorage.setItem('token1', data.token);
        this.showWelcomeToast();
        axios
          .get(URL_API + 'Department/' + this.DepartmentPart)
          .then((userDataResponse) => {
            const userData = userDataResponse.data;
            console.log("userData in ManagerLogin:", userData[0]);

            this.$router.push({ name: 'ManagersPage' });
          })
          .catch((userDataError) => {
            alert('Error fetching user data: ' + userDataError.message);
          });
      } else {
        alert('Login failed. Please enter valid credentials.');
      }
    })
    .catch((error) => {
      alert('Error logging in: ' + error.message);
    });
  }
   },
 };
 </script>
 <style scoped>
 h1{
  font-family:monospace;
  background-color: #e0e0e0;
 }
.my-toast-container .my-toast {
  background-color: blue;
}

  .my-toast-container {
  position: fixed;
  top: 20px;
  right: 20px;
  z-index: 9999;
}

.my-toast {
  background-color: blue !important; 
  color: white; 
  border-radius: 5px;
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.2); 
  max-width: 300px;
}
  .login {
    max-width: 400px;
    margin: 5% auto;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 5px;
  }
  .form-group {
    margin-bottom: 15px;
  }
</style>
  
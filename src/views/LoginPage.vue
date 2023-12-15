<template>
  <div class="login">
    <h1>Login Employee</h1>
    <form @submit.prevent="login">
      <div class="form-group">
        <div class="input-group">
          <div class="input-group-prepend">
            <span class="input-group-text"><i class="bi bi-person"></i></span>
          </div>
          <label for="username" class="sr-only"></label>
          <input
            type="text"
            id="username"
            v-model="this.EmployeeName"
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
            v-model="this.EmpPassword"
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
import { useToast } from 'vue-toastification';
const URL_API = "http://localhost:5242/api/";
import 'bootstrap-icons/font/bootstrap-icons.css';
import axios from 'axios';


export default {
  data() {
    return {
      EmployeeName: '',
      EmpPassword: '',
      EmployeeId:0,
      GetData:[],
      GenderType:''
    };
  },
  methods: {
    fetchGetDatabyEmployeeName() {
      axios
        .get(URL_API + `Employee/` + this.EmployeeName)
        .then((response) => {
          this.GetData = response.data;
          const Data = this.GetData;
          this.EmployeeId = Data[0].EmployeeId;
          this.GenderType=Data[0].GenderType;
          console.log("Data By EmployeeName:", this.EmployeeId);
          console.log("Gender Type:", this.GenderType);
        })
        .catch((error) => {
          console.error('Error fetching data by EmployeeName:', error);
        });
    },

    showWelcomeToast() {
      const toast = useToast();
      const toastOptions = {
        position: "top-right",
        timeout: 2000,
        containerClassNames: ["my-toast-container"],
        toastClassNames: ["my-toast"],
      };
      toast.success('Login Successfully Welcome, ' + this.EmployeeName.toUpperCase(), toastOptions);
    },

    async login() {
      this.fetchGetDatabyEmployeeName();

     
      await new Promise((resolve) => {
        setTimeout(resolve, 3000); 
      });

      const loginEmployee = {
        EmployeeName: this.EmployeeName,
        EmpPassword: this.EmpPassword,
        EmployeeId: this.EmployeeId,
        GenderType:this.GenderType,
      };

      try {
        const response = await axios.post(URL_API + 'Employee/api/Login', loginEmployee);
        const token = response.data.token;

        if (token) {
       
          localStorage.setItem('token', token);

      this.showWelcomeToast();
          this.$router.push({ name: 'EmployeesPage' });
        } else {
          alert('Login failed. Please enter valid credentials.');
        }
      } catch (error) {
        console.error('Login failed:', error);
      }
    },
  }
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

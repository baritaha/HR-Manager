<template>
    <div class="register">
      <h1>Employee Registration</h1>
      <form @submit.prevent="registerEmployee">
        <div class="form-group">
          <div class="input-group">
            <div class="input-group-prepend">
              <span class="input-group-text"><i class="bi bi-person"></i></span>
            </div>
            <label for="username" class="sr-only"></label>
            <input
              type="text"
              id="username"
              v-model="EmployeeName"
              class="form-control"
              placeholder="Username"
              required
            />
          </div>
        </div>
  
        <div class="form-group">
          <div class="input-group">
            <div class="input-group-prepend">
              <span class="input-group-text"><i class="bi bi-building"></i></span>
            </div>
            <label for="department" class="sr-only"></label>
            
                
                <select class="form-select" v-model="selectedDepartment">
                  <option value="" disabled>Select Department</option>
                  <option v-for="dep in AllDepartments" :value="dep.DepartmentName" :key="dep.DepartmentId">
                    {{ dep.DepartmentName }}
                  </option>
                </select>
          </div>
        </div>
  
        <div class="form-group">
          <div class="input-group">
            <div class="input-group-prepend">
              <span class="input-group-text"><i class="bi bi-calendar"></i></span>
            </div>
            <label for="dateOfJoining" class="sr-only"></label>
            <input
              type="date"
              id="dateOfJoining"
              v-model="DateOfJoining"
              class="form-control"
              placeholder="Date Of Joining"
              required
            />
          </div>
        </div>
  
        <div class="form-group">
          <div class="input-group">
            <div class="input-group-prepend">
              <span class="input-group-text"><i class="bi bi-file-earmark-image"></i></span>
            </div>
            <label for="photoFileName" class="sr-only"></label>
          
                <input type="file" @change="ImageUpload" />
             
          </div>
        </div>
        <div class="form-group">
          <div class="input-group">
            <div class="input-group-prepend">
              <span class="input-group-text"><i class="fa-solid fa-venus-mars"></i></span>
            </div>
            
           <select v-model="GenderType">
            <option value="" disabled selected>Select gender</option>
              <option value="Male">Male</option>
              <option value="Female">Female</option>
           </select>
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
              v-model="EmpPassword"
              class="form-control"
              placeholder="Password"
              required
            />
          </div>
        </div>
  
        <div class="form-group">
          <div class="input-group">
            <div class="input-group-prepend">
              <span class="input-group-text"><i class="bi bi-lock"></i></span>
            </div>
            <label for="confirmPassword" class="sr-only"></label>
            <input
              type="password"
              id="confirmPassword"
              v-model="EmpCPassword"
              class="form-control"
              placeholder="Confirm Password"
              required
            />
          </div>
        </div>
  
        <button type="submit" class="btn btn-primary">Register</button>
      </form>
    </div>
  </template>
  <script>
  import { useToast } from 'vue-toastification';
  import axios from 'axios';
  import 'bootstrap-icons/font/bootstrap-icons.css';

  const URL_API = "http://localhost:5242/api/";
  
  export default {
    data() {
      return {
        selectedDepartment: "",
        AllDepartments: [],
        EmployeeName: '',
        DepartmentPart:'',
        DateOfJoining:'',
        PhotoFileName:null,
        EmpPassword:'',
        EmpCPassword: '',
        DepartmentName:'',
        DepartmentId:0,
        EmployeeId:0,
        GenderType:''
      };
    },
    methods: {
        GetAllDepartment() {
        axios
          .get(URL_API + "Department")
          .then((response) => {
            this.AllDepartments = response.data;

          })
          .catch((error) => {
            console.error("Error fetching data:", error);
          });
      },
      registerEmployee() {
  if (this.EmpPassword !== this.EmpCPassword) {
    alert('Passwords do not match');
    return;
  }

  const newEmployee = {
    EmployeeName: this.EmployeeName,
    DepartmentPart: this.selectedDepartment,
    DateOfJoining: this.DateOfJoining,
    PhotoFileName: this.PhotoFileName,
    EmpPassword: this.EmpPassword,
    EmpCPassword: this.EmpCPassword,
    GenderType:this.GenderType,
  };

  axios
    .post(URL_API + 'Employee/api/Registing', newEmployee)
    .then((response) => {
      this.newEmployee = response.data;
      this.showWelcomeToast();
      this.EmployeeName = '';
      this.selectedDepartment = ''; 
      this.DateOfJoining = '';
      this.PhotoFileName = '';
      this.EmpPassword = '';
      this.EmpCPassword = '';
      this.GenderType='';
      this.$router.push({ name: 'LoginPage' });
    })
    .catch((error) => {
     
      alert('Error registering employee: ' + error.message);
    });
},

      ImageUpload(event) {
        const file = event.target.files[0];
  
        if (file) {
          const formData = new FormData();
          formData.append("file", file);
  
          axios.post(URL_API + "Employee/saveFile", formData).then((response) => {
            this.PhotoFileName = response.data;
          });
  
          const reader = new FileReader();
  
          reader.onload = (e) => {
            this.selectedImage = e.target.result;
          };
  
          reader.readAsDataURL(file);
        }
      },
      showWelcomeToast() {
      const toast = useToast();

      // Customize the toast options
      const toastOptions = {
        position: "top-center",
        timeout: 3000,
        containerClassNames: ["my-toast-container"],
        toastClassNames: ["my-toast"],
      };

      // Show the toast with your custom options
      toast.success('Registed Successfully !', toastOptions);
    },
    },
    mounted(){
        this.GetAllDepartment();
       
    }
  };
  </script>
  <style>
  h1{
    font-family:monospace;
  background-color: #e0e0e0;
  }
  label{
    border: 2px;
    
  }
</style>
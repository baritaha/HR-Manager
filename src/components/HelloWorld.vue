<template>
  <div class="x">
  <header>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/axios/1.5.0/axios.min.js">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-4bw+/aepP/YC94hEpVNVgiZdgIC5+VKNBQNGCHeKRQN+PtmoHDEXuppvnDJzQIu9" crossorigin="anonymous">
  </header>
  <body>
    <div id="app" class="container" >
      
        <h3 class="d-flex justify-content-center">
        </h3>
        <h5 class="d-flex justify-content-center">
           Employee Management Portal
        </h5>
        <nav class="navbar navbar-expand-sm bg-light navbar-dark">
            <ul class="navbar-nav">
               
                <li class="nav-item m-1">
                    <router-link class="btn btn-light btn-outline-primary" 
                    to="/homePage">home</router-link>
                </li>
             

                <li class="nav-item m-1">
                    <router-link class="btn btn-light btn-outline-primary" 
                    to="/DepartmentPage">Department</router-link>
                </li>
                <li class="nav-item m-1">
                    <router-link class="btn btn-light btn-outline-primary" 
                    to="/EmployeePage">Employee</router-link>
                </li>
                <li class="nav-item m-1">
                    <router-link class="btn btn-light btn-outline-primary" 
                    :to="`/EmployeesPage`">Employees Page</router-link>
                </li>
                <li class="nav-item m-1">
                    <router-link class="btn btn-light btn-outline-primary" 
                    :to="`/ManagersPage`">Managers Page</router-link>
                </li>
                <li class="nav-item m-1">
                    <router-link class="btn btn-light btn-outline-primary" 
                    to="/RegistPage">Regist</router-link>
                </li>
                <li class="nav-item m-1">
                    <router-link class="btn btn-light btn-outline-primary" 
                    to="/LoginManager">Login(Manager)</router-link>
                </li>
                <li class="nav-item m-1">
                    <router-link class="btn btn-light btn-outline-primary" 
                    to="/LoginPage">Login(Employee)</router-link>
                </li>
              
             </ul>
        </nav>
     
        <router-view></router-view>
     
   
      </div>
    </body>
  </div>
</template>

<script>
export default {
  name: 'HelloWorld',
  props: {
    msg: String
  },
  created() {
    const { EmployeeId } = this.$route.params;
    if (EmployeeId) {
      console.log('EmployeeId:', EmployeeId);
    }
  }
}
</script>


<style scoped>
h5{
  font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
  font-weight: bolder;
}
.container{
  background-image:url('../../public/reprter.png'); 
  min-height: 100vh; 
  background-size: cover;
  flex-direction: column;
}
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #5d42c0;
  font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
}


</style>

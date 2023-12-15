<template>
  <div class="title"> Manager Page</div>
    
      <div v-if="showImage" class="constant-image">
      <img src="../../public/pal.png" alt="Constant Image" />
    </div>
  <div class="ManagerPage">   
  <div>
    <h2>Welcome, <span>{{ this.DepartmentPart }} Manager</span>   <button type="button" class="btn btn-outline-primary" v-if="this.DepartmentPart" @click="logout" >logout</button></h2>
   </div>
   <h3>Vacation Requests</h3>
        <table class="table table-striped table-hover">
  <thead>
    <tr>
      <th>Employee Name</th>
      <th>Gender Type</th>
      <th>Start Date</th>
      <th>End Date</th>
      <th>Status</th>
      <th>Action</th>
    </tr>
  </thead>
  <tbody>
  
    <tr v-for="request in vacationRequests" :key="request.EmployeeId">
      <td>{{ request.EmployeeName }}</td>
      <td>{{ request.GenderType }}</td>
      <td>{{ request.StartDate }}</td>
      <td>{{ request.EndDate }}</td>
      <td :class="getStatusClass(request.Status)">{{ request.Status }}</td>
      <td>
        <button class="accbtn" @click="updateStatus(request.EmployeeId,'Accepted',request.StartDate,request.EndDate)" >Accept</button>
        <button class="ignbtn" @click="updateStatus(request.EmployeeId,'Ignored',request.StartDate,request.EndDate)" >Ignore</button>
        <button class="btn btn-outline-danger" @click="DeleteEmployee(request.EmployeeId,request.RequestId)" >Delete</button>
      </td>
    </tr>
  </tbody>
</table>
<h3>Departure Requests</h3>
        <table class="table table-striped table-hover">
  <thead>
    <tr>
      <th>Employee Name</th>
      <th>Start Date</th>
      <th>End Date</th>
      <th>Status</th>
      <th>Action</th>
    </tr>
  </thead>
  <tbody>
  
    <tr v-for="departure in DepartureRequest" :key="departure.EmployeeId">
      <td>{{ departure.EmployeeName }}</td>
      <td>{{ departure.StartHoure }}</td>
      <td>{{ departure.EndHoure }}</td>
      <td :class="getStatusClass(departure.Status)">{{ departure.Status }}</td>
      <td>
        <button class="accbtn" @click="updateStatusDep(departure.EmployeeId,'Accepted',departure.StartHoure,departure.EndHoure)" >Accept</button>
        <button class="ignbtn" @click="updateStatusDep(departure.EmployeeId,'Ignored',departure.StartHoure,departure.EndHoure)" >Ignore</button>
      </td>
    </tr>
  </tbody>
</table>
<h3>Suggestions from Employees</h3>
<table class="table table-striped table-hover">
  <thead>
    <tr>
      <th>Text Of Suggestions </th>
      <th>Employee Name</th>
      <th>Department Part</th>
    </tr>
  </thead>
  <tbody>
  
    <tr v-for="sugest in suggestionsStore" :key="sugest.EmployeeId">
      <td>{{ sugest.SuggestText }}</td>
      <td>{{ sugest.EmployeeName }}</td>
      <td>{{ sugest.DepartmentPart }}</td>
    </tr>
  </tbody>
</table>




        </div>
       
</template>
<script>
const URL_API = "http://localhost:5242/api/";
import axios from 'axios';

export default {
data(){
    return {
    vacationRequests:[],
    DepartureRequest:[],
    DepartmentId:null ,
DepartmentPart: '',
showImage: false,
suggestionsStore:[],
EmployeeId:0,
VacationRequestId:false

   
}
},
methods:{
  DeleteEmployee(EmployeeId,RequestId) {

        if (!confirm("Are You Sure")) {
          return;
        }
        console.log("Id = "+EmployeeId);
        
        axios.delete(URL_API + `Employee/DeleteRequest?EmployeeId=${EmployeeId}&RequestId=${RequestId}`)
        .then((response) => {
          this.fetchVacationRequests();
          alert(response.data);
        });
      },
  fetchSuggestions() {
     
     axios
       .get(URL_API + `Employee/GetAllSuggestions?DepartmentPart=${this.DepartmentPart}`)
       
       .then((response) => {
         this.suggestionsStore = response.data;
         console.log("Sugestions :"+this.suggestionsStore);
         console.log("Dep in suggestions = "+this.DepartmentPart);
         
       })
       
       .catch((error) => {
         console.error('Error fetching vacation requests:', error);
       });
       
   },
    logout(){
      this.showImage = true;
      setTimeout(() => {
        this.showImage = false;
        localStorage.removeItem('token1');
        this.$router.push({ name: 'LoginManager' });
      }, 1000);
  

   },
    getStatusClass(stat) {
        console.log('Status:', stat);
    switch (stat) {
      case 'Pending':
        return 'status-pending'; 
      case 'Accepted':
        return 'status-accepted';
      case 'Ignored':
        return 'status-ignored'; 
      default:
        return ''; 
    }
  },
      updateStatus(employeeId, newStatus,startdate,enddate) {
            axios.post(URL_API+ 'Employee/UpdateStatus', {
                EmployeeId: employeeId,
                Status: newStatus,
                StartDate:startdate,
                EndDate:enddate


            })
            .then(response => {
                console.log(response.data);
                this.fetchVacationRequests();
            })
            .catch(error => {
                console.error(error);
            });
        },
        updateStatusDep(employeeId, newStatus,starthoure,endhoure) {
            axios.post(URL_API+ 'Employee/UpdateStatusDep', {
                EmployeeId: employeeId,
                Status: newStatus,
                StartHoure:starthoure,
                EndHoure:endhoure


            })
            .then(response => {
                console.log(response.data);
                this.fetchDepartureRequests();
            })
            .catch(error => {
                console.error(error);
            });
        },
      fetchVacationRequests() {
     
    axios
      .get(URL_API + `Employee/GetVacationbyDepName?DepartmentPart=${this.DepartmentPart}`)
      
      .then((response) => {
        this.vacationRequests = response.data;
        console.log("vacation :"+this.vacationRequests);
        console.log("Dep = "+this.DepartmentPart);
        
      })
      
      .catch((error) => {
        console.error('Error fetching vacation requests:', error);
      });
      
  },
  fetchDepartureRequests() {
     
     axios
       .get(URL_API + `Employee/GetDeparturebyDepName?DepartmentPart=${this.DepartmentPart}`)
       
       .then((response) => {
         this.DepartureRequest = response.data;
         console.log("vacation :"+this.DepartureRequest);
         console.log("Dep = "+this.DepartmentPart);
         
       })
       
       .catch((error) => {
         console.error('Error fetching vacation requests:', error);
       });
       console.log("Deparment after get Request : "+this.DepartmentPart )
   },
},
mounted(){
  

    
},
created(){
  
    const token = localStorage.getItem('token1');

if (!token) {

  alert('You are not logged in. Please login to access this page.');
  this.$router.push({ name: 'LoginManager' });
} else {
  // User is authenticated, fetch data and proceed
  const jwtPayload = JSON.parse(atob(token.split(".")[1]));
  this.DepartmentPart = jwtPayload.DepartmentName;
  console.log("Deparment from token : "+this.DepartmentPart );
  this.fetchVacationRequests();
    this.fetchDepartureRequests();
    this.fetchSuggestions();
}
}
}
</script>
<style scoped>
.title{
  font-family: monospace;
  font-weight: bolder;
  font-size: 30px;
}
h2{
  font-family:monospace;
  background-color: #e0e0e0;
}
span{
  
}
.ManagerPage{
  padding-top: 20px;
  font-family:monospace;
  font-size: 20px;
  font-weight: bolder;
  
}
.accbtn{
    background-color: lightgreen;
  }
  .ignbtn{
    background-color: lightcoral;
  }
  .status-pending{
    background-color: gray;
  }
  .status-accepted{
    background-color: greenyellow;
  }
  .status-ignored{
    background-color: red;
  }
  h3{
    background-color:cornflowerblue;
    color: blue;
  }
</style>

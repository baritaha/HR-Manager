<template>
  <div class="EmpReq">
    <div class="form-group">
        <div class="input-group">
          <textarea v-model="text" class="form-control" rows="3" placeholder="Send Suggestion For Manager"></textarea>
          <div class="input-group-append">
            <button class="btn btn-outline-primary" type="button" @click="sendSuggestion">Send Suggestion</button>
          </div>
        </div>
      </div>
    <div v-if="showImage" class="constant-image">
      <img src="../../public/qds.png" alt="Constant Image" />
    </div>
    <div class="employee-info">
      <div class="employee-photo">
        <div v-if="!this.EmployeeId">Loading...</div>
        <h2 class="navbar navbar-expand-sm bg-light navbar-dark">Welcome, <span>{{ this.EmployeeName.toLocaleUpperCase() }}<em>!</em></span>  <button type="button" class="btn btn-outline-primary" v-if="this.EmployeeId" @click="logout" >logout</button></h2>
        <img :src="getImageUrl(this.PhotoFileName)" alt="Employee Photo" class="Employee-Photo" />
        
      </div>
      
    </div>
    <div>
      
      
    </div>
   
    <h1>Your Manager : <span>{{ this.DepartmentPart }}</span></h1>
    
    <button type="button" @click="showForm = !showForm" class="btn btn-primary" :class="{ 'red-background': showForm }">
      Request Vacation
    </button>
    <button type="button" @click="showForm4 = !showForm4" class="btn btn-primary" :class="{ 'red-background': showForm4 }">
      Request Departures
    </button>
    <button type="button" @click="showForm2 = !showForm2" class="btn btn-primary" :class="{ 'red-background': showForm2 }" >
      Your Vacations
    </button>
    <button type="button" @click="showForm3 = !showForm3" class="btn btn-primary" :class="{ 'red-background': showForm3 }">
      Your Departures
    </button>
    <form v-if="showForm"  @submit.prevent="showConfirmation=true">
      <h2>Request Vacation</h2>
      <div class="input-group">
        <span class="input-group-text">Start Date</span>
        <input type="date" class="form-control" v-model="StartDate" />
      </div>
      <div class="input-group">
        <span class="input-group-text">End Date</span>
        <input type="date" class="form-control" v-model="EndDate" />
      </div>
      <button type="submit" class="btn btn-primary">Request Now</button>
    </form>
    <div v-if="showConfirmation" class="modal-overlay">
      <div class="custom-modal">
        <h2>Confirm Vacation Request</h2>
        <p>Are you sure you want to request vacation from( {{ StartDate }} ) to ( {{ EndDate }} ) ?</p>
        <button @click="sendRequest">Yes</button>
        <button @click="showConfirmation = false">No</button>
      </div>
    </div>
    
    <form v-if="showForm4"  @submit.prevent="showConfirmation2=true">
      <h1>Request Departure</h1>
      <div class="input-group">
        <span class="input-group-text">Start Houre</span>
        <input type="datetime-local" class="form-control" v-model="StartHoure" />
      </div>
      <div class="input-group">
        <span class="input-group-text">End Houre</span>
        <input type="datetime-local" class="form-control" v-model="EndHoure" />
      </div>
      <button type="submit" class="btn btn-primary">Request Now</button>
    </form>
    <div v-if="showConfirmation2" class="modal-overlay">
      <div class="custom-modal">
        <h1>Confirm Departure Request</h1>
        <p>Are you sure you want to request Departure from( {{ StartHoure }} ) to ( {{ EndHoure }} ) ?</p>
        <button @click="sendRequestDep">Yes</button>
        <button @click="showConfirmation2 = false">No</button>
      </div>
    </div>
    
    <div v-if="showForm2">
      <h1>your Vacation</h1>
      <table class="table table-striped table-hover">
        <thead>
          <tr >
            <th>Start Date</th>
            <th>End Date</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="vac in employeeVacations" :key="vac.EmployeeId">
            <td>{{ vac.StartDate }}</td>
            <td>{{ vac.EndDate }}</td>
            <td :class="getStatusClass(vac.Status)">{{ vac.Status }}</td>
          </tr>
        </tbody>
      </table>
    </div>
    
    <div v-if="showForm3">
      <h1>your Departure</h1>
      <table class="table table-striped table-hover">
        <thead>
          <tr >
            <th>From the hour</th>
            <th>To the hour</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(dep,counter) in employeeDepartures" :key="dep.EmployeeId">
            <td>{{ ++counter }}- {{ dep.StartHoure }}</td>
            <td>{{ ++counter }}-{{ dep.EndHoure }}</td>
            <td :class="getStatusClass(dep.Status)">{{ dep.Status }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
 
</template>

<script>
import { useToast } from "vue-toastification";
import axios from 'axios';
const URL_API = 'http://localhost:5242/api/';

export default {
  data() {
    return {
      StartDate: null,
      EndDate: null,
      confirmationStatus: null,
      showConfirmation: false,
      showConfirmation2: false,
      showForm: false,
      showForm2: false,
      showForm3: false,
      showForm4: false,
      Status:'Pending',
      PhohtoPath: "http://localhost:5242/Photos/",
      employeeVacations:[],
      employeeDepartures:[],
      StartHoure:null,
      EndHoure:null,
      EmployeeId: null,
      EmployeeName: '',
      PhotoFileName: '',
      DepartmentPart: '',
      loading: true,
      showImage: false,
      counter:0,
      text:'',
      GenderType:''
    };
  },
  methods: {
   
sendSuggestion() {
  const suggestionData = {
    SuggestText: this.text ,
    EmployeeId:this.EmployeeId,
    EmployeeName:this.EmployeeName,
    DepartmentPart:this.DepartmentPart
    
  };

  axios
    .post(URL_API+'Employee/AddSuggestion', suggestionData)
    .then(response => {
    this.showWelcomeToast4();
      console.log(response.data);
    })
    .catch(error => {
      
      console.error(error);
    });
},

    logout() {
      this.showImage = true;
      setTimeout(() => {
        this.showImage = false;
        localStorage.removeItem('token');
        this.$router.push({ name: 'LoginPage' });
      }, 1000); 
    },
    fetchEmployeeVacations() {
 
    
      axios
        .get(URL_API + `Employee/GetEmployeeVacations?EmployeeId=${this.EmployeeId}`)
        .then((response) => {
          this.employeeVacations = response.data;
          this.EmployeeName = response.data[0].EmployeeName;
          this.DepartmentPart = response.data[0].DepartmentPart;
        })
        .catch((error) => {
          console.error('Error fetching employee vacations: ', error);
        });
        this.fetchGetDatabyEmployeeName();
    },
    fetchEmployeeDepartures() {
 
    
 axios
   .get(URL_API + `Employee/GetEmployeeDeparture?EmployeeId=${this.EmployeeId}`)
   .then((response) => {
     this.employeeDepartures = response.data;
     this.EmployeeName = response.data[0].EmployeeName;
     this.DepartmentPart = response.data[0].DepartmentPart;
   })
   .catch((error) => {
     console.error('Error fetching employee Departure: ', error);
   });
   this.fetchGetDatabyEmployeeName();
},
    fetchGetDatabyEmployeeName() {
      axios
        .get(URL_API + `Employee/` + this.EmployeeName)
        .then((response) => {
          this.GetData = response.data;
          const Data = this.GetData;
          this.PhotoFileName = Data[0].PhotoFileName;

          console.log("Data By PhotoFileName:", this.PhotoFileName);
        })
        .catch((error) => {
          console.error('Error fetching data by EmployeeName:', error);
        });
    },
    getImageUrl(photoFileName) {
      return this.PhohtoPath + photoFileName;
    },
    sendRequest() {
      if (this.StartDate >= this.EndDate) {
        alert('Invalid date range for vacation request.');
        return;
      }
      const vacationRequest = {
        EmployeeId: this.EmployeeId,
        StartDate: this.StartDate,
        EndDate: this.EndDate,
        Status: 'Pending',
        EmployeeName: this.EmployeeName,
        DepartmentPart: this.DepartmentPart,
        GenderType:this.GenderType,
      };
      console.log("Vacations Request : "+vacationRequest.data);
      console.log("id is : " + this.EmployeeId);
      console.log("status : " + this.Status);
      axios
        .post(URL_API + 'Employee/api/RequestVacation', vacationRequest)
        .then((response) => {
          if (response.data) {
            this.confirmationStatus = 'requested';
            this.showConfirmation = false;
            this.showWelcomeToast2();
            this.StartDate = null;
            this.EndDate = null;
          } else {
            alert('Unexpected response from the server.' + response.data);
          }
        })
        .catch(() => {
          alert('The Period Time Of Vacation you Choosen is Taken , Choose another Time');
        });
    },
    sendRequestDep() {
      if (this.StartHoure > this.EndHoure) {
        alert('Invalid date range for Departure request.');
        return;
      }
      const DepartureRequest = {
        EmployeeId: this.EmployeeId,
        StartHoure: this.StartHoure,
        EndHoure: this.EndHoure,
        Status: 'Pending',
        EmployeeName: this.EmployeeName,
        DepartmentPart: this.DepartmentPart,
      };
      console.log("Departure Request : "+DepartureRequest.data);
      console.log("id is : " + this.EmployeeId);
      console.log("status : " + this.Status);
      axios
        .post(URL_API + 'Employee/api/RequestDepartures', DepartureRequest)
        .then((response) => {
          if (response.data) {
            this.confirmationStatus = 'requested';
            this.showConfirmation2 = false;
            this.showWelcomeToast3();
            this.StartHoure = null;
            this.EndHoure = null;
          } else {
            alert('Unexpected response from the server.' + response.data);
          }
        })
        .catch(() => {
          alert('The Period Time Of Departure you Choosen is Taken , Choose another Time');
        });
    },
    getStatusClass(status) {
      switch (status) {
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
    showWelcomeToast() {
      const toast = useToast();
      const toastOptions = {
        position: "top-right",
        timeout: 5000,
        containerClassNames: ["my-toast-container"],
        toastClassNames: ["my-toast"],
      };
      toast.success(`Welcome, ${this.EmployeeName}!`, toastOptions);
    },
    showWelcomeToast2() {
      const toast = useToast();
      const toastOptions = {
        position: "top-center",
        timeout: 5000,
        containerClassNames: ["my-toast-container"],
        toastClassNames: ["my-toast"],
      };
      toast.success('Sen Request Vacation Successfully !', toastOptions);
    },
    showWelcomeToast3() {
      const toast = useToast();
      const toastOptions = {
        position: "top-center",
        timeout: 5000,
        containerClassNames: ["my-toast-container"],
        toastClassNames: ["my-toast"],
      };
      toast.success('Sen Request Departure Successfully !', toastOptions);
    },
    showWelcomeToast4() {
      const toast = useToast();
      const toastOptions = {
        position: "top-center",
        timeout: 5000,
        containerClassNames: ["my-toast-container"],
        toastClassNames: ["my-toast"],
      };
      toast.success('Send Suggestion Successfully !', toastOptions);
    },
  },
  showImageFor3Seconds() {
    return new Promise((resolve) => {
      const imageSource = '/#200ae9';
      const displayDuration = 3000;
      this.displayedImage = imageSource; 
      setTimeout(() => {
        this.displayedImage = '';
        resolve();
      }, displayDuration);
    });
  },
  created() {
    const token = localStorage.getItem('token');

if (!token) {

  alert('You are not logged in. Please login to access this page.');
  this.$router.push({ name: 'LoginPage' });
} else {

  const jwtPayload = JSON.parse(atob(token.split(".")[1]));
  this.EmployeeId = jwtPayload.EmployeeId;
  this.EmployeeName = jwtPayload.EmployeeName;
  this.GenderType=jwtPayload.GenderType;
  console.log("EmployeeId from token is: " + this.EmployeeId);
      console.log("EmployeeName from token is: " + this.EmployeeName.toLocaleUpperCase());
      console.log("GenderType from token is: " + this.GenderType);
      
  this.fetchEmployeeVacations();
  this.fetchEmployeeDepartures();
}

  },
  mounted() {
 
  },


};
</script>

<style scoped>
h2{
  font-family:monospace;
  font-weight: bolder;
}
.my-toast-container {
  position: fixed;
  top: 20px;
  right: 20px;
  z-index: 9999;
}

.my-toast {
  background-color: #200ae9; 
  color: white; 
  border-radius: 5px;
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.2); 
  max-width: 300px;
}
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal {
  background-color: aqua;
  padding: 20px;
  border-radius: 5px;
  text-align: center;
}

.Employee-Photo {
  transition: transform 0.3s ease-in-out;
  max-width: 200px;
  max-height: 200px;
  cursor: pointer;
  border-radius: 20%;
  border: 2px solid #ccc;
}

.employee-info {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.employee-photo {
  margin-right: 20px; 
}
.employee-info textarea{
  text-align: center;
 margin-right:400px ;
 padding-right:400px ;
 margin-bottom: 120px;
}
.EmpReq .employee-info .sugg button{
 

}
.employee-details {
  flex-grow: 1;
}
.custom-modal {
  background-color: white;
  padding: 20px;
  border-radius: 5px;
  text-align: center;
  position: absolute; 
  top: 50%; 
  left: 50%; 
  transform: translate(-50%, -50%);
}
.showForm2,h1{
  font-family:'Courier New', Courier, monospace;
}
.showForm2,h1 span{
  background-color:cornflowerblue;
  font-weight: bold;
  color: cyan;
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
  .red-background {
    background-color: red; /* Add this class for the red background */
  }
  .EmpReq{
    padding-top: 20px;
  font-family:monospace;
  font-size: 20px;;
  font-weight: bolder;

  }
  em{
    color: palevioletred;
  }
  thead tr th{
    background-color:cornflowerblue;
  }
  .input-group span{
    background-color: cornflowerblue;
  }
  .underlined-text{
    text-decoration: underline;
  }
</style>

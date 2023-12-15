<template>
    <div class="employee">
      <h1>Employee Page</h1>
    
    <div class="d-flex justify-content-start">
      <button
        type="button"
        class="btn btn-outline-primary"
        data-bs-toggle="modal"
        data-bs-target="#exampleModal"
        @click="AddClick()"
      >
        Add Employee
      </button>
    </div>
    <h2>All Employees</h2>
    <table class="table table-striped table-hover">
      <thead>
        <tr>
          <th>Employee Name</th>
          <th>Manager</th>
          <th>Date Of Joining</th>
          <th>Profile Picture</th>
          <th>Options</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="emp in Employees" :key="emp.EmployeeId">
          <td>{{ emp.EmployeeName }}</td>
          <td>{{ emp.DepartmentPart }}</td>
          <td>{{ emp.DateOfJoining }}</td>
          <td>
            <div @mouseover="enlargeImage(emp.PhotoFileName)" @mouseout="resetImageSize">
                <img :src="getFullImagePath(emp.PhotoFileName)" alt="Employee Photo" class="employee-photo" />
            </div>
        </td>
          <td>
            <button
              type="button"
              data-bs-toggle="modal"
              data-bs-target="#exampleModal"
              @click="editClick(emp)"
              class="btn btn-outline-primary mr-1"
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="16"
                height="16"
                fill="currentColor"
                class="bi bi-pencil"
                viewBox="0 0 16 16"
              >
                <path
                  d="M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168l10-10zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207 11.207 2.5zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293l6.5-6.5zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325z"
                />
              </svg>
            </button>
            <button
              type="button"
              @click="DeleteEmployee(emp.EmployeeId)"
              class="btn btn-outline-danger"
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="16"
                height="16"
                fill="currentColor"
                class="bi bi-trash3-fill"
                viewBox="0 0 16 16"
              >
                <path
                  d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a 2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5Zm-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5ZM4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06Zm6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528ZM8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5Z"
                />
              </svg>
            </button>
          </td>
        </tr>
      </tbody>
    </table>
    <h2>Vacation Requests</h2>
<table class="table table-striped table-hover">
  <thead>
    <tr>
      <th>Employee Name</th>
      <th>Start Date</th>
      <th>End Date</th>
      <th>Status</th>
      <th>Manager</th>
      <th>Action</th>
    </tr>
  </thead>
  <tbody>
  
    <tr v-for="request in vacationRequests" :key="request.EmployeeId">
      <td>{{ request.EmployeeName }}</td>
      <td>{{ request.StartDate }}</td>
      <td>{{ request.EndDate }}</td>
      <td :class="getStatusClass(request.Status)">{{ request.Status }}</td>
      <td>{{ request.DepartmentPart }}</td>
      <td>
        <button class="accbtn" @click="updateStatus(request.EmployeeId,'Accepted',request.StartDate,request.EndDate)" >Accept</button>
        <button class="ignbtn" @click="updateStatus(request.EmployeeId,'Ignored',request.StartDate,request.EndDate)" >Ignore</button>
      </td>
    </tr>
  </tbody>
</table>
<h2>Departure Requests</h2>
<table class="table table-striped table-hover">
  <thead>
    <tr>
      <th>Employee Name</th>
      <th>Start Houre</th>
      <th>End Houre</th>
      <th>Status</th>
      <th>Manager</th>
      <th>Action</th>
    </tr>
  </thead>
  <tbody>
  
    <tr v-for="Departure in DepartureRequests" :key="Departure.EmployeeId">
      <td>{{ Departure.EmployeeName }}</td>
      <td>{{ Departure.StartHoure }}</td>
      <td>{{ Departure.EndHoure }}</td>
      <td :class="getStatusClass(Departure.Status)">{{ Departure.Status }}</td>
      <td>{{ Departure.DepartmentPart }}</td>
      <td>
        <button class="accbtn" @click="updateStatusDep(Departure.EmployeeId,'Accepted',Departure.StartHoure,Departure.EndHoure)" >Accept</button>
        <button class="ignbtn" @click="updateStatusDep(Departure.EmployeeId,'Ignored',Departure.StartHoure,Departure.EndHoure)" >Ignore</button>
      </td>
    </tr>
  </tbody>
</table>
    <div
      v-if="showEnlarged"
      class="enlarged-image"
      @mouseenter="showEnlargedImage"
      @mouseleave="hideEnlargedImage"
    >
      <img :src="enlargedImageSrc" alt="Enlarged Employee Photo" />
    </div>
  
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLable" aria-hidden="true">
      <div class="modal-dialog modal-lg modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLable">{{ ModalTitle }}</h5>
            <button type="button" claas="btn-close" data-bs-dismiss="modal" aria-label="close"></button>
          </div>
          <div class="modal-body">
            <div class="input-group nb-3">
              <div class="input-group">
                <span class="input-group-text">Employee Name</span>
                <input type="text" class="form-control" v-model="EmployeeName" />
              </div>
              <div class="input-group">
                <span class="input-group-text">Department Part</span>
                <select class="form-select" v-model="selectedDepartment">
                  <option value="" disabled>Select Department</option>
                  <option v-for="dep in AllDepartments" :value="dep.DepartmentName" :key="dep.DepartmentId">
                    {{ dep.DepartmentName }}
                  </option>
                </select>
              </div>
  
              <div class="input-group">
                <span class="input-group-text">Date Of Joining</span>
                <input type="date" class="form-control" v-model="DateOfJoining" />
              </div>
              <div class="input-group">
                <span class="input-group-text">Password</span>
                <input type="text" class="form-control" v-model="EmpPassword" />
              </div>
              <div class="input-group">
                <span class="input-group-text">Confirm Password</span>
                <input type="text" class="form-control" v-model="EmpCPassword" />
              </div>
              <div class="input-group">
                <span class="input-group-text">Photo File</span>
                <input type="file" @change="ImageUpload" />
              </div>
  
             
              <div class="input-group mt-3">
                <span class="input-group-text">Selected Image</span>
                <img :src="selectedImage" alt="Selected Image" />
              </div>
            </div>
            <button type="button" @click="AddEmployee()" v-if="EmployeeId == 0" class="btn btn-primary">Create</button>
            <button type="button" @click="EditEmployee()" v-if="EmployeeId != 0" class="btn btn-primary">Update</button>
          </div>
        </div>
      </div>
    </div>
  </div>
  </template>
  
  <script>
  import axios from "axios";
  const URL_API = "http://localhost:5242/api/";
  export default {
    props: [],
    data() {
      return {
        AllDepartments: [],
        Employees: [],
        ModalTitle: "",
        EmployeeId: 0,
        EmployeeName: "",
        DepartmentPart: "IT",
        DateOfJoining: "",
        PhotoFileName: "",
        DepartmentId: 0,
        PhohtoPath: "http://localhost:5242/Photos/",
        DepartmentName: "",
        selectedDepartment: "",
        selectedImage: "",
        showEnlarged: false,
        enlargedImageSrc: "",
        EmpPassword:"",
        EmpCPassword:"",
        vacationRequests:[],
        DepartureRequests:[]

      };
    },
    methods: {
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
      .get(URL_API + 'Employee/GetRequests')
      .then((response) => {
        this.vacationRequests = response.data;
        console.log("vacation :"+this.vacationRequests);
        
      })
      .catch((error) => {
        console.error('Error fetching vacation requests:', error);
      });
  },
  fetchDepartureRequests() {
    axios
      .get(URL_API + 'Employee/GetRequestsDep')
      .then((response) => {
        this.DepartureRequests = response.data;
        console.log("Departure :"+this.DepartureRequests);
        
      })
      .catch((error) => {
        console.error('Error fetching Departure requests:', error);
      });
  },
        enlargeImage(photoFileName) {
            const imageElement = document.querySelector(`img[src="${this.getFullImagePath(photoFileName)}"]`);
            if (imageElement) {
                imageElement.style.transform = 'scale(5)';
                imageElement.style.transition = 'transform 0.3s ease-in-out';
            }
        },

        resetImageSize(event) {
            event.target.style.transform = 'scale(1)';
        },
      getFullImagePath(relativePath) {
        return `${this.PhohtoPath}${relativePath}`;
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
  
      showEnlargedImage(imageSrc) {
        this.enlargedImageSrc = this.getFullImagePath(imageSrc);
        this.showEnlarged = true;
      },
  
      hideEnlargedImage() {
        this.showEnlarged = false;
      },
  
      GetAllDepartment() {
        axios
          .get(URL_API + "Department")
          .then((response) => {
            this.AllDepartments = response.data;
          
        console.log('EmployeeId:', this.EmployeeId);
          })
          .catch((error) => {
            console.error("Error fetching data:", error);
          });
      },
      refreshData() {
        axios
          .get(URL_API + "Employee")
          .then((response) => {
            this.Employees = response.data;
          })
          .catch((error) => {
            console.error("Error fetching data:", error);
          });
      },
      AddClick() {
        this.ModalTitle = "Add Employee";
        this.EmployeeId = 0;
        this.EmployeeName = "";
        this.DepartmentPart = "";
        this.DateOfJoining = "";
        this.PhotoFileName = "";
        this.selectedImage = "";
        this.EmpPassword="";
        this.EmpCPassword="";
      },
      editClick(emp) {
        this.ModalTitle = "Edit Employee";
        this.EmployeeId = emp.EmployeeId;
        this.EmployeeName = emp.EmployeeName;
        this.DepartmentPart = emp.selectedDepartment;
        this.DateOfJoining = emp.DateOfJoining;
        this.PhotoFileName = emp.PhotoFileName;
        this.EmpPassword=emp.EmpPassword;
        this.EmpCPassword=emp.EmpCPassword;
      },
      AddEmployee() {
        axios
          .post(URL_API + "Employee/api/Registing", {
            EmployeeId: this.EmployeeId,
            EmployeeName: this.EmployeeName,
            DepartmentPart: this.selectedDepartment,
            DateOfJoining: this.DateOfJoining,
            PhotoFileName: this.PhotoFileName,
            EmpPassword: this.EmpPassword,
            EmpCPassword:this.EmpCPassword,
            
          })
          .then((response) => {
            this.refreshData();
            alert(response.data);
          });
      },
      EditEmployee() {
        axios
          .put(URL_API + "Employee", {
            EmployeeId: this.EmployeeId,
            EmployeeName: this.EmployeeName,
            DepartmentPart: this.selectedDepartment,
            DateOfJoining: this.DateOfJoining,
            PhotoFileName: this.PhotoFileName,
            EmpPassword: this.EmpPassword,
            EmpCPassword:this.EmpCPassword
          })
          .then((response) => {
            this.refreshData();
            alert(response.data);
          });
      },
      DeleteEmployee(id) {
        if (!confirm("Are You Sure")) {
          return;
        }
        axios.delete(URL_API + "Employee/" + id).then((response) => {
          this.refreshData();
          alert(response.data);
        });
      },
      imageUpload(event) {
        let formData = new FormData();
        formData.append("file", event.target.files[0]);
        axios
          .post(URL_API + "Employee/saveFile", formData)
          .then((response) => {
            this.PhotoFileName = response.data;
          });
      },
    },
    mounted() {
      this.refreshData();
      this.GetAllDepartment();
      this.fetchVacationRequests();
      this.fetchDepartureRequests();
    },
  };
  </script>
  
  <style scoped>
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
  .employee-photo {
    transition: transform 0.3s ease-in-out;
    max-width: 100px; 
    max-height: 100px; 
    cursor: pointer;
    border-radius: 20%; 
  border: 2px solid #ccc;
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
  h1 {
   
    font-family:monospace;
  }
  
  .enlarge-image-container {
    position: relative;
  }
  
  .enlarge-image {
    display: none;
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.7);
    display: flex;
    justify-content: center;
    align-items: center;
    cursor: pointer;
    z-index: 9999;
    max-width: 90%;
    max-height: 90%;
  }
  
  .enlarge-image img {
    max-width: 100%;
    max-height: 100%;
  }
  th{
    border-radius: 20%; 
  border: 2px solid aqua;
  }
  th:hover{
    background-color: aquamarine;
  }
  .table-hover tbody tr:hover td {
    background-color:aqua; 
}
h2{
  background-color: burlywood;
}
  </style>
  
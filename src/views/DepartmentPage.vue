<template>
    <div class="home">
        <h1>Department Page</h1> 
    </div>
    
   
    <div class="d-flex justify-content-start"> 
    <button
      type="button"
      class="btn btn-outline-primary"
      data-bs-toggle="modal"
      data-bs-target="#exampleModal"
      @click="AddClick()"
    >
      Add Department
    </button>
  </div>
   
    

    <table class="table table-striped">
        <thead>
            <div class="input-group">
    <span class="input-group-text">ID</span>
    <input
      type="text"
      class="form-control"
      v-model="searchId"
      placeholder="Search..."
      @input="filterDepartments"
    />
 
  
    <span class="input-group-text">Department Name</span>
    <input
      type="text"
      class="form-control"
      v-model="searchDepartmentName"
      placeholder="Search..."
      @input="filterDepartments"
    />
  </div>

            <tr>
                <th>Department ID</th>
                <th>Department Name</th>
                <th>Department Password</th>
                <th>Options</th>
            </tr>
            </thead>
            <tbody>
                <tr v-for="dep in Departments" :key="dep.DepartmentId" >
                    <td>{{ dep.DepartmentId }}</td>
                    <td>{{ dep.DepartmentName }}</td>
                    <td>{{ dep.DepPassword }}</td>
                    <td><button type="button" 
                        data-bs-toggle="modal"
                         data-bs-target="#exampleModal"
                         @click="editClick(dep)"
                        class="btn btn-outline-primary mr-1">
                          <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil" viewBox="0 0 16 16">
                                  <path d="M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168l10-10zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207 11.207 2.5zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293l6.5-6.5zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325z"/>
                          </svg>

                    </button>
                    <button type="button" @click="DeleteDeparment(dep.DepartmentId)"
                    class="btn btn-outline-danger">
                             <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">
                                     <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5Zm-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5ZM4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06Zm6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528ZM8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5Z"/>
                            </svg>

                    </button>
                    </td>
                </tr>
            </tbody>
    </table>
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLable"
     aria-hidden="true">
     <div class="modal-dialog modal-lg modal-dialog-centered">
    <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLable">{{ ModalTitle }}</h5>
            <button type="button" claas="btn-close" data-bs-dismiss="modal"
            aria-label="close" ></button>
        </div>
        <div class="modal-body">
            <div class="input-group nb-3">
                <span class="input-group-text">Department Name</span>
                <input type="text" class="form-control" v-model="DepartmentName">
            </div>
            <div class="input-group nb-3">
                <span class="input-group-text">Department Password</span>
                <input type="text" class="form-control" v-model="DepPassword">
            </div>
            <button type="button"  @click="AddDeparment()"
            v-if="DepartmentId==0" class="btn btn-primary">Create
            </button>
            <button type="button" @click="EditDeparment()"
            v-if="DepartmentId!=0" class="btn btn-primary">Update
            </button>
        </div>
    </div>
     </div>
     </div>
</template>
<script>
import axios from 'axios';
const URL_API = 'http://localhost:5242/api/';

export default {
    components: {
   
  },
    data() {
        return {
            Departments: [],
            ModalTitle:'',
            DepartmentId:0,
            DepartmentName:'',
            searchId: '',
        searchDepartmentName: '',
        DepPassword:'',
        MadalPass:''
        };
    },
    methods: {
        filterDepartments() {
        this.Departments = this.Departments.filter(dep => {
            const searchIdWords = this.searchId.split(' ');
            const searchNameWords = this.searchDepartmentName.split(' ');

            // Check if the first word of ID matches the first word of searchId
            const matchesId = this.searchId === '' || searchIdWords.some(word => dep.DepartmentId.toString().toLowerCase().startsWith(word.toLowerCase()));

            // Check if the first word of Name matches the first word of searchDepartmentName
            const matchesName = this.searchDepartmentName === '' || searchNameWords.some(word => dep.DepartmentName.toLowerCase().startsWith(word.toLowerCase()));
            if (this.searchId === '' && this.searchDepartmentName === '') {
      this.refreshData();
    }
            return matchesId && matchesName;
        });
    },
        refreshData() {
            axios.get(URL_API + 'Department')
                .then((response) => {
                    this.Departments = response.data;
                })
                .catch((error) => {
                    console.error('Error fetching data:', error);
                });
        },
        
        createClick(){
            axios.post(URL_API + 'Department',{
                DepartmentName:this.DepartmentName,
                DepPassword:this.DepPassword
            })
            .then((response)=>{
                this.refreshData();
                alert(response.data)
            });
            console.log("DEpPassword: "+this.DepPassword)
        },
        AddClick(){
            this.ModalTitle="Add Department";
            this.DepartmentId=0;
            this.DepartmentName="";
            this.DepPassword="";
        },
        editClick(dep){
           
            this.ModalTitle="Edit Department";
            this.DepartmentId=dep.DepartmentId;
            this.DepartmentName=dep.DepartmentName;
            this.DepPassword=dep.DepPassword;

        },
        AddDeparment(){
            axios.post(URL_API+"Department",{
                DepartmentName:this.DepartmentName,
                DepPassword:this.DepPassword
            })
            .then((response)=>{
                this.refreshData();
                alert(response.data);
            });
        },
        EditDeparment(){
            axios.put(URL_API+"Department",{
                DepartmentId:this.DepartmentId,
                DepartmentName:this.DepartmentName,
                DepPassword:this.DepPassword
            })
            .then((response)=>{
                this.refreshData();
                alert(response.data);
            });
        },
        DeleteDeparment(id){
            if(!confirm("Are You Sure")){
                return;
            }
            axios.delete(URL_API+"Department/"+id)
            .then((response)=>{
                this.refreshData();
                alert(response.data);
            });
        },
        clearSearchFilters() {
        this.searchId = '';
        this.searchDepartmentName = '';
        this.refreshData();
    }
    },
    mounted() {
        this.refreshData();
    }
}
</script>
<style scoped>
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
</style>

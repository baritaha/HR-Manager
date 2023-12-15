<!-- BackgroundLayout.vue -->

<template>
    <div class="background-layout">
      <h1>EMployeeId: {{ this.EmployeeId }}</h1>
      <router-view></router-view>
    </div>
  </template>
  <script>
  export default{
    data(){
      return{
        EmployeeId:this.$route.params.EmployeeId
      }
    },
    created(){
      console.log("Emp Id in back : "+this.EmployeeId)
    }
  }
</script>
  <style scoped>
  .background-layout {
    background-color: #f0f0f0; 
    font-family: Arial, sans-serif;
    margin: 0; 
    padding: 0;
  }
  </style>
  
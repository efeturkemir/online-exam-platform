<script setup lang="ts">

import { useAuthStore } from '../stores/AuthStore';
import {ref} from 'vue'
const store = useAuthStore();

let username = ref("")
let userRole = ref("")




try {
  const decodedToken = store.decodeJWT();
  if (decodedToken) {
    const { _username, _role } = decodedToken;
    userRole.value = _role;
    username.value = _username;
  } else {
    // Handle case where decodeJWT() returns null
    console.error("Token is not valid or expired.");
  }
} catch (error: any) {
  console.error("Error:", error.message);
}






</script>


<template>
<header class="bg-gray-900 text-gray-100 p-4 shadow-lg">
  <div class="container mx-auto flex justify-between items-center">
    <RouterLink to="/" class="text-2xl font-bold hover:text-blue-400 transition duration-300 ease-in-out">
      Online Exam System
    </RouterLink>
    <nav class="flex items-center gap-4">
      <div class="group bg-gray-800 px-4 py-2 rounded-md flex items-center gap-2 hover:bg-gray-700 transition duration-300 ease-in-out">
        <i class="fas fa-user-circle text-lg"></i>
        <span>{{ username }}</span>
        <div class="absolute bg-gray-800 rounded-md shadow-lg p-2 mt-12 hidden group-hover:block">
          <!-- Additional user options can be added here -->
        </div>
      </div>
      <RouterLink v-if="userRole == 'admin'" to="/admin" class="bg-blue-600 px-4 py-2 rounded-md hover:bg-blue-500 transition duration-300 ease-in-out">
        <i class="fas fa-tools"></i> Admin Panel
      </RouterLink>
      <button @click="store.logout()" class="bg-red-700 px-4 py-2 rounded-md hover:bg-red-500 transition duration-300 ease-in-out">
        <i class="fas fa-sign-out-alt"></i> Logout
      </button>
    </nav>
  </div>
</header>

</template>


<style>
/* Add your styles here */
</style>

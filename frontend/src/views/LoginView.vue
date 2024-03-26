<script setup lang="ts">
import { ref } from 'vue';
import { useAuthStore } from '../stores/AuthStore';

let username = ref("")
let password = ref("")

const store = useAuthStore()

</script>
<template>


    <div class="min-h-screen flex items-center justify-center">
        <div class="bg-white p-8 rounded shadow-md w-full max-w-md">
            <h2 class="text-3xl font-semibold mb-6 text-center">Welcome to Online Exam Platform</h2>
            <form  @submit.prevent="store.login(username, password)" class="space-y-4">
                <div>
                    <label for="username" class="block text-gray-700 font-semibold">Username</label>
                    <input v-model="username" type="text" id="username" name="username"
                        class="w-full mt-1 p-2 border border-gray-300 rounded focus:outline-none focus:ring focus:ring-gray-900"
                        required>
                </div>
                <div>
                    <label for="password" class="block text-gray-700 font-semibold">Password</label>
                    <input v-model="password" type="password" id="password" name="password"
                        class="w-full mt-1 p-2 border border-gray-300 rounded focus:outline-none focus:ring focus:ring-gray-900"
                        required>
                </div>
                <div>
                    <button type="submit"
                        class="w-full bg-gray-900 hover:bg-gray-600 text-white font-semibold py-2 px-4 rounded focus:outline-none">
                        Login
                    </button>
                </div>
            </form>
            <p class="text-center text-gray-700 mt-4">Don't have an account? <RouterLink to="register"
                    class="text-gray-900 hover:underline">Register</RouterLink>
            </p>
            <p class="text-center text-xs text-gray-900 mt-2">&copy; 2024 Online Exam Platform. All rights reserved.</p>
        </div>
    </div>
</template>
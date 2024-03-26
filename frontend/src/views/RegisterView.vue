<script setup lang="ts">
import { ref } from 'vue';
import  { useAuthStore } from '../stores/AuthStore';

const store = useAuthStore()


let username = ref("")
let password = ref("")
let confirmPassword = ref("")
let role = ref("")
</script>
<template>
    <div class="min-h-screen flex items-center justify-center">
        <div class="bg-white p-8 rounded shadow-md w-full max-w-md">
            <h2 class="text-3xl font-semibold mb-6 text-center">Welcome Online Exam Platform</h2>
            <form @submit="store.register(username, password, confirmPassword, role)"  class="space-y-4">
                <div>
                    <label for="username" class="block text-gray-700 font-semibold">Username</label>
                    <input type="text" v-model="username" id="username" name="username"
                        class="w-full mt-1 p-2 border border-gray-300 rounded focus:outline-none focus:ring focus:ring-gray-900"
                        required>
                </div>
                <div>
                    <label for="password" class="block text-gray-700 font-semibold">Password</label>
                    <input type="password" v-model="password" id="password" name="password"
                        class="w-full mt-1 p-2 border border-gray-300 rounded focus:outline-none focus:ring focus:ring-gray-900"
                        required>
                </div>
                <div>
                    <label for="confirmPassword" class="block text-gray-700 font-semibold">Confirm Password</label>
                    <input type="password" v-model="confirmPassword" id="confirmPassword" name="confirmPassword"
                        class="w-full mt-1 p-2 border border-gray-300 rounded focus:outline-none focus:ring focus:ring-gray-900"
                        required>
                </div>
                <div>
                    <label for="role" class="block text-gray-700 font-semibold">Role</label>
                    <select v-model="role" id="role" name="role"
                        class="w-full mt-1 p-2 border border-gray-300 rounded focus:outline-none focus:ring focus:ring-gray-900"
                        required>
                        <option value="user">User</option>
                        <option value="admin">Admin</option>
                    </select>
                </div>
                <div>
                    <button  type="submit"
                        class="w-full bg-gray-900 hover:bg-gray-600 text-white font-semibold py-2 px-4 rounded focus:outline-none">
                        Register
                    </button>
                </div>
            </form>
            <p class="text-center text-gray-700 mt-4">Already have an account? <RouterLink to="login"
                    class="text-gray-900 hover:underline">Login</RouterLink>
            </p>
            <p class="text-center text-xs text-gray-900 mt-2">&copy; 2024 Online Exam Platform. All rights reserved.</p>
        </div>
    </div>
</template>
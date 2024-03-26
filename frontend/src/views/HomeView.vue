<script setup lang="ts">
import Navbar from '../components/Navbar.vue';
import router from '../router';
import { useExamStore } from '../stores/ExamStore';
import type  {  Exam }from '../types/Exam'
const store = useExamStore()

const exams : Exam[] = store.exams

const startExam  = async (id : String)  =>{
    
    await router.push(`/exams/${id}`)
}


</script>

<template>
  <!-- Navbar -->
  <Navbar></Navbar>

  <!-- Main Content -->
  <div class="container mx-auto mt-8 px-4 lg:px-0">
    <div v-if="exams.length" class="text-center">
      <h1 class="text-3xl font-bold text-gray-800 mb-6">
        You have <span class="text-blue-600">{{ exams.length }}</span> new exams <i class="fas fa-book text-blue-600"></i>
      </h1>
      <!-- Exam Cards -->
      <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
        <div v-for="exam in exams" :key="exam.id" class="bg-white transition-shadow duration-300 hover:shadow-xl rounded-lg overflow-hidden mb-4">
          <div class="p-6">
            <h2 class="text-2xl font-bold mb-2 text-gray-800">{{ exam.examName }}</h2>
            <p class="text-gray-600 mb-4">
              <i class="fas fa-file-alt mr-2"></i> Exam Type: {{ exam.examType }}
            </p>
            <button @click="startExam(exam.id)"
              class="bg-blue-500 hover:bg-blue-700 text-white font-semibold py-2 px-4 rounded transition duration-300 ease-in-out transform hover:-translate-y-1">
              <span>Start Exam!</span>
              <span class="ml-2" role="img" aria-label="Start Exam">🚀</span>
            </button>
          </div>
        </div>
      </div>
    </div>

    <div v-else class="text-center text-gray-600 text-lg mt-8">
      <p><i class="far fa-sticky-note mr-2"></i> There are no exams available at the moment. Please check back later!</p>
    </div>
  </div>
</template>


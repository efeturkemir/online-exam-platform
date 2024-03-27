<script setup lang="ts">
import Navbar from '../components/Navbar.vue';
import router from '../router';
import { useExamStore } from '../stores/ExamStore';
import type  {  Exam }from '../types/Exam'
const store = useExamStore()

const exams : Exam[] = store.exams

const startExam  = async (id : String)  =>{
    store.loading = true
    await router.push(`/exams/${id}`)
    store.loading = false
  }


</script>

<template>
  <!-- Navbar -->
  <Navbar></Navbar>

  <!-- Main Content -->
  <section class="main-content bg-gray-100 py-10">
    <div class="container mx-auto px-4">
      <div v-if="exams.length" class="exams-section">
        <h1 class="text-4xl font-extrabold text-center text-gray-800 mb-10">
          <span class="block sm:inline">Ready for </span>
          <span class="block sm:inline text-indigo-600">{{ exams.length }} New Challenges?</span>
        </h1>
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          <div v-for="exam in exams" :key="exam.id" class="exam-card bg-white rounded-xl shadow-lg transform transition duration-500 hover:scale-105">
            <div class="p-8">
              <h2 class="text-2xl font-semibold mb-3">{{ exam.examName }}</h2>
              <p class="text-gray-500 mb-5">
                <i class="fas fa-layer-group mr-2"></i>{{ exam.examType }}
              </p>
              <button @click="startExam(exam.id)"
                class="start-exam-btn w-full bg-indigo-500 hover:bg-indigo-600 text-white font-bold py-3 px-6 rounded-md transition duration-300 ease-in-out">
                Start Exam
                <i class="fas fa-arrow-right ml-2"></i>
              </button>
            </div>
          </div>
        </div>
      </div>
      <div v-else class="no-exams text-center py-10">
        <p class="text-xl text-gray-600">
          <i class="far fa-calendar-times mr-2"></i>No exams scheduled. Enjoy your free time!
        </p>
      </div>
    </div>
  </section>
</template>

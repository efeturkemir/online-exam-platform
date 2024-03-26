<script setup lang="ts">

import { useExamStore } from '../../stores/ExamStore';

const store = useExamStore()




</script>
<template>
  <div class="flex flex-col lg:flex-row h-screen">
    <!-- Sidebar -->
    <aside class="bg-gray-800 text-gray-200 w-full lg:w-64 lg:min-h-screen border-r border-gray-600">
  <div class="flex items-center justify-between p-4 border-b border-gray-600">
    <h1 class="text-xl font-bold tracking-wider uppercase">Admin Panel</h1>
    <button class="lg:hidden focus:outline-none">
      <i class="fas fa-bars h-6 w-6"></i>
    </button>
  </div>
  <nav class="mt-4">
    <RouterLink class="flex items-center py-3 px-4 rounded hover:bg-gray-700 transition duration-300" to="/admin/addExam">
      <i class="fas fa-plus-circle mr-2"></i>
      Add Exam
    </RouterLink>
    <RouterLink class="flex items-center py-3 px-4 rounded hover:bg-gray-700 transition duration-300" to="/admin/addQuestions">
      <i class="fas fa-question-circle mr-2"></i>
      Add Questions
    </RouterLink>
    <RouterLink class="flex items-center py-3 px-4 rounded hover:bg-gray-700 transition duration-300" to="/admin">
      <i class="fas fa-list-ul mr-2"></i>
      All Exams
    </RouterLink>
    <RouterLink class="flex items-center py-3 px-4 rounded hover:bg-gray-700 transition duration-300" to="/">
      <i class="fas fa-home mr-2"></i>
      Home Page
    </RouterLink>
  </nav>
</aside>

    <!-- Main Content -->
    <main class="bg-gray-100 flex-grow p-5">
      <RouterView></RouterView>
      <section v-if="$route.matched.length === 1" class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <article v-for="exam in store.exams" :key="exam.id" class="bg-white rounded-lg shadow p-6">
          <h2 class="text-2xl font-semibold mb-3">{{ exam.examName }}</h2>
          <p class="text-gray-700 mb-4">Exam type: {{ exam.examType }}</p>
          <p :class="{
            'text-green-600': exam.questionCount === exam.questions.length,
            'text-red-600': exam.questionCount !== exam.questions.length
          }">
            {{ exam.questionCount === exam.questions.length ? 'Ready' : 'Not Ready - Insufficient Questions' }}
          </p>
          <div class="flex items-center justify-center gap-4 mt-4">
            <router-link v-if="exam.questionCount !== exam.questions.length" :to="'/admin/addQuestions'"
              class="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded transition duration-300">
              Add Questions
            </router-link>
            <button @click="store.removeExam(exam.id)"
              class="bg-red-600 hover:bg-red-700 text-white font-bold py-2 px-4 rounded transition duration-300">
              Remove Exam
            </button>
          </div>
        </article>
      </section>
    </main>
  </div>
</template>

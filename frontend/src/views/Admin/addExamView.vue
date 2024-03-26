<script setup lang="ts">
import { ref } from 'vue';
import { useExamStore } from '../../stores/ExamStore';
import type { Exam } from '../../types/Exam';

const newExam: Exam = {
    id: ref("").value,
    examName: ref("").value,
    examType: ref("").value,
    questionCount: ref(0).value,
    participants: [],
    questions: []

}


const store = useExamStore()
</script>



<template>
    <div class="p-6 bg-gray-100">
      <h1 class="text-3xl font-bold mb-6 text-gray-800">Create New Exam</h1>
      <form @submit.prevent="store.addExam(newExam)" class="bg-white shadow-md rounded-lg p-6">
        <div class="mb-6">
          <label class="block text-gray-800 font-semibold mb-2" for="exam-type">Exam Type:</label>
          <select v-model="newExam.examType"
            class="block w-full border-gray-300 rounded-md shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
            id="exam-type" required>
            <option disabled value="">-- Select Exam Type --</option>
            <option value="midterm">Midterm</option>
            <option value="final">Final</option>
          </select>
        </div>
  
        <div class="mb-6">
          <label class="block text-gray-800 font-semibold mb-2" for="exam-name">Exam Name:</label>
          <input v-model="newExam.examName"
            class="block w-full border-gray-300 rounded-md shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
            id="exam-name" type="text" placeholder="Enter exam name..." required>
        </div>
  
        <div class="mb-6">
          <label class="block text-gray-800 font-semibold mb-2" for="question-count">Question Count:</label>
          <select v-model="newExam.questionCount"
            class="block w-full border-gray-300 rounded-md shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
            id="question-count" required>
            <option disabled value="">-- Select Question Count --</option>
            <option value="10">10</option>
            <option value="20">20</option>
            <option value="30">30</option>
            <option value="40">40</option>
          </select>
        </div>
  
        <div class="flex justify-end">
          <button class="bg-indigo-600 hover:bg-indigo-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="submit">
            Create Exam
          </button>
        </div>
      </form>
    </div>
  </template>
  
  
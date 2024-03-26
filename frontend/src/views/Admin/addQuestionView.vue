<script setup lang="ts">
import { ref } from 'vue';
import { useExamStore } from '../../stores/ExamStore';
import type { Exam, Question } from '../../types/Exam';

const store = useExamStore()

const exams: Exam[] = store.exams

const selectedExam = ref<Exam>(exams[0])

const newQuestion: Question = {
  questionText: ref<string>("").value,
  options: ([""]),
  answerIndex: ref(0).value

}


</script>

<template>
  <div class="p-6 bg-gray-100">
    <h1 class="text-3xl font-bold mb-6 text-gray-800">Add a New Question</h1>
    <form @submit.prevent="store.addQuestion(selectedExam.id, newQuestion)" class="bg-white shadow-md rounded-lg p-6">
      <div class="mb-6">
        <label class="block text-gray-800 font-semibold mb-2">Select Exam:</label>
        <select v-model="selectedExam"
          class="block w-full border-gray-300 rounded-md shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
          required>
          <option disabled value="">-- Select an Exam --</option>
          <option v-for="exam in exams" :key="exam.id" :value="exam">{{ exam.examName }}</option>
        </select>
      </div>

      <div class="mb-6">
        <label class="block text-gray-800 font-semibold mb-2" for="question-text">Question Text:</label>
        <textarea v-model="newQuestion.questionText"
          class="block w-full border-gray-300 rounded-md shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
          id="question-text" rows="4" placeholder="Enter the question text here..." required></textarea>
      </div>

      <div class="grid grid-cols-1 md:grid-cols-2 gap-6 mb-6">
        <!-- Options input fields -->
        <div v-for="index in 4" :key="index">
          <label class="block text-gray-800 font-semibold mb-2" :for="'option-' + index">Option {{ index }}:</label>
          <input v-model="newQuestion.options[index - 1]"
            class="block w-full border-gray-300 rounded-md shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
            :id="'option-' + index" type="text" :placeholder="'Option ' + index + '...'" required>
        </div>
      </div>

      <div class="mb-6">
        <label class="block text-gray-800 font-semibold mb-2" for="answer-index">Correct Answer Index:</label>
        <input v-model="newQuestion.answerIndex"
          class="block w-full border-gray-300 rounded-md shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
          id="answer-index" type="number" placeholder="Enter the index of the correct answer..." min="0" max="3" required>
      </div>

      <div class="flex justify-end">
        <button class="bg-indigo-600 hover:bg-indigo-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="submit">
          Add Question
        </button>
      </div>
    </form>
  </div>
</template>

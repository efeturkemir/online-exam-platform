<script setup lang="ts">
import { defineProps } from 'vue';
import type { Exam } from '../types/Exam';

const props = defineProps<{
  exam: Exam;
  answers: number[];
  correctCount: number;
  closeModal: () => void;
}>();
</script>




<template>
  <transition name="fade">
    <div class="fixed inset-0 bg-gray-900 bg-opacity-75 flex items-center justify-center p-4 z-50" @click.self="props.closeModal">
      <div class="bg-white rounded-lg shadow-2xl w-full max-w-lg max-h-full overflow-y-auto">
        <header class="flex justify-between items-center bg-blue-500 text-white p-4">
          <h2 class="text-xl font-semibold flex items-center"><i class="fas fa-clipboard-list mr-2"></i> Exam Results</h2>
          <button class="text-2xl focus:outline-none" @click="props.closeModal"><i class="fas fa-times"></i></button>
        </header>

        <div class="p-6">
          <div class="bg-blue-100 text-blue-800 p-4 rounded-lg mb-6">
            <p class="flex items-center"><i class="fas fa-star mr-2"></i> Your Score: <strong class="ml-1">{{ props.correctCount }}/{{ props.exam.questionCount }}</strong></p>
            <p class="flex items-center"><i class="fas fa-chart-line mr-2"></i> Percentage: <strong class="ml-1">{{ (props.correctCount / props.exam.questionCount * 100).toFixed(2) }}%</strong></p>
          </div>
          <div class="space-y-4">
            <div class="bg-gray-100 p-4 rounded-lg" v-for="(question, index) in props.exam.questions" :key="index">
              <div class="mb-2">
                <h3 class="text-lg font-semibold">Question {{ index + 1 }}</h3>
              </div>
              <div>
                <p class="font-bold mb-2">{{ question.questionText }}</p>
                <p class="flex items-center" :class="{ 'text-green-600': props.answers[index] === question.answerIndex, 'text-red-600': props.answers[index] !== question.answerIndex }">
                  <i class="fas" :class="{'fa-check mr-2': props.answers[index] === question.answerIndex, 'fa-times mr-2': props.answers[index] !== question.answerIndex}"></i>
                  {{ props.answers[index] === question.answerIndex ? 'Correct' : 'Your' }} Answer: {{ question.options[props.answers[index]] }}
                  <span v-if="props.answers[index] !== question.answerIndex" class="ml-2">| Correct Answer: {{ question.options[question.answerIndex] }}</span>
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </transition>
</template>


<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.8);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 999;
}

.modal {
  background: #fff;
  padding: 20px;
  border-radius: 15px;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);
  width: 80%;
  max-width: 650px;
  max-height: 80vh; /* Set maximum height */
  overflow-y: auto; /* Enable vertical scrollbar if needed */
}

.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.close-button {
  background: none;
  border: none;
  font-size: 1.5rem;
  cursor: pointer;
  color: #333;
}

.score-card,
.question-cards {
  margin-bottom: 20px;
}

.card {
  background: #f7f7f7;
  border: 1px solid #e1e1e1;
  border-radius: 10px;
  padding: 15px;
  margin-bottom: 10px;
}

.card-header {
  border-bottom: 1px solid #e1e1e1;
  padding-bottom: 10px;
}

.card-body {
  padding-top: 10px;
}

.question {
  font-weight: bold;
}

.answer {
  display: flex;
  align-items: center;
  margin-top: 5px;
}

.correct {
  color: #28a745;
}

.incorrect {
  color: #dc3545;
}

.fas {
  margin-right: 10px;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s ease;
}
.fade-enter-from, .fade-leave-to {
  opacity: 0;
}
</style>

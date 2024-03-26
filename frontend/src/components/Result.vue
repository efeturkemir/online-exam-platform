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
    <div class="modal-overlay" @click.self="props.closeModal">
      <div class="modal">
        <header class="modal-header">
          <h2><i class="fas fa-clipboard-list"></i> Exam Results</h2>
          <button class="close-button" @click="props.closeModal"><i class="fas fa-times"></i></button>
        </header>

        <div class="modal-content">
          <div class="score-card">
            <p><i class="fas fa-star"></i> Your Score: <strong>{{ props.correctCount }}/{{ props.exam.questionCount }}</strong></p>
            <p><i class="fas fa-chart-line"></i> Percentage: <strong>{{ (props.correctCount / props.exam.questionCount * 100).toFixed(2) }}%</strong></p>
          </div>
          <div class="question-cards">
            <div class="card" v-for="(question, index) in props.exam.questions" :key="index">
              <div class="card-header">
                <h3>Question {{ index + 1 }}</h3>
              </div>
              <div class="card-body">
                <p class="question">{{ question.questionText }}</p>
                <p class="answer" :class="{ 'correct': props.answers[index] === question.answerIndex, 'incorrect': props.answers[index] !== question.answerIndex }">
                  <i class="fas" :class="{'fa-check': props.answers[index] === question.answerIndex, 'fa-times': props.answers[index] !== question.answerIndex}"></i>
                  {{ props.answers[index] === question.answerIndex ? 'Correct' : 'Your' }} Answer: {{ question.options[props.answers[index]] }}
                  <span v-if="props.answers[index] !== question.answerIndex">| Correct Answer: {{ question.options[question.answerIndex] }}</span>
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

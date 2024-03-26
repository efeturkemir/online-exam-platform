<script setup lang="ts">
import { useRoute } from 'vue-router';
import Question from '@/components/Question.vue';
import Result from '@/components/Result.vue';
import { useExamStore } from '@/stores/ExamStore';
import type { Exam } from '@/types/Exam';
import { computed, ref } from 'vue';

const route = useRoute();
const store = useExamStore();
const selectedExam = store.exams.find((exam : Exam) => exam.id === route.params.examId);
const correctAnswer = ref(0);
const selectedAnswers = ref<number[]>([]);
const submitted = ref(false);
const currentIndex = ref(0);
const resultModal = ref(false);

const updateSelectedAnswer = (questionIndex: number, selectedIndex: number) => {
  selectedAnswers.value[questionIndex] = selectedIndex;
};

const submitExam = () => {
  selectedAnswers.value.forEach((selected, index) => {
    if (selectedExam?.questions[index].answerIndex === selected) {
      correctAnswer.value++;
    }
  });
  console.log(`Total correct answers: ${correctAnswer.value}`);
  submitted.value = true;
  resultModal.value = true;
};

const closeModal = () => {
  resultModal.value = false;
};

const navigateQuestion = (direction: number) => {
  const newIndex = currentIndex.value + direction;
  if (newIndex >= 0 && selectedExam && newIndex < selectedExam.questions.length) {
    currentIndex.value = newIndex;
  }
};

const progressBarWidth = computed(() => {
  const questionCount = selectedExam?.questions.length ?? 1;
  return `${(currentIndex.value / questionCount) * 100}%`;
});
</script>

<template>
  <body v-if="selectedExam" class="bg-gray-100 flex flex-col min-h-screen">
    <main class="flex-grow container mx-auto px-4 py-6 flex flex-col items-center justify-center min-h-screen">
      <div class="bg-white shadow-lg rounded-lg p-8 w-full max-w-4xl">
        <!-- Exam div -->
        <div class="flex items-center">
          <router-link to="/" class="text-blue-600 hover:text-blue-800 mr-6">
            <i class="fa fa-home fa-2x" aria-hidden="true"></i>
          </router-link>
          <h2 class="text-4xl font-extrabold">{{ selectedExam.examName }}</h2>
        
        </div>
        <div class="text-right">
            <span class="text-xl font-semibold">Time Remaining:</span>
            <span id="timer" class="text-xl font-bold text-red-500">60:00</span>
          </div>

        <!-- Progress Bar -->
        <div class="w-full bg-gray-300 rounded-full h-2.5 mb-8">
          <div class="bg-green-500 rounded-full h-2.5" :style="{ width: progressBarWidth }"></div>
        </div>

        <!-- Question Component -->
        <Question @update-answer="updateSelectedAnswer" :question="selectedExam.questions[currentIndex]"
                  :index="currentIndex" />

        <!-- Navigation Buttons -->
        <div class="flex justify-between my-8">
          <button @click="navigateQuestion(-1)"
                  class="bg-gray-400 hover:bg-gray-500 text-gray-800 font-bold py-2 px-6 rounded-lg transition duration-300">
            Previous
          </button>
          <button v-if="currentIndex !== selectedExam.questions.length - 1" @click="navigateQuestion(1)"
                  class="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-6 rounded-lg transition duration-300">
            Next
          </button>
        </div>

        <!-- Submit Button -->
        <div class="flex justify-center my-8">
          <button @click="submitExam"
                  class="bg-green-600 hover:bg-green-700 text-white font-bold py-3 px-8 rounded-lg transition duration-300">
            Submit Exam
          </button>
        </div>
      </div>
    </main>

    <!-- Result Modal -->
    <Result v-show="resultModal" :exam="selectedExam" :answers="selectedAnswers" :correctCount="correctAnswer"
            :close-modal="closeModal" />

    <!-- Footer -->
    <footer class="bg-gray-200 w-full py-6 text-center">
      <p class="text-gray-700">© 2024 Online Exam Platform. All rights reserved.</p>
    </footer>
  </body>
</template>


<style scoped>
/* Add your styles here */
html,
body {
  height: 100%;
}

body {
  display: flex;
  flex-direction: column;
}

main {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
}

.bg-green-500 {
  /* Add transition effect for width change */
  transition: width 0.5s ease-in-out;
}
</style>

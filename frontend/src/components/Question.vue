<script setup lang="ts">
import { ref, watch } from 'vue';
import type { Question } from '../types/Exam';
const props = defineProps<{
  question: Question
  index: number;
}>();
const emits = defineEmits(['update-answer']); 


const selectedOptionIndex = ref<number | null>(null);

const updateSelectedAnswer = (optIndex: number) => {
  selectedOptionIndex.value = optIndex;
  emits('update-answer', props.index, optIndex);
};

watch(() => props.question, () => {
  selectedOptionIndex.value = null;
});


</script>

<template>
  <section class="mb-10 p-6 bg-white rounded-lg shadow">
    <h3 class="text-2xl font-bold text-gray-900 mb-4">Question {{ index + 1 }}</h3>
    <p class="text-gray-700 text-lg mb-6">{{ props.question.questionText }}</p>
    <div class="flex flex-col space-y-4">
      <div v-for="(option, optionIndex) in props.question.options" :key="optionIndex" class="flex items-center">
        <input type="radio" :id="'option' + optionIndex" :value="optionIndex" class="hidden peer" :name="'question_' + index" :checked="selectedOptionIndex === optionIndex" @change="updateSelectedAnswer(optionIndex)">
        <label :for="'option' + optionIndex" class="flex items-center cursor-pointer peer-checked:bg-blue-600 peer-checked:text-white peer-checked:font-semibold p-3 w-full bg-gray-200 rounded-lg transition-colors duration-300">
          <span class="w-6 h-6 inline-block mr-2 rounded-full border border-gray-400 flex items-center justify-center">
            <span class="w-4 h-4 rounded-full bg-blue-600 scale-0 peer-checked:scale-100 transition-transform duration-300"></span>
          </span>
          {{ option }}
        </label>
      </div>
    </div>
  </section>
</template>

<style scoped>
/* Additional styles to support the custom radio button */
input[type="radio"] {
  accent-color: transparent;
}
input[type="radio"]:checked + label {
  background-color: #3182ce; /* TailwindCSS blue-600 */
  color: white;
}
input[type="radio"]:checked + label span:first-child {
  border-color: #3182ce; /* TailwindCSS blue-600 */
}
input[type="radio"]:checked + label span:last-child {
  background-color: white;
}
</style>

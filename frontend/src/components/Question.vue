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
  <section class="mb-10 p-8 bg-gray-100 rounded-xl shadow-lg">
    <h3 class="text-3xl font-bold text-gray-800 mb-6">Question {{ index + 1 }}</h3>
    <p class="text-lg mb-8 text-gray-700">{{ props.question.questionText }}</p>
    <div class="flex flex-col space-y-4">
      <div v-for="(option, optionIndex) in props.question.options" :key="optionIndex" class="flex items-center">
        <input type="radio" :id="'option' + optionIndex" :value="optionIndex" class="hidden peer" :name="'question_' + index" :checked="selectedOptionIndex === optionIndex" @change="updateSelectedAnswer(optionIndex)">
        <label :for="'option' + optionIndex" class="flex items-center cursor-pointer peer-checked:bg-gray-300 peer-checked:ring-2 peer-checked:ring-gray-400 p-4 w-full bg-white rounded-full transition-all duration-300 ease-in-out">
          <span class="w-6 h-6 inline-block mr-3 rounded-full border-2 border-gray-400 flex items-center justify-center">
            <span class="w-4 h-4 rounded-full bg-gray-800 scale-0 peer-checked:scale-100 transition-transform duration-300"></span>
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
  background-color: #e2e8f0; /* TailwindCSS gray-300 */
  color: #2d3748; /* TailwindCSS gray-800 */
}
input[type="radio"]:checked + label span:first-child {
  border-color: #cbd5e0; /* TailwindCSS gray-400 */
}
input[type="radio"]:checked + label span:last-child {
  background-color: #2d3748; /* TailwindCSS gray-800 */
}
</style>


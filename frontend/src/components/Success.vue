<script setup lang="ts">
import { useExamStore } from '../stores/ExamStore';

const store = useExamStore();
const props = defineProps<{
  text: string;
}>();


</script>




<template>
    <Transition name="success-modal">
        <div :class="{ 'scale-100': store.success }" class="fixed scale-0 z-10 inset-0 overflow-y-auto">
    <div class="flex items-center justify-center min-h-screen px-4 pt-4 pb-20 text-center">

      <div class="fixed inset-0 bg-gray-500 bg-opacity-75"></div>

      <div :class="{ 'scale-100': store.success }"
        class="transform scale-0 transition-transform duration-300 relative z-10 w-full max-w-md p-6 bg-white rounded-lg shadow-lg">

        <div class="flex items-center justify-center w-12 h-12 mx-auto mb-4 text-green-500 bg-green-100 rounded-full">
          <svg class="w-6 h-6 fill-current" viewBox="0 0 20 20">
            <path
              d="M17.707 4.293c-.391-.391-1.023-.391-1.414 0l-7.293 7.293-3.293-3.293c-.391-.391-1.023-.391-1.414 0-.391.391-.391 1.023 0 1.414l4 4c.195.195.451.293.707.293s.512-.098.707-.293l8-8c.391-.391.391-1.023 0-1.414z">
            </path>
          </svg>
        </div>

        <p class="text-lg font-medium text-gray-800">{{text}}</p>




        <div class="mt-6">
          <button @click="store.success = !store.success"
            class="px-4 py-2 text-sm font-medium text-white bg-green-500 rounded hover:bg-green-600 focus:outline-none focus:ring focus:ring-green-500">
            Okay
          </button>
        </div>
      </div>
    </div>
  </div>


    </Transition>
</template>


<style scoped>
.success-modal-enter-active,
.success-modal-leave-active {
  transition: opacity 0.3s ease;
}

.success-modal-enter-from,
.success-modal-leave-to {
  opacity: 0;
}
</style>
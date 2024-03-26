<script setup lang="ts">
import { useAuthStore } from '../stores/AuthStore';
import { useExamStore } from '../stores/ExamStore';

const examStore = useExamStore();
const authStore = useAuthStore()
const props = defineProps<{
  text: string;
  storeName: string;
}>();


const resetError = () =>{
    if(props.storeName === "Auth"){
     authStore.error = false;
    }else{
        examStore.error = false;
    }
}
</script>
<template>
  <Transition name="error-modal">
    <div v-if="examStore.error || authStore.error" class="fixed z-10 inset-0 overflow-y-auto">
      <div class="flex items-center justify-center min-h-screen px-4 pt-4 pb-20 text-center">
        <!-- Overlay background -->
        <div class="fixed inset-0 bg-gray-500 bg-opacity-75"></div>
        <!-- Modal content -->
        <div class="transform transition-transform duration-300 relative z-10 w-full max-w-md p-6 bg-white rounded-lg shadow-lg">
          <!-- Icon -->
          <div class="flex items-center justify-center w-12 h-12 mx-auto mb-4 text-red-500 bg-red-100 rounded-full">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
              <circle cx="12" cy="12" r="10" />
              <line x1="15" y1="9" x2="9" y2="15" />
              <line x1="9" y1="9" x2="15" y2="15" />
            </svg>
          </div>
          <!-- Message -->
          <p class="text-lg font-medium text-gray-800">{{ text }}</p>
          <!-- Button -->
          <div class="mt-6">
            <button @click="resetError" class="px-4 py-2 text-sm font-medium text-white bg-red-500 rounded hover:bg-red-600 focus:outline-none focus:ring focus:ring-red-500">
              Try again
            </button>
          </div>
        </div>
      </div>
    </div>
  </Transition>
</template>


<style scoped>
.error-modal-enter-active,
.error-modal-leave-active {
  transition: opacity 0.3s ease;
}

.error-modal-enter-from,
.error-modal-leave-to {
  opacity: 0;
}
</style>
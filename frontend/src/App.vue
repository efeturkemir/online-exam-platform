<script setup lang="ts">
import { RouterView } from 'vue-router'
import { useExamStore } from './stores/ExamStore'
import Loading from './components/Loading.vue';
import Error from './components/Error.vue'
import { useAuthStore } from './stores/AuthStore';
import Success from './components/Success.vue';





const examStore = useExamStore()
const authStore = useAuthStore()
examStore.getExams()

const isAnyStoreLoading = () => {
  return examStore.loading || authStore.loading
}


</script>

<template>
  <Loading v-if="isAnyStoreLoading()"></Loading>
  <RouterView v-else></RouterView>
  <Error v-show="examStore.error || authStore.error"
    :text="examStore.error ? examStore.errorMessage : authStore.errorMessage"
    :storeName="examStore.error ? 'Exam' : 'Auth'">></Error>
  <Success :text="examStore.successMessage" v-show="examStore.success"></Success>

</template>
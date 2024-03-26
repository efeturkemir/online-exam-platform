import { ref } from "vue";
import { defineStore } from "pinia";
import axios from "axios";
import type { Exam, Question } from "../types/Exam";
import { useAuthStore } from "./AuthStore";
const apiUrl = import.meta.env.VITE_API_URL;
export const useExamStore = defineStore("exams", {
  state: () => ({
    exams: ref<Exam[]>([]),
    loading: ref(false),
    error: ref(false),
    errorMessage: ref(""),
    success: ref(false),
    successMessage: ref(""),
  }),
  actions: {
    async getExams() {
      try {
        this.loading = true;
        const response = await axios.get(apiUrl + "exams");
        this.exams = await response.data;
        this.loading = false;
        console.log(this.exams);
      } catch (error: any) {
        this.loading = false;
        this.error = true;
        this.errorMessage = error.response.data.message;
      }
    },
    async addExam(exam: Exam) {
      const store = useAuthStore();
      const token = store.getCookie("userToken");
      try {
        this.loading = true;
        const response = await axios.post(
          apiUrl + `exams`,
          {
            examName: exam.examName,
            examType: exam.examType,
            questionCount: exam.questionCount,
            questions: exam.questions,
          },
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        await this.getExams();
        this.loading = false;
        this.success = true;
        this.successMessage = response.data.message;
      } catch (err: any) {
        this.loading = false;
        this.error = true;
        this.errorMessage = err.response.data.message;
      }
    },
    async removeExam(examId: string) {
      try {
        this.loading = true;
        const store = useAuthStore();
        const token = store.getCookie("userToken");
        const response = await axios.delete(apiUrl + `exams/${examId}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        this.getExams();
        this.loading = false;
        this.success = true;
        this.successMessage = response.data.message;
      } catch (error: any) {
        this.loading = false;
        this.error = true;
        this.errorMessage = error.response.data.message;
      }
    },
    async addQuestion(examId: string, question: Question) {
      try {
        this.loading = true;
        const store = useAuthStore();
        const token = store.getCookie("userToken");
        const response = await axios.post(
          apiUrl + `exams/${examId}/questions`,
          {
            questionText: question.questionText,
            options: question.options,
            answerIndex: question.answerIndex,
          },
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        this.getExams();
        this.loading = false;
        this.success = true;
        this.successMessage = response.data.message;
      } catch (err: any) {
        this.loading = false;
        this.error = true;
        this.errorMessage = err.response.data.message;
      }
    },
  },
});

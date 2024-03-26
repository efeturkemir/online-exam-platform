import { createRouter, createWebHistory } from "vue-router";
import { useAuthStore } from "../stores/AuthStore";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: () => import("../views/HomeView.vue"),
      meta: { requiresAuth: true },
    },
    {
      path: "/login",
      name: "login",
      component: () => import("../views/LoginView.vue"),
    },
    {
      path: "/register",
      name: "register",
      component: () => import("../views/RegisterView.vue"),
    },
    {
      path: "/exams/:examId",
      name: "exam",
      component: () => import("../views/ExamView.vue"),
    },

    {
      path: "/admin",
      name: "Admin",
      component: () => import("../views/Admin/AdminView.vue"),
      meta: { requiresAuth: true, requiresAdmin: true },

      children: [
        {
          path: "addExam",
          component: () => import("../views/Admin/addExamView.vue"),
        },
        {
          path: "addQuestions",
          component: () => import("../views/Admin/addQuestionView.vue"),
        },
      ],
    },
  ],
});

router.beforeEach(async (to, from, next) => {
  const store = useAuthStore();
  const isLoggedIn = !!store.getCookie("userToken");
  

  if (to.name === "login" || to.name === "register") {
    if (isLoggedIn) {
      next({ name: "home" });
    } else {
      next();
    }
  } else {
    if (to.matched.some((record) => record.meta.requiresAuth)) {
      if (!isLoggedIn) {
        next({ name: "login" });
      } else {
        if (to.matched.some((record) => record.meta.requiresAdmin)) {
          const store = useAuthStore()
          const role = store.decodeJWT()?._role
          if (role !== "admin") {
            next({ name: "home" });
          } else {
            next();
          }
        } else {
          next();
        }
      }
    } else {
      next();
    }
  }
});

export default router;

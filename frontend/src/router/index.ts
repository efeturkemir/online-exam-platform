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
  const token = store.getCookie("userToken");
  const isLoggedIn = !!token;

  // Check if the user is trying to access login or register page
  if (to.name === "login" || to.name === "register") {
    if (isLoggedIn && !store.decodeJWT().isExpired) {
      next({ name: "home" });
    } else {
      next();
    }
  } else {
    // Check if the route requires authentication
    if (to.matched.some((record) => record.meta.requiresAuth)) {
      if (!isLoggedIn || store.decodeJWT().isExpired) {
        next({ name: "login" });
      } else {
        // Check if the route requires admin role
        const role = store.decodeJWT()._role;
        if (to.matched.some((record) => record.meta.requiresAdmin) && role !== "admin") {
          next({ name: "home" });
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

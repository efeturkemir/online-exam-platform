import { ref } from "vue";
import { defineStore } from "pinia";
import axios from "axios";
import type User from "../types/User";
import router from "../router";
const apiUrl = import.meta.env.VITE_API_URL;
export const useAuthStore = defineStore("auth", {
  state: () => ({
    error: ref(false),
    errorMessage: ref(""),
    user: ref<User>(),
    loading: ref(false),
  }),
  actions: {
    async login(username: string, password: string) {
      try {
        this.loading = true;
        const response = await axios.post(apiUrl + "users/login", {
          username: username,
          password: password,
        });
        this.loading = false;

        this.setCookie("userToken", response.data.token, 1);
        router.push("/");
      } catch (error: any) {
        this.loading = false;
        this.error = true;
        this.errorMessage = error.response.data.message;
      }
    },

    async register(
      username: string,
      password: string,
      confirmPassword: string,
      role: string
    ) {
      try {
        this.loading = true;
        const response = await axios.post(apiUrl + "users/register", {
          username: username,
          password: password,
          confirmPassword: confirmPassword,
          role: role,
        });
        this.loading = false;
        this.login(username, password);
      } catch (error: any) {
        this.loading = false;
        this.error = true;
        this.errorMessage = error.response.data.message;
      }
    },
    decodeJWT() {
      const token = this.getCookie("userToken")
      const base64urlDecode = (str: string) => {
        return decodeURIComponent(atob(str).split('').map((c) => {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));
      };
    
      const parts = token.split(".");
      if (parts.length !== 3) {
        return { isExpired: true };
      }
    
      const payload = JSON.parse(base64urlDecode(parts[1]));
      const now = Date.now() / 1000; // Current time in seconds
    
      return {
        _userId: payload.ID,
        _username: payload.username,
        _role: payload.role,
        isExpired: payload.exp < now
      };
    },
    async logout() {
      this.deleteCookie("userToken");
      await router.push("/login");
    },
    setCookie(cname: string, cvalue: string, exdays: number) {
      const d = new Date();
      d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
      let expires = "expires=" + d.toUTCString();
      document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    },
    getCookie(cname: string) {
      let name = cname + "=";
      let decodedCookie = decodeURIComponent(document.cookie);
      let ca = decodedCookie.split(";");
      for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == " ") {
          c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
          return c.substring(name.length, c.length);
        }
      }
      return "";
    },
    deleteCookie(cname: string) {
      document.cookie =
        cname + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    },
  },
});

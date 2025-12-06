import axios from 'axios';
import { CINESLATE_API_URL, LOCAL_JWT } from '@/config';
import { useUserStore } from '../store/userStore';
import { User } from '@/features/Users/Models/userType';

const apiClient = axios.create({
  baseURL: import.meta.env.VITE_CINESLATE_API_URL ?? CINESLATE_API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem(LOCAL_JWT);

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  } else {
    delete config.headers.Authorization;
  }

  return config;
});

apiClient.interceptors.response.use(
  (response) => response?.data,
  async (error) => {
    if (error?.status === 401) {
      const token = await GetRefreshToken();

      if (!token) return;

      const originalRequest = error.config;
      originalRequest._retry = true;
      originalRequest.headers.Authorization = `Bearer ${token}`;

      return apiClient(originalRequest);
    }

    return Promise.reject({
      status: { code: error?.status, text: error?.response?.statusText },
      title: error?.response?.data?.title,
      traceId: error?.response?.data?.traceId,
      type: error?.response?.data?.type,
      detail: error?.response?.data?.detail,
    });
  }
);

async function GetRefreshToken(): Promise<string | null> {
  const { refreshToken } = useUserStore.getState().user;
  console.log(refreshToken);

  if (!refreshToken) return null;

  const refreshResult = await apiClient.post<User>('/users/refresh-token', {
    refreshToken: refreshToken,
  });

  if (!refreshResult) return null;

  useUserStore.setState({ user: refreshResult.data });
  return refreshResult.data.token ?? null;
}

export default apiClient;

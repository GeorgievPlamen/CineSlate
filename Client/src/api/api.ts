import axios from 'axios';
import { CINESLATE_API_URL, LOCAL_JWT } from '../config';

const apiClient = axios.create({
  baseURL: import.meta.env.VITE_CINESLATE_API_URL ?? CINESLATE_API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

apiClient.interceptors.request.use((config) => {
  const token = `Bearer ${localStorage.getItem(LOCAL_JWT)}`;
  config.headers['Authorization'] = token;
  return config;
});

apiClient.interceptors.response.use(
  (response) => response?.data,
  (error) =>
    Promise.reject({
      status: { code: error?.status, text: error?.response?.statusText },
      title: error?.response?.data?.title,
      traceId: error?.response?.data?.traceId,
      type: error?.response?.data?.type,
      detail: error?.response?.data?.detail,
    })
);

export default apiClient;

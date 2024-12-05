import axios from 'axios';
import { CINESLATE_API_URL, SESSION_JWT } from '../config';

const api = axios.create({
  baseURL: CINESLATE_API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

api.interceptors.request.use((config) => {
  const token = `Bearer ${sessionStorage.getItem(SESSION_JWT)}`;
  config.headers['Authorization'] = token;
  return config;
});

api.interceptors.response.use(
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

export default api;
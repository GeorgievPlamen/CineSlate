import apiClient from '@/api/api';
import { Paged } from '@/models/paged';
import { User } from '../Models/userType';


export const usersApi = {
  login: async (formData: FormData): Promise<User> =>
    apiClient.post('users/login', formData),
  register: async (formData: FormData): Promise<User> =>
    apiClient.post('users/register', formData),
  me: async (): Promise<User> => apiClient.get('users/me'),
  refresh: async (refreshToken: string): Promise<User> => {
    return apiClient.post('users/refresh-token', { refreshToken: refreshToken });
  },
  getLatestUsers: async (page: number): Promise<Paged<User>> =>
    apiClient.get(`/users/${page}`),

  postGetUsersByIdQuery: async (ids: string[]): Promise<User[]> =>
    apiClient.post("/users", { UserIds: ids })
};

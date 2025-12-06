import { Paged } from '@/common/models/paged';
import { User } from '../Models/userType';
import apiClient from '@/api';

export const usersClient = {
  login: async (formData: FormData): Promise<User> =>
    apiClient.post('users/login', formData),

  register: async (formData: FormData): Promise<User> =>
    apiClient.post('users/register', formData),

  me: async (): Promise<User> => apiClient.get('users/me'),

  refresh: async (refreshToken: string): Promise<User> =>
    apiClient.post('users/refresh-token', {
      refreshToken: refreshToken,
    }),

  getLatestUsers: async (page: number): Promise<Paged<User>> =>
    apiClient.get(`/users/${page}`),

  getUsersByIds: async (ids: string[]): Promise<User[]> =>
    apiClient.post('/users', { UserIds: ids }),

  updateUser: async (id: string, bio: string, pictureBase64: string) =>
    apiClient.put(`users/${id}?bio=${bio}`, `"${pictureBase64}"`),
};

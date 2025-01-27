import api from '../../../app/api/api';
import { User } from '../Models/userType';

export const userApi = {
  login: async (formData: FormData): Promise<User> =>
    api.post('users/login', formData),
  register: async (formData: FormData): Promise<User> =>
    api.post('users/register', formData),
  me: async (): Promise<User> => api.get('users/me'),
  refresh: async (refreshToken: string): Promise<User> =>
    api.post('users/refresh-token', { refreshToken: refreshToken }),
};

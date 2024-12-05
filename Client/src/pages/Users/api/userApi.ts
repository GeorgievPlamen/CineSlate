import api from '../../../app/api/api';
import { User } from '../Models/userType';

export const userApi = {
  login: async (formData: FormData): Promise<User> =>
    await api.post('users/login', formData),

  register: async (formData: FormData): Promise<User> =>
    await api.post('users/register', formData),

  me: async (): Promise<User> => await api.get('users/me'),
};

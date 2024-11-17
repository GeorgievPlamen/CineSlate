import axios from 'axios';
import { User } from '../Models/userType';

export const userApi = {
  login: async (formData: FormData): Promise<User> =>
    await axios.post('users/login', formData),

  register: async (formData: FormData): Promise<User> =>
    await axios.post('users/register', formData),

  me: async (): Promise<User> => await axios.get('users/me'),
};

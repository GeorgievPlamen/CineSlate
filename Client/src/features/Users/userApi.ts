import axios from 'axios';
import { User } from './userType';

export const userApi = {
  login: async (formData: FormData): Promise<User> =>
    await axios.post('users/login', formData),

  me: async (): Promise<User> => await axios.get('users/me'),
};

import axios from 'axios';
import { User } from './userType';

export const userApi = {
  login: async function login(formData: FormData): Promise<User> {
    try {
      return await axios.post('users/login', formData);
    } catch (error) {
      console.log(error);
      throw error;
    }
  },
};

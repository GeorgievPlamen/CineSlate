import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { User } from './userType';

interface UserState {
  user: User;
}

const initialState: UserState = {
  user: {
    email: '',
    firstName: '',
    id: '',
    lastName: '',
    token: '',
  },
};

export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser: (state, action: PayloadAction<User>) => {
      state.user = action.payload;
    },
  },
});

export const { setUser } = userSlice.actions;

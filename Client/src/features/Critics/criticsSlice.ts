import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { useAppDispatch, useAppSelector } from '../../store/reduxHooks';
import { useEffect } from 'react';
import { User } from '../Users/Models/userType';

export interface Critic {
  username: string;
  id: string;
  bio: string;
  pictureBase64: string;
}

interface CriticsState {
  critics: Critic[];
}

const initialState: CriticsState = {
  critics: [],
};

export const criticsSlice = createSlice({
  name: 'critics',
  initialState,
  reducers: {
    setCritics: (state, action: PayloadAction<Critic[]>) => {
      state.critics = action.payload;
    },
  },
});

export const { setCritics } = criticsSlice.actions;

export function useCritics() {
  return useAppSelector((state) => state.critics.critics);
}

export function useCriticById(id?: string) {
  const critics = useAppSelector((state) => state.critics.critics);

  return critics.find((c) => c.id === id);
}

export function useSetCritics(users?: User[]) {
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (users === undefined) return;

    dispatch(setCritics(users as Critic[]));
  }, [dispatch, users]);

  const dispatchCritics = (critics: Critic[]) => dispatch(setCritics(critics));

  return { dispatchCritics };
}

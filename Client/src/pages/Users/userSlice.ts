import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { User } from './Models/userType';
import { useAppDispatch, useAppSelector } from '../../app/store/reduxHooks';

interface UserState {
  user: User;
}

const initialState: UserState = {
  user: {
    email: '',
    username: '',
    token: '',
    id: '',
  },
};

export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser: (state, action: PayloadAction<User>) => {
      state.user = action.payload;
    },
    setBio: (state, action: PayloadAction<string>) => {
      state.user.bio = action.payload;
    },
    setAvatarBase64: (state, action: PayloadAction<string>) => {
      state.user.pictureBase64 = action.payload;
    },
  },
});

export function useUser() {
  return useAppSelector((store) => store.users.user);
}

export const { setUser, setBio, setAvatarBase64 } = userSlice.actions;

export function useDispatchUser() {
  const dispatch = useAppDispatch();

  const setMyBio = (bio: string) => dispatch(setBio(bio));
  const setMyAvatarBase64 = (avatarBase64: string) =>
    dispatch(setAvatarBase64(avatarBase64));

  return { setMyBio, setMyAvatarBase64 };
}

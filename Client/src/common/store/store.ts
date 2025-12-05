import { User } from '@/features/Users/Models/userType';
import { create } from 'zustand';

interface UserStore {
  user: User;
  setUser: (user: User) => void;
  setBio: (bio: string) => void;
  setAvatarBase64: (avatarBase64: string) => void;
}

export const useUserStore = create<UserStore>((set) => ({
  user: {
    email: '',
    username: '',
    token: '',
    id: '',
  },
  setUser: (user: User) => set({ user: user }),
  setBio: (bio: string) =>
    set((state) => ({ user: { ...state.user, bio: bio } })),
  setAvatarBase64: (avatarBase64: string) =>
    set((state) => ({ user: { ...state.user, pictureBase64: avatarBase64 } })),
}));

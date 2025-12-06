import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import { useUserStore } from '@/store/userStore';
import { UserResponse } from '@/features/Users/Models/UserResponse';

export default function useHandleUserResponse(userResponse: UserResponse) {
  const navigate = useNavigate();
  const setUser = useUserStore((state) => state.setUser);

  useEffect(() => {
    if (userResponse?.user) {
      setUser(userResponse.user);
      navigate('/');
    }
  }, [navigate, setUser, userResponse]);
}

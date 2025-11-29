import { useNavigate } from 'react-router-dom';
import { useAppDispatch } from '../store/reduxHooks';
import { useEffect } from 'react';
import { setUser } from '../features/Users/userSlice';
import { UserResponse } from '../features/Users/Models/UserResponse';

export default function useHandleUserResponse(userResponse: UserResponse) {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (userResponse?.user) {
      dispatch(setUser(userResponse.user));
      navigate('/');
    }
  }, [dispatch, navigate, userResponse]);
}

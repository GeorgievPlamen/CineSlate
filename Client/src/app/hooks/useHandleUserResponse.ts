import { useNavigate } from 'react-router-dom';
import { useAppDispatch } from '../store/reduxHooks';
import { useEffect } from 'react';
import { setUser } from '../../pages/Users/userSlice';
import { UserResponse } from '../../pages/Users/Models/UserResponse';

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

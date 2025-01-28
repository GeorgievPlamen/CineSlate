import { useEffect } from 'react';
import { LOCAL_REFRESH } from '../config';
import { useAppDispatch } from '../store/reduxHooks';
import { setUser } from '../../pages/Users/userSlice';
import { userApi } from '../../pages/Users/api/userApi';

function Background() {
  const dispatch = useAppDispatch();
  

  useEffect(() => {
    async function getMe() {
      const refreshToken = localStorage.getItem(LOCAL_REFRESH);

      if (!refreshToken) return;

      const user = await userApi.refresh(refreshToken);

      if (!user.refreshToken) return;

      localStorage.setItem(LOCAL_REFRESH, user.refreshToken);
      dispatch(setUser(user));
    }

    getMe();
  }, [dispatch]);

  return null;
}
export default Background;

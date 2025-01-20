import { useEffect } from 'react';
import { LOCAL_REFRESH } from '../config';
import { useAppDispatch } from '../store/reduxHooks';
import { setUser } from '../../pages/Users/userSlice';
import { userApi } from '../../pages/Users/api/userApi';

function Background() {
  const dispatch = useAppDispatch();
  const refreshToken = localStorage.getItem(LOCAL_REFRESH);

  useEffect(() => {
    if (refreshToken) {
      async function getMe() {
        const user = await userApi.refresh(refreshToken ?? '');
        dispatch(setUser(user));
      }

      getMe();
    }
  }, [dispatch, refreshToken]);

  return null;
}
export default Background;

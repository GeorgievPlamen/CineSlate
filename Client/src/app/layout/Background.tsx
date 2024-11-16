import { useEffect } from 'react';
import { SESSION_JWT } from '../config';
import { useAppDispatch } from '../store/reduxHooks';
import { userApiAxios } from '../../features/Users/userApiAxios';
import { setUser } from '../../features/Users/userSlice';

function Background() {
  const dispatch = useAppDispatch();
  const jwt = sessionStorage.getItem(SESSION_JWT);

  useEffect(() => {
    if (jwt) {
      async function getMe() {
        const user = await userApiAxios.me();
        dispatch(setUser(user));
      }

      getMe();
    }
  }, [dispatch, jwt]);

  return null;
}
export default Background;

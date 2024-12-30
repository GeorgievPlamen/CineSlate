import { useEffect } from 'react';
import { SESSION_JWT } from '../config';
import { useAppDispatch } from '../store/reduxHooks';
import { setUser } from '../../pages/Users/userSlice';
import { userApi } from '../../pages/Users/api/userApi';

function Background() {
  const dispatch = useAppDispatch();
  const jwt = sessionStorage.getItem(SESSION_JWT);

  useEffect(() => {
    if (jwt) {
      async function getMe() {
        const user = await userApi.me();
        console.log(user.email);
        dispatch(setUser({ ...user, token: jwt ?? '' }));
      }

      getMe();
    }
  }, [dispatch, jwt]);

  return null;
}
export default Background;

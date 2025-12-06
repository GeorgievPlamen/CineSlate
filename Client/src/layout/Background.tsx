import { useEffect, useRef } from 'react';
import { LOCAL_REFRESH } from '../config';
import { usersClient } from '../features/Users/api/usersClient';
import { useUserStore } from '@/store/userStore';

function Background() {
  const hasRefresh = useRef(false);
  const setUser = useUserStore((state) => state.setUser);

  useEffect(() => {
    async function getMe() {
      const refreshToken = localStorage.getItem(LOCAL_REFRESH);

      console.log(refreshToken);

      if (!refreshToken || hasRefresh.current) return;

      hasRefresh.current = true;
      const user = await usersClient.refresh(refreshToken);

      if (!user?.refreshToken) {
        localStorage.removeItem(LOCAL_REFRESH);
        return;
      }

      localStorage.setItem(LOCAL_REFRESH, user.refreshToken);
      setUser(user);
    }

    getMe();
  }, [setUser]);

  return null;
}
export default Background;

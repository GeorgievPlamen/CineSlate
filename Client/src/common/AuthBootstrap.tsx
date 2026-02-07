import { LOCAL_REFRESH } from '@/config';
import { usersClient } from '@/modules/Users/api/usersClient';
import { useUserStore } from '@/store/userStore';
import { ReactNode, useEffect } from 'react';
import logger from './logger';

interface Props {
  children: ReactNode;
}

export default function AuthBootstrap({ children }: Props) {
  useEffect(() => {
    const refreshToken = localStorage.getItem(LOCAL_REFRESH);

    if (!refreshToken) return;

    (async () => {
      try {
        const user = await usersClient.refresh(refreshToken);

        logger.log('Fetching refresh token.');
        logger.log(user);

        if (!user?.refreshToken) {
          localStorage.removeItem(LOCAL_REFRESH);
          return;
        }

        localStorage.setItem(LOCAL_REFRESH, user.refreshToken);
        useUserStore.setState(() => ({ user: user }));
      } catch (err) {
        localStorage.removeItem(LOCAL_REFRESH);
        logger.log(err);
      }
    })();
  }, []);

  return children;
}

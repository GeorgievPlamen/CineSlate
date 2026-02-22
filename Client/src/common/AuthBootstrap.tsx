import { LOCAL_JWT, LOCAL_REFRESH } from '@/config';
import { usersClient } from '@/modules/Users/api/usersClient';
import { useUserStore } from '@/store/userStore';
import { ReactNode, useEffect, useRef, useState } from 'react';
import logger from './logger';
import Loading from '@/components/Loading/Loading';

interface Props {
  children: ReactNode;
}

export default function AuthBootstrap({ children }: Props) {
  const hasRan = useRef(false);
  const [ready, setReady] = useState(false);

  useEffect(() => {
    if (hasRan.current) return;

    hasRan.current = true;
    const refreshToken = localStorage.getItem(LOCAL_REFRESH);

    if (!refreshToken) {
      setReady(true);
      return;
    };

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
        localStorage.setItem(LOCAL_JWT, user.token ?? '');
        useUserStore.setState(() => ({ user: user }));
      } catch (err) {
        localStorage.removeItem(LOCAL_REFRESH);
        localStorage.removeItem(LOCAL_JWT);

        logger.log(err);
      } finally {
        setReady(true);
      }
    })();
  }, []);

  return ready ? children : <Loading />
}

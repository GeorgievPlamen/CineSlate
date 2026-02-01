import { ReactNode, useEffect } from 'react';
import realtimeClient from './realtimeClient';
import { useUserStore } from '@/store/userStore';

interface Props {
  children: ReactNode;
}

export default function RealtimeProvider({ children }: Props) {
  const isAuthenticated = useUserStore((s) => s.user.username.length > 0);

  console.log('rendering RealtimeProvider');

  useEffect(() => {
    if (isAuthenticated) {
      (async () => {
        realtimeClient.init();
        await realtimeClient.start();
      })();
    } else {
      (async () => await realtimeClient.stop())();
    }
  }, [isAuthenticated]);

  return children;
}

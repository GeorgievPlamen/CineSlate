import { ReactNode, useEffect } from 'react';
import signalR from './signalR';
import { useUserStore } from '@/store/userStore';

interface Props {
  children: ReactNode;
}

export default function WebSocketProvider({ children }: Props) {
  const userToken = useUserStore((state) => state.user.token);

  useEffect(() => {
    if (userToken) {
      (async () => {
        signalR.init();
        await signalR.start();
      })();
    } else {
      (async () => await signalR.stop())();
    }
  }, [userToken]);

  return children;
}

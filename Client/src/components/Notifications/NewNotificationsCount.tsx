import { RealtimeEvents } from '@/common/Realtime/constants';
import useRealtimeEvent from '@/common/Realtime/useRealtimeEvent';
import { notificationsClient } from '@/modules/Notifications/api/notificationsClient';
import { useUserStore } from '@/store/userStore';
import { useQuery } from '@tanstack/react-query';
import { Dispatch, SetStateAction, useEffect } from 'react';

interface Props {
  newNotificationsCount: number;
  setNewNotificationsCount: Dispatch<SetStateAction<number>>;
}

export default function NewNotificationsCount({
  newNotificationsCount,
  setNewNotificationsCount,
}: Props) {
  const token = useUserStore((x) => x.user.token);

  const { data: initialNewNotificationsCount } = useQuery({
    queryKey: ['new-notifications-count'],
    queryFn: () => notificationsClient.getNewCount(),
    enabled: !!token,
  });

  useEffect(() => {
    const setInitialNewNotificationsCount = () => {
      setNewNotificationsCount(initialNewNotificationsCount ?? 0);
    };

    setInitialNewNotificationsCount();
  }, [initialNewNotificationsCount, setNewNotificationsCount]);

  const onNotify = () => {
    setNewNotificationsCount((prev) => prev + 1);
  };

  useRealtimeEvent(RealtimeEvents.Notify, onNotify);

  if (newNotificationsCount === 0) {
    return null;
  }

  return (
    <div className="text-xs rounded-full bg-red-500 size-4 text-center absolute bottom-0 right-0 m-0.5">
      {newNotificationsCount}
    </div>
  );
}

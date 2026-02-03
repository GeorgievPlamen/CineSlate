import { notificationsClient } from '@/modules/Notifications/api/notificationsClient';
import { useUserStore } from '@/store/userStore';
import { useQuery } from '@tanstack/react-query';
import { Bell } from 'lucide-react';

// TODO add dropdown
// TODO set all seen on click
// TODO fetch my notifications per 10
// TODO add load more to dropdown
// TODO sync realtime for new notifications
export default function NotificationsBell() {
  const token = useUserStore((x) => x.user.token);

  const { data } = useQuery({
    queryKey: ['new-notifications-count'],
    queryFn: () => notificationsClient.getNewCount(),
    enabled: !!token,
  });

  if (!token) return;

  return (
    <div
      className={'rounded hover:bg-primary active:bg-opacity-80 p-1 relative'}
    >
      <Bell size={26} />
      {data && data > 0 && (
        <div className="text-xs rounded-full bg-red-500 size-4 text-center absolute bottom-0 right-0 m-0.5">
          {data}
        </div>
      )}
    </div>
  );
}

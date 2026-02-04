import { notificationsClient } from '@/modules/Notifications/api/notificationsClient';
import { useUserStore } from '@/store/userStore';
import { useQuery } from '@tanstack/react-query';
import { Bell } from 'lucide-react';
import Dropdown from '../Dropdown';
import { useState } from 'react';
import NotificationItem from './NotificationItem';
import Button from '../Buttons/Button';

// TODO add dropdown
// TODO set all seen on click
// TODO fetch my notifications per 10
// TODO add load more to dropdown
// TODO sync realtime for new notifications
// TODO fix created on on BE
export default function NotificationsBell() {
  const token = useUserStore((x) => x.user.token);
  const [page, setPage] = useState(1);
  const [enabled, setEnabled] = useState(false);

  const { data } = useQuery({
    queryKey: ['new-notifications-count'],
    queryFn: () => notificationsClient.getNewCount(),
    enabled: !!token,
  });

  const { data: myNotificationsData } = useQuery({
    queryKey: ['my-notifications', page],
    queryFn: () => notificationsClient.getMy(page),
    enabled: enabled,
  });

  if (!token) return;
  console.log('render');

  const notifications = myNotificationsData?.values.map((n) => (
    <NotificationItem notification={n} key={n.id} />
  ));

  return (
    <Dropdown
      items={notifications}
      classNameTrigger="rounded hover:bg-primary active:bg-opacity-80 p-1 relative"
      classNameMenu="md:max-h-[50vh] md:w-80"
      onOpen={(open) => (open ? setEnabled(true) : setEnabled(false))}
      onLoadMore={() => console.log('load more')}
    >
      <Bell size={26} />
      {data && data > 0 && (
        <div className="text-xs rounded-full bg-red-500 size-4 text-center absolute bottom-0 right-0 m-0.5">
          {data}
        </div>
      )}
    </Dropdown>
  );
}

import { notificationsClient } from '@/modules/Notifications/api/notificationsClient';
import { useUserStore } from '@/store/userStore';
import { useInfiniteQuery, useMutation } from '@tanstack/react-query';
import { Bell } from 'lucide-react';
import Dropdown from '../Dropdown';
import renderNotification from './renderNotification';
import ToPagedData from '@/utils/toPagedData';
import NewNotificationsCount from './NewNotificationsCount';
import { useState } from 'react';

// TODO fix created on on BE
const NOTIFICATIONS_QUANTITY = 6;

export default function NotificationsBell() {
  const token = useUserStore((x) => x.user.token);
  const [newNotificationsCount, setNewNotificationsCount] = useState(0);

  const { mutateAsync: setAllSeen } = useMutation({
    mutationFn: () => notificationsClient.setAllSeen(),
    onSuccess: () => setNewNotificationsCount(0),
  });

  const {
    data: myNotificationsData,
    isFetching,
    fetchNextPage,
  } = useInfiniteQuery({
    queryKey: ['my-notifications'],
    queryFn: ({ pageParam }) =>
      notificationsClient.getMy(pageParam, NOTIFICATIONS_QUANTITY),
    initialPageParam: 1,
    getNextPageParam: (lastPage) => lastPage.currentPage + 1,
    select: (data) => ToPagedData(data),
    enabled: !!token,
  });

  if (!token) return;

  const notifications = myNotificationsData?.values.map((n) =>
    renderNotification(n)
  );

  const hasMoreNotifications =
    myNotificationsData?.totalCount &&
    notifications?.length &&
    myNotificationsData.totalCount > notifications?.length + 1;

  const hasAnyNotifications =
    myNotificationsData?.values && myNotificationsData?.values.length > 0;

  const onLoadMore = hasMoreNotifications ? () => fetchNextPage() : undefined;

  return (
    <Dropdown
      items={notifications}
      classNameTrigger="rounded hover:bg-primary active:bg-opacity-80 p-1 relative"
      classNameMenu="md:max-h-[50vh] md:w-80 w-screen"
      onOpen={(open) => (open ? setAllSeen() : null)}
      triggerDisabled={!hasAnyNotifications}
      onLoadMore={onLoadMore}
      isFetching={isFetching}
    >
      <Bell size={26} />
      <NewNotificationsCount
        newNotificationsCount={newNotificationsCount}
        setNewNotificationsCount={setNewNotificationsCount}
      />
    </Dropdown>
  );
}

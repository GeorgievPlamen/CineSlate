import { NotificationResponse } from '@/modules/Notifications/models/notificationModels';

interface Props {
  notification: NotificationResponse;
}

export default function NotificationItem({ notification }: Props) {
  // console.log(notification);
  return (
    <div>
      <p>test</p>
      <p>{notification.id}</p>
      <p>{notification.status}</p>
      <p>{notification.type}</p>
      <p>{notification.userId}</p>
    </div>
  );
}

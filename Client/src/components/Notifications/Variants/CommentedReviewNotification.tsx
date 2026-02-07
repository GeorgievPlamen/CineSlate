import { NotificationResponse } from '@/modules/Notifications/models/notificationModels';

interface Props {
  notification: NotificationResponse;
}

export default function CommentedReviewNotification({ notification }: Props) {
  return <div>commented review {notification.id}</div>;
}

import {
  NotificationResponse,
  NotificationType,
} from '@/modules/Notifications/models/notificationModels';
import LikedReviewNotification from './Variants/LikedReviewNotification';
import CommentedReviewNotification from './Variants/CommentedReviewNotification';

export default function renderNotification(notification: NotificationResponse) {
  switch (notification.type) {
    case NotificationType.LikedReview:
      return <LikedReviewNotification notification={notification} />;
    case NotificationType.CommentedReview:
      return <CommentedReviewNotification notification={notification} />;
  }
}

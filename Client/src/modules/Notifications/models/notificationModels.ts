export interface NotificationResponse {
  id: string;
  userId: string;
  type: NotificationType;
  status: NotificationStatus;
  data: Record<string, string>;
  createdOn: Date;
}

export enum NotificationType {
  LikedReview = 'LikedReview',
  CommentedReview = 'CommentedReview',
}

export enum NotificationStatus {
  New,
  Seen,
}

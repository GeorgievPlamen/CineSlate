export interface NotificationResponse {
  Id: string;
  UserId: string;
  Type: NotificationType;
  Status: NotificationStatus;
  Data: Record<string, string>;
  CreatedOn: Date;
}

export enum NotificationType {
  LikedReview,
  CommentedReview,
}

export enum NotificationStatus {
  New,
  Seen,
}

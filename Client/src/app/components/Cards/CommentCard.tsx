import { Comment } from '../../../pages/Reviews/models/review';

interface Props {
  comment: Comment;
}

export default function CommentCard({ comment }: Props) {
  console.log(comment.fromUser.onlyName);
  console.log(comment.fromUser.value);
  console.log(comment.fromUserId.value);
  console.log(comment.value);
  return <div>CommentCard</div>;
}

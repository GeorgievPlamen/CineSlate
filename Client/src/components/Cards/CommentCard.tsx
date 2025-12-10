import { Comment } from '@/modules/Review/models/review';
import { BACKUP_PROFILE } from '@/config';
import { useQuery } from '@tanstack/react-query';
import { Link } from '@tanstack/react-router';
import { usersClient } from '@/modules/Users/api/usersClient';

interface Props {
  comment: Comment;
}

export default function CommentCard({ comment }: Props) {
  const { data: usersData } = useQuery({
    queryKey: ['getUsersByIds', comment.fromUserId.value],
    queryFn: () => usersClient.getUsersByIds([comment.fromUserId.value ?? '']),
  });

  return (
    <div className="my-5 flex rounded-2xl border border-grey bg-background p-1">
      <img
        src={
          usersData?.[0].pictureBase64 &&
          usersData?.[0].pictureBase64.length > 0
            ? usersData?.[0].pictureBase64
            : BACKUP_PROFILE
        }
        alt="profile-pic"
        className="h-20 w-20 rounded-full object-cover"
      />
      <div className="mx-4 my-2 w-80">
        <div className="mb-2 flex justify-between">
          <p className="text-xl">
            <Link className={'font-heading hover:text-primary'} to={`/critics`}>
              {usersData?.[0].username.split('#')[0]}
            </Link>
            <span className="text-xs text-muted-foreground">
              {' '}
              #{usersData?.[0].username.split('#')[1]}
            </span>
          </p>
        </div>
        <p className="min-h-10 font-primary">{comment.value}</p>
      </div>
    </div>
  );
}

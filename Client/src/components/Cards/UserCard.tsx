import { BACKUP_PROFILE } from '@/config';
import { Link } from '@tanstack/react-router';
import { base64ToImage } from '@/lib/utils';
import { User } from '@/modules/Users/Models/userType';

interface Props {
  user: User;
  className?: string;
}

export default function UserCard({ user, className }: Props) {
  const username = user.username?.split('#');

  return (
    <Link
      to={'/critics/$id'}
      params={{ id: user.id ?? '' }}
      className={
        'flex rounded-2xl border border-grey bg-background p-1 hover:border-primary ' +
        className
      }
    >
      <img
        src={
          user?.pictureBase64?.length && user?.pictureBase64?.length > 0
            ? base64ToImage(user.pictureBase64)
            : BACKUP_PROFILE
        }
        alt="profile-pic"
        className="h-20 w-20 rounded-full object-cover"
      />
      <div className="mx-4 my-2 w-80">
        <div className="mb-2 flex flex-col justify-between">
          <p className="text-xl">
            {username[0]}
            <span className="text-xs text-muted-foreground">
              {' '}
              #{username[1]}
            </span>
          </p>
          <p className="font-primary text-sm text-muted-foreground">
            {user?.bio}
          </p>
        </div>
      </div>
    </Link>
  );
}

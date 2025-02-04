import { NavLink } from 'react-router-dom';
import { User } from '../../../pages/Users/Models/userType';
import { BACKUP_PROFILE } from '../../config';

interface Props {
  user: User;
  className?: string;
}

export default function UserCard({ user, className }: Props) {
  const username = user.username?.split('#');

  return (
    <NavLink
      to={`${user.id}`}
      className={
        'flex rounded-2xl border border-grey bg-background p-1 hover:border-primary ' +
        className
      }
    >
      <img
        src={
          user?.pictureBase64?.length && user?.pictureBase64?.length > 0
            ? user.pictureBase64
            : BACKUP_PROFILE
        }
        alt="profile-pic"
        className="h-20 w-20 rounded-full object-cover"
      />
      <div className="mx-4 my-2 w-80">
        <div className="mb-2 flex flex-col justify-between">
          <p className="text-xl">
            {username[0]}
            <span className="text-xs text-grey"> #{username[1]}</span>
          </p>
          <p className="font-roboto text-sm text-grey">{user?.bio}</p>
        </div>
      </div>
    </NavLink>
  );
}

import { NavLink } from 'react-router-dom';
import { User } from '../../../pages/Users/Models/userType';

interface Props {
  user: User;
  className?: string;
}

export default function UserCard({ user, className }: Props) {
  const username = user.username?.split('#');

  console.log(user.id);

  return (
    <NavLink
      to={`${username[0]}.${username[1]}.${user.id}`}
      className={
        'flex rounded-2xl border border-grey bg-background p-1 ' + className
      }
    >
      <img
        src="https://freesvg.org/img/abstract-user-flat-3.png"
        alt="profile-pic"
        className="h-20 w-20"
      />
      <div className="mx-4 my-2 w-80">
        <div className="mb-2 flex justify-between">
          <p className="text-xl">
            {username[0]}
            <span className="text-xs text-grey"> #{username[1]}</span>
          </p>
        </div>
      </div>
    </NavLink>
  );
}

import { ReactNode, useState } from 'react';
import { NavLink } from 'react-router-dom';
import { useUserStore } from '@/store/userStore';

interface Props {
  children: ReactNode;
}

export default function DropdownMobile({ children }: Props) {
  const [isListActive, setIsListActive] = useState(false);
  const user = useUserStore((state) => state.user);

  return (
    <div className="relative mx-2">
      <button
        onClick={() => setIsListActive(!isListActive)}
        className={
          'active:bg-opcaity-80 flex rounded px-2 py-1 text-foreground hover:bg-primary ' +
          `${isListActive ? 'outline outline-foreground' : null}`
        }
      >
        {children}
      </button>
      <ul
        className={`${
          isListActive
            ? 'w-content absolute right-0 mt-4 flex flex-col items-center gap-2 rounded-lg bg-background px-2 py-1 font-bold text-foreground outline transition-transform'
            : 'hidden'
        }`}
      >
        <li>
          <NavLink
            onClick={() => setIsListActive(false)}
            to="/"
            className={({ isActive }) => ` ${isActive ? 'underline' : null}`}
          >
            Home
          </NavLink>
        </li>
        <li>
          <NavLink
            onClick={() => setIsListActive(false)}
            to="/movies"
            className={({ isActive }) => ` ${isActive ? 'underline' : null}`}
          >
            Movies
          </NavLink>
        </li>
        <li>
          <NavLink
            onClick={() => setIsListActive(false)}
            to="/critics"
            className={({ isActive }) => ` ${isActive ? 'underline' : null}`}
          >
            Critics
          </NavLink>
        </li>
        {user?.username?.length > 0 ? (
          <>
            <li className="text-nowrap hover:underline">
              <NavLink
                onClick={() => setIsListActive(false)}
                to="/my-details"
                className={({ isActive }) =>
                  ` ${isActive ? 'underline' : null}`
                }
              >
                My Details
              </NavLink>
            </li>
            <li className="text-nowrap hover:underline">
              <a
                href="/"
                onClick={() => {
                  localStorage.clear();
                }}
              >
                Sign out
              </a>
            </li>
          </>
        ) : (
          <NavLink
            onClick={() => setIsListActive(false)}
            to="/login"
            className={({ isActive }) => ` ${isActive ? 'underline' : null}`}
          >
            Sign in
          </NavLink>
        )}
      </ul>
    </div>
  );
}

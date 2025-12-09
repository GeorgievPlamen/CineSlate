import { ReactNode, useState } from 'react';
import { NavLink } from 'react-router-dom';

interface Props {
  children: ReactNode;
}

export default function DropdownButton({ children }: Props) { // TODO
  const [isListActive, setIsListActive] = useState(false);

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
            ? 'absolute mt-2 flex w-full flex-col items-center gap-2 rounded-lg bg-background px-2 py-1 font-bold text-foreground outline transition-transform'
            : 'hidden'
        }`}
      >
        <li className="text-nowrap hover:underline">
          <NavLink
            to="/my-details"
            className={({ isActive }) => ` ${isActive ? 'underline' : null}`}
            onClick={() => setIsListActive(false)}
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
      </ul>
    </div>
  );
}

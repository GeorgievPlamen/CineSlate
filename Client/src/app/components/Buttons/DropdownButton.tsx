import { useState } from 'react';
import { NavLink } from 'react-router-dom';

interface Props {
  children: JSX.Element;
}

export default function DropdownButton({ children }: Props) {
  const [isListActive, setIsListActive] = useState(false);

  return (
    <div className="relative mx-2">
      <button
        onClick={() => setIsListActive(!isListActive)}
        className={
          'active:bg-opcaity-80 flex rounded px-2 py-1 text-whitesmoke hover:bg-primary ' +
          `${isListActive ? 'outline outline-1 outline-whitesmoke' : null}`
        }
      >
        {children}
      </button>
      <ul
        className={`${
          isListActive
            ? 'absolute mt-2 flex flex-col gap-2 rounded-lg bg-background px-2 py-1 font-bold text-whitesmoke outline outline-1 transition-transform'
            : 'hidden'
        }`}
      >
        <li className="text-nowrap">
          <NavLink
            to="/reviews"
            className={({ isActive }) => ` ${isActive ? 'underline' : null}`}
            onClick={() => setIsListActive(false)}
          >
            Reviews
          </NavLink>
        </li>
        <li className="text-nowrap">
          <a
            href="/"
            onClick={() => {
              sessionStorage.clear();
            }}
          >
            Sign out
          </a>
        </li>
      </ul>
    </div>
  );
}

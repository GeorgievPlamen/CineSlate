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
          'hover:bg-primary-hover active:bg-primary-active flex rounded px-2 py-1 text-whitesmoke ' +
          `${isListActive ? 'bg-primary-selected' : null}`
        }
      >
        {children}
      </button>
      <ul
        className={`${
          isListActive
            ? 'text-primary-selected absolute mt-2 flex flex-col gap-2 rounded-lg bg-whitesmoke px-2 py-1 font-bold transition-transform'
            : 'hidden'
        }`}
      >
        <li className="text-nowrap">
          <NavLink
            to="/reviews"
            className={({ isActive }) => ` ${isActive ? 'underline' : null}`}
          >
            Reviews
          </NavLink>
        </li>
        <li className="text-nowrap">
          <button onClick={() => sessionStorage.clear()}>Sign out</button>
        </li>
      </ul>
    </div>
  );
}

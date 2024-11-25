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
          'flex rounded px-2 py-1 text-whitesmoke hover:bg-indigo-700 active:bg-indigo-500 ' +
          `${isListActive ? 'bg-indigo-700' : null}`
        }
      >
        {children}
      </button>
      <ul
        className={`${
          isListActive
            ? 'absolute mt-2 flex flex-col gap-2 rounded-lg bg-whitesmoke px-2 py-1 font-bold text-indigo-800 transition-transform'
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

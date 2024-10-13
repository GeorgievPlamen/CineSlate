import { NavLink } from 'react-router-dom';
import CineSlateLogo from '../assets/images/cineslateLogo.png';
import { useState } from 'react';
import { useAppSelector } from '../store/reduxHooks';
import { MagnifyingGlassIcon } from '@heroicons/react/16/solid';

function Header() {
  const [isBouncing, setIsBouncing] = useState(false);
  const user = useAppSelector((state) => state.users.user);

  const handleBounce = () => {
    setIsBouncing(true);
    setTimeout(() => setIsBouncing(false), 1500);
  };

  return (
    <header className="flex border bg-indigoTropical py-2 shadow-xl dark:shadow-light">
      <nav className="flex w-full items-center justify-evenly">
        <NavLink to="/" className="flex w-min items-center justify-center">
          <img
            src={CineSlateLogo}
            alt="Cineslate Logo"
            className={`mx-10 w-16 ${isBouncing ? 'animate-bounce' : ''}`}
            onClick={handleBounce}
          />
        </NavLink>
        <div className="w-fulm 0 relative mx-2 flex w-full max-w-md items-center rounded-full bg-whitesmoke">
          <input
            placeholder="Search Movies"
            type="search"
            name="search"
            className="h-8 flex-grow rounded-full bg-whitesmoke pl-2 text-grayPayns focus:outline-none"
          />
          <MagnifyingGlassIcon className="absolute right-2 size-6 cursor-pointer rounded-full bg-whitesmoke text-gray-400" />
        </div>
        <ul className="flex gap-4">
          <li>
            <NavLink
              to="/movies"
              className={({ isActive }) =>
                'rounded px-4 py-1 text-whitesmoke hover:bg-indigo-600 active:bg-indigo-500' +
                ` ${isActive ? 'bg-indigo-700' : null}`
              }
            >
              Movies
            </NavLink>
          </li>
          <li>
            <NavLink
              to="/critics"
              className={({ isActive }) =>
                'rounded px-2 py-1 text-whitesmoke hover:bg-indigo-600 active:bg-indigo-500' +
                ` ${isActive ? 'bg-indigo-700' : null}`
              }
            >
              Critics
            </NavLink>
          </li>
          <li>
            <NavLink
              to="/stories"
              className={({ isActive }) =>
                'rounded px-2 py-1 text-whitesmoke hover:bg-indigo-600 active:bg-indigo-500' +
                ` ${isActive ? 'bg-indigo-700' : null}`
              }
            >
              Stories
            </NavLink>
          </li>
          <li>
            <NavLink
              to="/quizzes"
              className={({ isActive }) =>
                'rounded px-2 py-1 text-whitesmoke hover:bg-indigo-600 active:bg-indigo-500' +
                ` ${isActive ? 'bg-indigo-700' : null}`
              }
            >
              Quizzes
            </NavLink>
          </li>
        </ul>
        {user?.firstName?.length > 0 ? (
          <p className="mx-2 rounded px-2 py-1 text-whitesmoke hover:bg-indigo-700 active:bg-indigo-500">
            {user?.firstName}
          </p>
        ) : (
          <NavLink
            to="login"
            className={({ isActive }) =>
              'mx-2 rounded px-2 py-1 text-whitesmoke hover:bg-indigo-700 active:bg-indigo-500' +
              ` ${isActive ? 'bg-indigo-700' : null}`
            }
          >
            Sign in
          </NavLink>
        )}
      </nav>
    </header>
  );
}
export default Header;

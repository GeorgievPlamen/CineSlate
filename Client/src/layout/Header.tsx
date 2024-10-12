import { NavLink } from 'react-router-dom';
import CineSlateLogo from '../assets/images/cineslateLogo.png';
import { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import { useAppSelector } from '../store/reduxHooks';

function Header() {
  const [isBouncing, setIsBouncing] = useState(false);
  const user = useAppSelector((state) => state.users.user);

  const handleBounce = () => {
    setIsBouncing(true);
    setTimeout(() => setIsBouncing(false), 1500);
  };

  return (
    <header className="bg-indigoTropical dark:shadow-light flex border py-2 shadow-xl">
      <nav className="flex w-full items-center justify-evenly">
        <NavLink to="/" className="flex w-min items-center justify-center">
          <img
            src={CineSlateLogo}
            alt="Cineslate Logo"
            className={`mx-10 w-16 ${isBouncing ? 'animate-bounce' : ''}`}
            onClick={handleBounce}
          />
        </NavLink>
        <div className="bg-whitesmoke w-fulm 0 relative mx-2 flex w-full max-w-md items-center rounded-full">
          <input
            placeholder="Search Movies"
            type="search"
            name="search"
            className="bg-whitesmoke text-grayPayns h-8 flex-grow rounded-full pl-2 focus:outline-none"
          />
          <FontAwesomeIcon
            icon={faMagnifyingGlass}
            className="bg-whitesmoke absolute right-0 cursor-pointer rounded-full pr-2 text-gray-400"
          />
        </div>
        <ul className="flex gap-4">
          <li>
            <NavLink
              to="/movies"
              className={({ isActive }) =>
                'text-whitesmoke rounded px-4 py-1 hover:bg-indigo-600 active:bg-indigo-500' +
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
                'text-whitesmoke rounded px-2 py-1 hover:bg-indigo-600 active:bg-indigo-500' +
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
                'text-whitesmoke rounded px-2 py-1 hover:bg-indigo-600 active:bg-indigo-500' +
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
                'text-whitesmoke rounded px-2 py-1 hover:bg-indigo-600 active:bg-indigo-500' +
                ` ${isActive ? 'bg-indigo-700' : null}`
              }
            >
              Quizzes
            </NavLink>
          </li>
        </ul>
        {user?.firstName.length > 0 ? (
          <p className="text-whitesmoke mx-2 rounded px-2 py-1 hover:bg-indigo-700 active:bg-indigo-500">
            {user?.firstName}
          </p>
        ) : (
          <NavLink
            to="login"
            className={({ isActive }) =>
              'text-whitesmoke mx-2 rounded px-2 py-1 hover:bg-indigo-700 active:bg-indigo-500' +
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

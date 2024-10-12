import { NavLink } from 'react-router-dom';
import CineSlateLogo from '../assets/images/cineslateLogo.png';
import { useState } from 'react';

function Header() {
  const [isBouncing, setIsBouncing] = useState(false);
  const handleBounce = () => {
    setIsBouncing(true);
    setTimeout(() => setIsBouncing(false), 1500);
  };

  return (
    <header className="bg-indigoTropical dark:shadow-light flex justify-center py-2 shadow-xl">
      <nav className="flex w-2/3 items-center justify-around">
        <NavLink
          to="/"
          className="flex w-min items-center justify-center border"
        >
          <img
            src={CineSlateLogo}
            alt="Cineslate Logo"
            className={`mx-10 w-16 ${isBouncing ? 'animate-bounce' : ''}`}
            onClick={handleBounce}
          />
        </NavLink>
        <input placeholder="Search Movies" type="search" name="search" />
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
        <NavLink
          to="login"
          className={({ isActive }) =>
            'text-whitesmoke rounded px-2 py-1 hover:bg-indigo-700 active:bg-indigo-500' +
            ` ${isActive ? 'bg-indigo-700' : null}`
          }
        >
          Login
        </NavLink>
      </nav>
    </header>
  );
}
export default Header;

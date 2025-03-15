import { NavLink, useNavigate } from 'react-router-dom';
import CineSlateLogo from '../assets/images/cineslateLogo.png';
import { useCallback, useEffect, useState } from 'react';
import { useAppSelector } from '../store/reduxHooks';
import { MagnifyingGlassIcon } from '@heroicons/react/16/solid';
import DropdownButton from '../components/Buttons/DropdownButton';
import { BACKUP_PROFILE } from '../config';
import useDebounce from '../hooks/useDebounce';

function Header() {
  const [isBouncing, setIsBouncing] = useState(false);
  const user = useAppSelector((state) => state.users.user);

  const [searchTerm, setSearchTerm] = useState<string>();
  const debouncedSearchTerm = useDebounce(searchTerm);

  const navigate = useNavigate();

  const handleBounce = () => {
    setIsBouncing(true);
    setTimeout(() => setIsBouncing(false), 1500);
  };

  const navigateToMovies = useCallback(
    () => navigate('/movies?search=' + debouncedSearchTerm),
    [debouncedSearchTerm, navigate]
  );

  useEffect(() => {
    navigateToMovies();
  }, [debouncedSearchTerm, navigate, navigateToMovies]);

  return (
    <header className="fixed z-10 flex w-full bg-background py-2 shadow shadow-dark">
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
            onChange={(e) => setSearchTerm(e.target.value)}
            placeholder="Search Movies"
            type="search"
            name="search"
            className="h-8 flex-grow rounded-full bg-whitesmoke pl-2 text-placeholder focus:outline-none"
            onKeyDown={(e) => {
              if (e.key !== 'Enter') return;
              navigateToMovies();
            }}
          />
          <MagnifyingGlassIcon
            onClick={navigateToMovies}
            className="absolute right-2 size-6 cursor-pointer rounded-full bg-whitesmoke text-gray-400"
          />
        </div>
        <ul className="flex gap-4">
          <li>
            <NavLink
              to="/movies"
              className={({ isActive }) =>
                'rounded px-2 py-1 text-whitesmoke hover:bg-primary active:bg-opacity-80' +
                ` ${isActive ? 'outline outline-1 outline-whitesmoke' : null}`
              }
            >
              Movies
            </NavLink>
          </li>
          <li>
            <NavLink
              to="/critics"
              className={({ isActive }) =>
                'rounded px-2 py-1 text-whitesmoke hover:bg-primary active:bg-opacity-80' +
                ` ${isActive ? 'outline outline-1 outline-whitesmoke' : null}`
              }
            >
              Critics
            </NavLink>
          </li>
        </ul>
        {user?.username?.length > 0 ? (
          <DropdownButton>
            <div className="flex items-center gap-2">
              {user?.username?.split('#')[0]}
              <img
                src={
                  user?.pictureBase64?.length && user?.pictureBase64?.length > 0
                    ? user.pictureBase64
                    : BACKUP_PROFILE
                }
                alt="profile-pic"
                className="h-8 w-8 rounded-full object-cover"
              />
            </div>
          </DropdownButton>
        ) : (
          <NavLink
            to="login"
            className={({ isActive }) =>
              'mx-2 rounded px-2 py-1 text-whitesmoke hover:bg-primary active:bg-opacity-80' +
              ` ${isActive ? 'outline outline-1 outline-whitesmoke' : null}`
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

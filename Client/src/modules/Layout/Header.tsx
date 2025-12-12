import CineSlateLogo from '@/assets/images/cineslateLogo.png';
import { useCallback, useEffect, useState } from 'react';
import { MagnifyingGlassIcon } from '@heroicons/react/16/solid';
import { useUserStore } from '@/store/userStore';
import useDebounce from '@/hooks/useDebounce';
import { Link, useNavigate } from '@tanstack/react-router';
import { BACKUP_PROFILE } from '@/config';
import BarsIcon from '@/Icons/BarsIcon';
import Dropdown from '@/components/Dropdown';
import { User } from '../Users/Models/userType';

function Header() {
  const [isBouncing, setIsBouncing] = useState(false);
  const [searchTerm, setSearchTerm] = useState<string>();
  const debouncedSearchTerm = useDebounce(searchTerm);
  const user = useUserStore((state) => state.user);

  const navigate = useNavigate();
  const handleBounce = () => {
    setIsBouncing(true);
    setTimeout(() => setIsBouncing(false), 1500);
  };

  const navigateToMovies = useCallback(() => {
    if (debouncedSearchTerm === undefined) return;

    if (debouncedSearchTerm?.length > 0) {
      navigate({
        to: '/movies',
        search: {
          search: debouncedSearchTerm,
        },
      });
    } else navigate({ to: '/movies' });
  }, [debouncedSearchTerm, navigate]);

  useEffect(() => {
    if (debouncedSearchTerm !== undefined) navigateToMovies();
  }, [debouncedSearchTerm, navigate, navigateToMovies]);

  return (
    <header className="fixed z-50 flex w-full bg-background py-2 shadow shadow-dark">
      <nav className="hidden w-full items-center justify-evenly md:flex">
        <Link to="/" className="flex w-min items-center justify-center">
          <img
            src={CineSlateLogo}
            alt="Cineslate Logo"
            className={`mx-10 w-16 ${isBouncing ? 'animate-bounce' : ''}`}
            onClick={handleBounce}
          />
        </Link>
        <div className="0 relative mx-2 flex w-full max-w-md items-center rounded-full bg-foreground">
          <input
            onChange={(e) => setSearchTerm(e.target.value)}
            placeholder="Search Movies"
            type="search"
            name="search"
            className="h-8 grow rounded-full bg-foreground pl-2 text-accent focus:outline-none"
            onKeyDown={(e) => {
              if (e.key !== 'Enter') return;
              navigateToMovies();
            }}
          />
          <MagnifyingGlassIcon
            onClick={navigateToMovies}
            className="absolute right-2 size-6 cursor-pointer rounded-full bg-foreground text-gray-400"
          />
        </div>
        <ul className="flex gap-4">
          <li>
            <Link
              to="/movies"
              activeProps={{
                className: 'outline  outline-foreground',
              }}
              className={
                'mx-2 rounded px-2 py-1 text-foreground hover:bg-primary active:bg-opacity-80'
              }
            >
              Movies
            </Link>
          </li>
          <li>
            <Link
              to="/critics"
              activeProps={{
                className: 'outline  outline-foreground',
              }}
              className={
                'mx-2 rounded px-2 py-1 text-foreground hover:bg-primary active:bg-opacity-80'
              }
            >
              Critics
            </Link>
          </li>
        </ul>
        {user?.username?.length > 0 ? (
          <Dropdown items={DropdownItems}>
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
          </Dropdown>
        ) : (
          <Link
            to="/login"
            activeProps={{
              className: 'outline  outline-foreground',
            }}
            className={
              'mx-2 rounded px-2 py-1 text-foreground hover:bg-primary active:bg-opacity-80'
            }
          >
            Sign in
          </Link>
        )}
      </nav>
      <nav className="flex w-full items-center justify-between md:hidden">
        <div className="0 relative mx-2 flex w-2/3 max-w-md items-center rounded-full bg-foreground">
          <input
            onChange={(e) => setSearchTerm(e.target.value)}
            placeholder="Search Movies"
            type="search"
            name="search"
            className="h-8 grow rounded-full bg-foreground pl-2 text-accent focus:outline-none"
            onKeyDown={(e) => {
              if (e.key !== 'Enter') return;
              navigateToMovies();
            }}
          />
        </div>
        <Dropdown items={getMobileDropdownMenuItems(user)}>
          <BarsIcon />
        </Dropdown>
      </nav>
    </header>
  );
}
export default Header;

const DropdownItems = [
  <Link
    to="/my-details"
    className="text-nowrap hover:underline w-full"
    activeProps={{
      className: 'underline',
    }}
  >
    My Details
  </Link>,
  <a
    href="/"
    className="text-nowrap hover:underline w-full"
    onClick={() => {
      localStorage.clear();
    }}
  >
    Sign out
  </a>,
];

const getMobileDropdownMenuItems = (user?: User) => {
  const items = [
    <Link
      to="/"
      className="text-nowrap hover:underline w-full"
      activeProps={{
        className: 'underline',
      }}
    >
      Home
    </Link>,
    <Link
      to="/movies"
      className="text-nowrap hover:underline w-full"
      activeProps={{
        className: 'underline',
      }}
    >
      Movies
    </Link>,
    <Link
      to="/critics"
      className="text-nowrap hover:underline w-full"
      activeProps={{
        className: 'underline',
      }}
    >
      Critics
    </Link>,
  ];

  if (user?.username?.length && user?.username?.length > 0) {
    items.push(
      <Link
        to="/my-details"
        className="text-nowrap hover:underline w-full"
        activeProps={{
          className: 'underline',
        }}
      >
        My Details
      </Link>,
      <a
        href="/"
        className="text-nowrap hover:underline w-full"
        onClick={() => {
          localStorage.clear();
        }}
      >
        Sign out
      </a>
    );
  } else {
    items.push(
      <Link
        to="/login"
        className="text-nowrap hover:underline w-full"
        activeProps={{
          className: 'underline',
        }}
      >
        Sign in
      </Link>
    );
  }

  return items;
};

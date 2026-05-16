import { useCallback, useEffect, useState } from 'react';
import { useUserStore } from '@/store/userStore';
import useDebounce from '@/hooks/useDebounce';
import { Link, useLocation, useNavigate } from '@tanstack/react-router';
import { BACKUP_PROFILE } from '@/config';
import Dropdown from '@/components/Dropdown';
import { User } from '../Users/Models/userType';
import { base64ToImage } from '@/lib/utils';
import { Search, TextAlignJustifyIcon } from 'lucide-react';
import NotificationsBell from '@/components/Notifications/NotificationsBell';
import HeaderLink from '@/components/HeaderLink';

function Header() {
  const [searchTerm, setSearchTerm] = useState<string>();
  const debouncedSearchTerm = useDebounce(searchTerm);
  const user = useUserStore((state) => state.user);
  const { pathname } = useLocation();

  console.log(pathname);

  const navigate = useNavigate();

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
    <header className="sticky top-0 z-50 flex w-full bg-background border-b border-b-border">
      <nav className="hidden w-full items-center justify-between md:flex min-h-18 px-2 lg:px-6">
        <Link to="/" className="flex w-min justify-center">
          <h1 className="text-sm lg:text-xl pl-2 xl:text-2xl xl:pl-4 font-bold tracking-wider font-heading text-start">
            <span>CINE</span>
            <span className="text-secondary">SLATE</span>
          </h1>
        </Link>
        <div className="0 relative mx-2 flex lg:w-full max-w-md  items-center rounded-full bg-foreground">
          <input
            onChange={(e) => setSearchTerm(e.target.value)}
            placeholder="Search Movies"
            type="text"
            name="search"
            className="h-8 grow rounded-full bg-foreground pl-2 text-accent focus:outline-none"
            onKeyDown={(e) => {
              if (e.key !== 'Enter') return;
              navigateToMovies();
            }}
          />
          <Search
            onClick={navigateToMovies}
            className="absolute right-2 size-6 cursor-pointer rounded-full bg-foreground text-gray-400"
          />
        </div>
        <ul className="flex gap-2 items-center relative h-full">
          <li className="h-full">
            <HeaderLink label="Movies" linkOptions={{ to: '/movies' }} />
          </li>
          <li className="h-full">
            <HeaderLink label="Critics" linkOptions={{ to: '/critics' }} />
          </li>
          <li className="h-full">
            <HeaderLink label="Watchlist" linkOptions={{ to: '/watchlist' }} />
          </li>
          <li className="flex gap-4 items-center">
            <NotificationsBell />
          </li>
          {user?.username?.length > 0 ? (
            <li>
              <Dropdown items={DropdownItems}>
                <div className="flex items-center gap-2 rounded px-2 py-1 text-foreground hover:bg-primary w-full cursor-pointer">
                  {user?.username?.split('#')[0]}
                  <img
                    src={
                      user?.pictureBase64?.length &&
                      user?.pictureBase64?.length > 0
                        ? base64ToImage(user.pictureBase64)
                        : BACKUP_PROFILE
                    }
                    alt="profile-pic"
                    className="h-8 w-8 rounded-full object-cover"
                  />
                </div>
              </Dropdown>
            </li>
          ) : (
            <li className="h-full">
              <HeaderLink label="Sign in" linkOptions={{ to: '/login' }} />
            </li>
          )}
        </ul>
      </nav>
      <nav className="flex w-full items-center justify-between md:hidden min-h-18 px-6">
        <div className="0 relative mx-2 flex w-2/3 max-w-md items-center rounded-full bg-foreground">
          <input
            onChange={(e) => setSearchTerm(e.target.value)}
            placeholder="Search Movies"
            type="text"
            name="search"
            className="h-8 grow rounded-full bg-foreground pl-2 text-accent focus:outline-none"
            onKeyDown={(e) => {
              if (e.key !== 'Enter') return;
              navigateToMovies();
            }}
          />
          <Search
            onClick={navigateToMovies}
            className="absolute right-2 size-6 cursor-pointer rounded-full bg-foreground text-gray-400"
          />
        </div>
        <NotificationsBell />
        <Dropdown items={getMobileDropdownMenuItems(user)}>
          <div className="rounded px-2 py-1 hover:bg-primary cursor-pointer">
            <TextAlignJustifyIcon />
          </div>
        </Dropdown>
      </nav>
    </header>
  );
}
export default Header;

const DropdownItems = [
  <Link
    to="/my-details"
    key="/my-details"
    className="text-nowrap hover:underline w-full"
    activeProps={{
      className: 'underline',
    }}
  >
    My Details
  </Link>,
  <a
    href="/"
    key="/"
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
      key="/"
      className="text-nowrap hover:underline w-full"
      activeProps={{
        className: 'underline',
      }}
    >
      Home
    </Link>,
    <Link
      to="/movies"
      key="/movies"
      className="text-nowrap hover:underline w-full"
      activeProps={{
        className: 'underline',
      }}
    >
      Movies
    </Link>,
    <Link
      to="/critics"
      key="/critics"
      className="text-nowrap hover:underline w-full"
      activeProps={{
        className: 'underline',
      }}
    >
      Critics
    </Link>,
    <Link
      to="/watchlist"
      key="/watchlist"
      className="text-nowrap hover:underline w-full"
      activeProps={{
        className: 'underline',
      }}
    >
      Watchlist
    </Link>,
  ];

  if (user?.username?.length && user?.username?.length > 0) {
    items.push(
      <Link
        to="/my-details"
        key="/my-details"
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
        key="/login"
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

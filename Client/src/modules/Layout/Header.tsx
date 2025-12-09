import CineSlateLogo from '@/assets/images/cineslateLogo.png';
import { useCallback, useEffect, useState } from 'react';
import { MagnifyingGlassIcon } from '@heroicons/react/16/solid';

import { useUserStore } from '@/store/userStore';
import useDebounce from '@/hooks/useDebounce';
import { Link, useNavigate } from '@tanstack/react-router';
import DropdownButton from '@/components/Buttons/DropdownButton';
import DropdownMobile from '@/components/Buttons/DropdownMobile';
import { BACKUP_PROFILE } from '@/config';
import BarsIcon from '@/Icons/BarsIcon';
import Dropdown from '@/components/Dropdown';

function Header() {
  const [isBouncing, setIsBouncing] = useState(false);
  const user = useUserStore((state) => state.user);

  const [searchTerm, setSearchTerm] = useState<string>();
  const debouncedSearchTerm = useDebounce(searchTerm);

  const navigate = useNavigate();

  const handleBounce = () => {
    setIsBouncing(true);
    setTimeout(() => setIsBouncing(false), 1500);
  };

  const navigateToMovies = useCallback(() => {
    if (debouncedSearchTerm === undefined) return;

    let search;

    if (debouncedSearchTerm?.length > 0) {
      search = '?search=' + debouncedSearchTerm;
      navigate({ to: '/movies', search: search });
    } else navigate({ to: '/movies' });
  }, [debouncedSearchTerm, navigate]);

  useEffect(() => {
    if (debouncedSearchTerm !== undefined) navigateToMovies();
  }, [debouncedSearchTerm, navigate, navigateToMovies]);

  return (
    <header className="fixed z-10 flex w-full bg-background py-2 shadow shadow-dark">
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
        <Dropdown />
        {user?.username?.length > 0 ? ( // TODO
            <>dropdown</>
        //   <DropdownButton>
        //     <div className="flex items-center gap-2">
        //       {user?.username?.split('#')[0]}
        //       <img
        //         src={
        //           user?.pictureBase64?.length && user?.pictureBase64?.length > 0
        //             ? user.pictureBase64
        //             : BACKUP_PROFILE
        //         }
        //         alt="profile-pic"
        //         className="h-8 w-8 rounded-full object-cover"
        //       />
        //     </div>
        //   </DropdownButton>
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
        {/* <DropdownMobile>
          <BarsIcon />
        </DropdownMobile> */}
      </nav>
    </header>
  );
}
export default Header;

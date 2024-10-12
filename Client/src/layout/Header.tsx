import { Link } from 'react-router-dom';
import CineSlateLogo from '../assets/images/cineslateLogo.png';

function Header() {
  return (
    <header className="flex justify-center border py-2">
      <nav className="flex w-2/3 items-center justify-around">
        <Link to="/">
          <img
            src={CineSlateLogo}
            alt="Cineslate Logo"
            className="mx-10 w-16"
          />
        </Link>
        <input placeholder="Search Movies" type="search" name="search" />
        <ul className="flex gap-4">
          <li>
            <Link to="/movies">Movies</Link>
          </li>
          <li>
            <Link to="/critics">Critics</Link>
          </li>
          <li>
            <Link to="/stories">Stories</Link>
          </li>
          <li>
            <Link to="/quizzes">Quizzes</Link>
          </li>
        </ul>
        <Link to="login">Login</Link>
      </nav>
    </header>
  );
}
export default Header;

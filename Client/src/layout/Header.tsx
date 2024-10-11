import { Link } from "react-router-dom";

function Header() {
  return (
    <header className="flex flex-col items-center">
      <h1 className="text-3xl text-center">CineSlate</h1>
      <nav>
        <ul className="flex gap-4">
          <li>
            <Link to="/">Home</Link>
          </li>
          <li>
            <Link to="login">Login</Link>
          </li>
        </ul>
      </nav>
    </header>
  );
}
export default Header;

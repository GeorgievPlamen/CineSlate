import tmdbLogo from '../assets/images/tmdbLogo.png';

function Footer() {
  return (
    <footer className="flex items-center justify-between bg-indigo-800 py-2">
      <p className="text-sm">Created by Plamen Georgiev.</p>
      <p className="text-xs">© 2024 CineSlate. All rights reserved.</p>
      <a
        href="https://www.themoviedb.org"
        className="hover:text-gray-400"
        target="_blank"
      >
        <img src={tmdbLogo} alt="The movies database logo" className="w-40" />
      </a>
    </footer>
  );
}
export default Footer;

import tmdbLogo from '@/assets/images/tmdbLogo.png';

function Footer() {
  return (
    <footer className="fixed bottom-0 flex w-full items-center justify-between bg-background px-6 py-2 z-50">
      <p className="hidden text-sm md:block">Created by Plamen Georgiev.</p>
      <p className="hidden text-xs md:block">
        © {new Date().getUTCFullYear()} CineSlate. All rights reserved.
      </p>
      <p className="text-xs md:hidden">Created by P. Georgiev</p>
      <p className="text-xs md:hidden">
        © {new Date().getUTCFullYear()} CineSlate
      </p>
      <a
        href="https://www.themoviedb.org"
        className="hover:text-gray-400"
        target="_blank"
      >
        <img
          src={tmdbLogo}
          alt="The movies database logo"
          className="w-20 md:w-40"
        />
      </a>
    </footer>
  );
}
export default Footer;

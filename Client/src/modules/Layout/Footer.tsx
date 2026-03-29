import tmdbLogo from '@/assets/images/tmdbLogo.png';

function Footer() {
  return (
    <footer className="sticky bottom-0 flex w-full h-12 items-center justify-between bg-muted-background px-6 py-2 z-50 border-t border-t-border">
      <p className="hidden text-sm md:block text-muted-foreground">
        Created by <span className="text-secondary">Plamen Georgiev.</span>
      </p>
      <p className="text-xs md:hidden text-muted-foreground">
        Created by <span className="text-secondary">P. Georgiev</span>
      </p>
      <div className="flex items-center gap-2">
        <p className="hidden text-xs md:block  text-muted-foreground">
          © {new Date().getUTCFullYear()} CineSlate. All rights reserved.
        </p>
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
            className="w-20 md:w-28"
          />
        </a>
      </div>
    </footer>
  );
}
export default Footer;

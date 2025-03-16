import GenreButton from '../../app/components/Buttons/GenreButton';
import { genres } from '../../app/assets/tmdbGenres.json';

export default function Filters() {
  return (
    <section className="mx-auto flex w-2/3 flex-wrap items-center justify-center">
      {genres?.map((g) => (
        <GenreButton key={g.id} name={g.name} genreId={g.id} />
      ))}
    </section>
  );
}

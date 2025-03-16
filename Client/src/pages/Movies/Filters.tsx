import GenreButton from '../../app/components/Buttons/GenreButton';
import { genres } from '../../app/assets/tmdbGenres.json';

export default function Filters() {
  return (
    <section className="flex flex-wrap items-center justify-center">
      {genres?.map((g) => (
        <GenreButton key={g.id} name={g.name} genreId={g.id} />
      ))}
    </section>
  );
}

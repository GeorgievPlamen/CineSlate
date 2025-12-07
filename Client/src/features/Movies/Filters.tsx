import GenreButtonOld from '@/components/Buttons/GenreButtonOld';
import { genres } from '@/assets/tmdbGenres.json';

export default function Filters() {
  return (
    <section className="mx-auto flex w-2/3 flex-wrap items-center justify-center">
      {genres?.map((g) => (
        <GenreButtonOld key={g.id} name={g.name} genreId={g.id} />
      ))}
    </section>
  );
}

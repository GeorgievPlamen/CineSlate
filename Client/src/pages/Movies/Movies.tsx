import { useLoaderData } from 'react-router-dom';
import MovieCard from '../../app/components/Cards/MovieCard';
import { Paged } from '../../app/models/paged';
import { Movie } from './models/movieType';

function Movies() {
  const movies = useLoaderData() as Paged<Movie>;
  // TODO add load more button
  // TODO add suspense and loading fallback
  // TODO link from movie to movie details

  return (
    <article className="grid grid-cols-1 gap-y-10 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 2xl:px-40">
      {movies.values.map((m) => (
        <MovieCard
          key={m.id}
          title={m.title}
          id={m.id}
          rating={m.rating}
          releaseDate={m.releaseDate}
          posterPath={m.posterPath}
        />
      ))}
    </article>
  );
}
export default Movies;

import MovieCard from '../../app/components/Cards/MovieCard';

function Movies() {
  return (
    <div>
      <MovieCard
        title="title"
        id={123}
        rating={4.2}
        releaseDate="2024-01-01"
        posterPath="/aosm8NMQ3UyoBVpSxyimorCQykC.jpg"
      />
    </div>
  );
}
export default Movies;

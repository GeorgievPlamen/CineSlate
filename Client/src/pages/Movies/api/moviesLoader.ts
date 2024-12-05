import { moviesApi, MoviesBy } from './moviesApi';

export async function moviesLoader() {
  console.log('inside loader');
  const res = await moviesApi.getMovies(1, MoviesBy.GetNowPlaying);
  return res;
}

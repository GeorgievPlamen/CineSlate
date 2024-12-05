import { LoaderFunctionArgs } from 'react-router-dom';
import { moviesApi, MoviesBy } from './moviesApi';

export async function moviesLoader({ params }: LoaderFunctionArgs) {
  console.log('In loader');
  console.log(params);
  const res = await moviesApi.getMovies(1, MoviesBy.GetNowPlaying);
  return res;
}

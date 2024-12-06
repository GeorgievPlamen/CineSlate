import { cineslateApi } from '../../../app/api/cineslateApi';
import { Paged } from '../../../app/models/paged';
import { Movie } from '../models/movieType';

export enum MoviesBy {
  GetNowPlaying = '/now_playing',
  GetPopular = '/popular',
  GetTopRated = '/top_rated',
  GetUpcoming = '/upcoming',
}

const moviesApi = cineslateApi.injectEndpoints({
  endpoints: (build) => ({
    pagedMovies: build.query<
      Paged<Movie>,
      { page: number; moviesBy: MoviesBy }
    >({
      query: ({ page, moviesBy }) => `/movies${moviesBy}?page=${page}`,
    }),
  }),
});

export const { usePagedMoviesQuery } = moviesApi;

import { cineslateApi } from '../../../app/api/cineslateApi';
import { Paged } from '../../../app/models/paged';
import { Movie, MovieDetails } from '../models/movieType';

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
      serializeQueryArgs: () => {
        return 'getPagedMovies';
      },
      merge: (cache, newData) => {
        cache.values.push(...newData.values);
        cache.currentPage = newData.currentPage;
        cache.totalCount = newData.totalCount;
      },
      forceRefetch({ currentArg, previousArg }) {
        return currentArg?.page !== previousArg?.page;
      },
    }),

    movieDetails: build.query<MovieDetails, { id: string | undefined }>({
      query: ({ id }) => `/movies/${id}`,
    }),

    pagedMoviesSearchByTitle: build.query<
      Paged<Movie>,
      { page: number; searchTerm: string }
    >({
      query: ({ page, searchTerm }) =>
        `/movies/search?title=${searchTerm}&page=${page}`,
      serializeQueryArgs: ({ endpointName, queryArgs }) => {
        return `${endpointName}-${queryArgs.searchTerm}`;
      },
      merge: (cache, newData) => {
        cache.values.push(...newData.values);
        cache.currentPage = newData.currentPage;
        cache.totalCount = newData.totalCount;
      },
      forceRefetch({ currentArg, previousArg }) {
        return currentArg?.page !== previousArg?.page;
      },
    }),
  }),
});

export const {
  usePagedMoviesQuery,
  useMovieDetailsQuery,
  usePagedMoviesSearchByTitleQuery,
} = moviesApi;

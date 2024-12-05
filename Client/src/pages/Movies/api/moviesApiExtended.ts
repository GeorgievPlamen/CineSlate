import { cineslateApi } from '../../../app/api/cineslateApi';

const moviesExtended = cineslateApi.injectEndpoints({
  endpoints: (build) => ({
    pagedMovies: build.query({
      query: ({ page, moviesBy }) => `/movies${moviesBy}?page=${page}`,
    }),
  }),
});

export const { useLazyPagedMoviesQuery } = moviesExtended;

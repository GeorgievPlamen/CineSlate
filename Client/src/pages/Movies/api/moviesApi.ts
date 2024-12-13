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
    }),
    movieDetails: build.query<MovieDetails, { id: string | undefined }>({
      query: ({ id }) => `/movies/${id}`,
    }),
    addReview: build.mutation<
      void,
      {
        rating: number;
        movieId: number;
        text: string;
        containsSpoilers: boolean;
      }
    >({
      query: ({ rating, movieId, text, containsSpoilers }) => ({
        url: '/reviews/',
        method: 'POST',
        body: { rating, movieId, text, containsSpoilers },
      }),
    }),
  }),
});

export const {
  usePagedMoviesQuery,
  useMovieDetailsQuery,
  useAddReviewMutation,
} = moviesApi;

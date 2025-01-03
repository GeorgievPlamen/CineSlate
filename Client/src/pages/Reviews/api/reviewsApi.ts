import { cineslateApi } from '../../../app/api/cineslateApi';
import { Paged } from '../../../app/models/paged';
import { Review } from '../models/review';

const reviewsApi = cineslateApi.injectEndpoints({
  endpoints: (build) => ({
    reviewsByMovieId: build.query<
      Paged<Review>,
      { movieId: number; page: number }
    >({
      query: ({ movieId, page }) => `/reviews/${movieId}?page=${page}`,
    }),
    ownedReviewsByMovieId: build.query<Review, { movieId: number }>({
      query: ({ movieId }) => `reviews/own/${movieId}`,
    }),
    addReview: build.mutation<
      { location: string | null },
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
      transformResponse: (_, meta) => {
        const locationHeader = meta?.response?.headers?.get('Location') ?? null;

        return {
          location: locationHeader,
        };
      },
    }),
    updateReview: build.mutation<
      { location: string | null },
      {
        reviewId: string;
        rating: number;
        movieId: number;
        text: string;
        containsSpoilers: boolean;
      }
    >({
      query: ({ reviewId, rating, movieId, text, containsSpoilers }) => ({
        url: '/reviews/',
        method: 'PUT',
        body: { reviewId, rating, movieId, text, containsSpoilers },
      }),
      transformResponse: (_, meta) => {
        const locationHeader = meta?.response?.headers?.get('Location') ?? null;

        return {
          location: locationHeader,
        };
      },
    }),
  }),
});

export const {
  useReviewsByMovieIdQuery,
  useAddReviewMutation,
  useOwnedReviewsByMovieIdQuery,
  useUpdateReviewMutation,
} = reviewsApi;

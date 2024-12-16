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
  }),
});

export const { useReviewsByMovieIdQuery } = reviewsApi;

import { cineslateApi } from '../../../app/api/cineslateApi';
import { Paged } from '../../../app/models/paged';
import { Review } from '../../Reviews/models/review';

export interface ReviewWithMovieDetailsResponse {
  title: string;
  movieId: number;
  releaseDate: string;
  posterPath: string;
  reviewResponse: Review;
}

const criticDetailsApi = cineslateApi.injectEndpoints({
  endpoints: (build) => ({
    getReviewsByAuthorId: build.query<
      Paged<ReviewWithMovieDetailsResponse>,
      { id: string; page: number }
    >({
      query: ({ id, page }) => `/reviews/user/${id}?page=${page}`,
      serializeQueryArgs: () => {
        return 'getReviewsByAuthorId';
      },
      merge: (cache, newData) => {
        if (
          cache.values[0]?.reviewResponse.authorId ===
          newData.values[0]?.reviewResponse.authorId
        ) {
          cache.values.push(...newData.values);
        } else {
          cache.values = newData.values;
        }
        cache.currentPage = newData.currentPage;
        cache.totalCount = newData.totalCount;
      },
      forceRefetch({ currentArg, previousArg }) {
        return (
          currentArg?.page !== previousArg?.page ||
          currentArg?.id !== previousArg?.id
        );
      },
    }),
  }),
});

export const { useGetReviewsByAuthorIdQuery } = criticDetailsApi;

import { cineslateApi } from '../../../app/api/cineslateApi';
import { Paged } from '../../../app/models/paged';
import { Review } from '../../Reviews/models/review';

const criticDetailsApi = cineslateApi.injectEndpoints({
  endpoints: (build) => ({
    getReviewsByAuthorId: build.query<
      Paged<Review>,
      { id: string; page: number }
    >({
      query: ({ id, page }) => `/reviews/user/${id}?page=${page}`,
      serializeQueryArgs: () => {
        return 'getReviewsByAuthorId';
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

export const { useGetReviewsByAuthorIdQuery } = criticDetailsApi;

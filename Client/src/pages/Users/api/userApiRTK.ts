import { cineslateApi } from '../../../app/api/cineslateApi';
import { Paged } from '../../../app/models/paged';
import { User } from '../Models/userType';

const userApiRTK = cineslateApi.injectEndpoints({
  endpoints: (build) => ({
    getLatestUsers: build.query<Paged<User>, { page: number }>({
      query: ({ page }) => `/users/${page}`,
      serializeQueryArgs: () => {
        return 'getLatestUsers';
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

    getUsersById: build.query<User[], { ids: string[] }>({
      query: ({ ids }) => ({
        url: '/users',
        method: 'POST',
        body: {
          UserIds: ids,
        },
      }),
    }),
  }),
});

export const {
  useGetLatestUsersQuery,
  useLazyGetUsersByIdQuery,
  useGetUsersByIdQuery,
} = userApiRTK;

import { cineslateApi } from '../../../api/cineslateApi';
import { User } from '../Models/userType';

const userApiRTK = cineslateApi.injectEndpoints({
  endpoints: (build) => ({
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
  useLazyGetUsersByIdQuery,
  useGetUsersByIdQuery,
} = userApiRTK;

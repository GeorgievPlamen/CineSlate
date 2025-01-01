import { cineslateApi } from '../../../app/api/cineslateApi';
import { User } from '../../Users/Models/userType';

const myDetailsApi = cineslateApi.injectEndpoints({
  endpoints: (build) => ({
    updateUser: build.mutation<User, { id: string; bio: string }>({
      query: ({ id, bio }) => ({
        url: `users/${id}?bio=${bio}`,
        method: 'PUT',
      }),
    }),
  }),
});

export const { useUpdateUserMutation } = myDetailsApi;

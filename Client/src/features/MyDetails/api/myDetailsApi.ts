import { cineslateApi } from '../../../api/cineslateApi';
import { User } from '../../Users/Models/userType';

const myDetailsApi = cineslateApi.injectEndpoints({
  endpoints: (build) => ({
    updateUser: build.mutation<
      User,
      { id: string; bio: string; pictureBase64: string }
    >({
      query: ({ id, bio, pictureBase64 }) => ({
        url: `users/${id}?bio=${bio}`,
        method: 'PUT',
        body: `"${pictureBase64}"`,
      }),
    }),
  }),
});

export const { useUpdateUserMutation } = myDetailsApi;

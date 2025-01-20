import {
  BaseQueryFn,
  createApi,
  FetchArgs,
  fetchBaseQuery,
  FetchBaseQueryError,
} from '@reduxjs/toolkit/query/react';
import { CINESLATE_API_URL } from '../config';
import { RootState } from '../store/store';
import { redirect } from 'react-router-dom';
import { setUser } from '../../pages/Users/userSlice';
import { User } from '../../pages/Users/Models/userType';

export const cineslateApi = createApi({
  reducerPath: 'cineslateApi',
  baseQuery: fetchBaseQuery({
    baseUrl: CINESLATE_API_URL,
    prepareHeaders: (headers, { getState }) => {
      const token = (getState() as RootState).users.user.token;

      if (token) {
        headers.set('authorization', `Bearer ${token}`);
      }
    },
  }),
  endpoints: (builder) => ({
    getHealth: builder.query<void, void>({
      query: () => '/',
    }),
  }),
});

const rawBaseQuery = fetchBaseQuery({
  baseUrl: 'https://your-api.com',
  prepareHeaders: (headers, { getState }) => {
    // If you stored the token in Redux store, get it from there
    const token = (getState() as RootState).users.user.token;
    if (token) {
      headers.set('authorization', `Bearer ${token}`);
    }
    return headers;
  },
});

export const baseQueryWithReauth: BaseQueryFn<
  string | FetchArgs,
  unknown,
  FetchBaseQueryError
> = async (args, api, extraOptions) => {
  const result = await rawBaseQuery(args, api, extraOptions);

  if (result.error && result.error.status === 401) {
    const refreshToken = (api.getState() as RootState).users.user.refreshToken;

    if (!refreshToken) redirect('/login');

    const refreshResult = await rawBaseQuery(
      {
        url: '/users/refresh-token',
        method: 'POST',
        body: { refreshToken: refreshToken },
      },
      api,
      extraOptions
    );

    if (!refreshResult.data) redirect('/login');

    api.dispatch(setUser(refreshResult?.data as User));
  }

  return await rawBaseQuery(args, api, extraOptions);
};

export const { useGetHealthQuery } = cineslateApi;

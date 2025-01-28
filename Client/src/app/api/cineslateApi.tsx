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

const rawBaseQuery = fetchBaseQuery({
  baseUrl: CINESLATE_API_URL,
  prepareHeaders: (headers, { getState }) => {
    const token = (getState() as RootState).users.user.token;
    if (token) {
      headers.set('authorization', `Bearer ${token}`);
    }

    headers.set('Content-Type', 'application/json');
    return headers;
  },
});

const baseQueryWithReauth: BaseQueryFn<
  string | FetchArgs,
  unknown,
  FetchBaseQueryError
> = async (args, api, extraOptions) => {
  const result = await rawBaseQuery(args, api, extraOptions);

  if (result?.error?.status !== 401) return result;

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

  return await rawBaseQuery(args, api, extraOptions);
};

export const cineslateApi = createApi({
  reducerPath: 'cineslateApi',
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    getHealth: builder.query<void, void>({
      query: () => '/',
    }),
  }),
});

export const { useGetHealthQuery } = cineslateApi;

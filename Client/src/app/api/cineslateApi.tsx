import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { CINESLATE_API_URL } from '../config';
import { RootState } from '../store/store';

export const cineslateApi = createApi({
  reducerPath: 'cineslateApi',
  baseQuery: fetchBaseQuery({
    baseUrl: CINESLATE_API_URL,
    prepareHeaders: (headers, { getState }) => {
      const token = (getState() as RootState).users.user.token;

      if (token) {
        headers.set('authorization', `Bearer ${token}`);
        headers.set('Content-Type', 'application/json');
      }
    },
  }),
  endpoints: (builder) => ({
    getHealth: builder.query<void, void>({
      query: () => '/',
    }),
  }),
});

export const { useGetHealthQuery } = cineslateApi;

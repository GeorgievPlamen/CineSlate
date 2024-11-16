import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { CINESLATE_API_URL } from '../config';

export const cineslateApi = createApi({
  reducerPath: 'cineslateApi',
  baseQuery: fetchBaseQuery({ baseUrl: CINESLATE_API_URL }),
  endpoints: (builder) => ({
    getHealth: builder.query<void, void>({
      query: () => '/',
    }),
  }),
});

export const { useGetHealthQuery } = cineslateApi;

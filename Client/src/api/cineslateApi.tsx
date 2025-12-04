// const baseQueryWithReauth: BaseQueryFn<
//   string | FetchArgs,
//   unknown,
//   FetchBaseQueryError
// > = async (args, api, extraOptions) => {
//   const result = await rawBaseQuery(args, api, extraOptions);

//   if (result?.error?.status !== 401) return result;

//   const refreshToken = (api.getState() as RootState).users.user.refreshToken;

//   if (!refreshToken) redirect('/login');

//   const refreshResult = await rawBaseQuery(
//     {
//       url: '/users/refresh-token',
//       method: 'POST',
//       body: { refreshToken: refreshToken },
//     },
//     api,
//     extraOptions
//   );

//   if (!refreshResult.data) redirect('/login');

//   api.dispatch(setUser(refreshResult?.data as User));

//   return await rawBaseQuery(args, api, extraOptions);
// };


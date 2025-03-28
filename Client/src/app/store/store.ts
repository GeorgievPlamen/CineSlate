import { configureStore } from '@reduxjs/toolkit';
import { userSlice } from '../../pages/Users/userSlice';
import { cineslateApi } from '../api/cineslateApi';
import { criticsSlice } from '../../pages/Critics/criticsSlice';
import { moviesSlice } from '../../pages/Movies/moviesSlice';

export const store = configureStore({
  reducer: {
    users: userSlice.reducer,
    [cineslateApi.reducerPath]: cineslateApi.reducer,
    critics: criticsSlice.reducer,
    movies: moviesSlice.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(cineslateApi.middleware),
});

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>;
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch;

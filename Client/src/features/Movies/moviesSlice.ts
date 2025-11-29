import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { useAppSelector } from '../../store/reduxHooks';

interface MoviesState {
  filters: {
    searchTerm?: string;
  };
}

const init: MoviesState = {
  filters: {},
};

export const moviesSlice = createSlice({
  name: 'movies',
  initialState: init,
  reducers: {
    setSearchTermFilter: (
      state,
      action: PayloadAction<MoviesState['filters']['searchTerm']>
    ) => {
      state.filters.searchTerm = action.payload;
    },
  },
});

export const { setSearchTermFilter } = moviesSlice.actions;

export const useMovieFilters = () =>
  useAppSelector((store) => store.movies.filters);

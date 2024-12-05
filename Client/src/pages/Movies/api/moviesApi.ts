import api from '../../../app/api/api';
import { Paged } from '../../../app/models/paged';
import { Movie } from '../models/movieType';

export enum MoviesBy {
  GetNowPlaying = '/now_playing',
  GetPopular = '/popular',
  GetTopRated = '/top_rated',
  GetUpcoming = '/upcoming',
}

export const moviesApi = {
  getMovies: async (page: number, moviesBy: MoviesBy): Promise<Paged<Movie>> =>
    api.get(`/movies${moviesBy}?page=${page}`),
};

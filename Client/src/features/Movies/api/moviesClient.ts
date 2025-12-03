import apiClient from '@/api/api';
import { MovieDetails } from '../models/movieType';

export const moviesClient = {
  getMovieDetails: async (id: string): Promise<MovieDetails> =>
    apiClient.get(`/movies/${id}`),
};

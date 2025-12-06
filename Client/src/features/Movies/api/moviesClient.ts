import apiClient from '@/api';
import { Movie, MovieDetails } from '../models/movieType';
import { Paged } from '@/common/models/paged';

export enum MoviesBy {
  GetNowPlaying = '/now_playing',
  GetPopular = '/popular',
  GetTopRated = '/top_rated',
  GetUpcoming = '/upcoming',
}

export const moviesClient = {
  getMovieDetails: async (id: string): Promise<MovieDetails> =>
    apiClient.get(`/movies/${id}`),

  getPagedMovies: async (by: MoviesBy, page: number): Promise<Paged<Movie>> =>
    apiClient.get(`/movies${by}?page=${page}`),

  getPagedMoviesSearchByTitle: async (
    searchTerm: string,
    page: number
  ): Promise<Paged<Movie>> =>
    apiClient.get(`/movies/search?title=${searchTerm}&page=${page}`),

  getPagedMoviesSearchByFilters: async (
    genreIds: string[],
    year: string,
    page: number
  ): Promise<Paged<Movie>> => {
    let genreIdsQuery;
    let yearQuery = '&year=';
    if (genreIds) {
      genreIdsQuery = genreIds.map((id) => `&genreIds=${id}`).join('');
    }
    if (yearQuery) {
      yearQuery = `&year=${year}`;
    }

    return apiClient.get(
      `/movies/filter?page=${page}${yearQuery}${genreIdsQuery}`
    );
  },
};

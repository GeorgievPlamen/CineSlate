import apiClient from '@/api';
import { Movie, MovieDetails } from '../models/movieType';
import { Paged } from '@/common/models/paged';

export enum MoviesBy {
  NowPlaying = '/now_playing',
  Popular = '/popular',
  TopRated = '/top_rated',
  Upcoming = '/upcoming',
}

export const MoviesByTitleMap: Record<MoviesBy, string> = {
  [MoviesBy.NowPlaying]: 'Now Playing',
  [MoviesBy.Popular]: 'Popular',
  [MoviesBy.TopRated]: 'Top Rated',
  [MoviesBy.Upcoming]: 'Upcoming',
};

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
    genreIds: number[],
    yearFrom: string,
    yearTo: string,
    page: number
  ): Promise<Paged<Movie>> => {
    let genreIdsQuery;
    let yearFromQuery = '';
    let yearToQuery = '';
    if (genreIds) {
      genreIdsQuery = genreIds.map((id) => `&genreIds=${id}`).join('');
    }
    if (yearFrom) {
      yearFromQuery = `&yearFrom=${yearFrom}`;
    }
    if (yearTo) {
      yearToQuery = `&yearTo=${yearTo}`;
    }

    return apiClient.get(
      `/movies/filter?page=${page}${genreIdsQuery}${yearFromQuery}${yearToQuery}`
    );
  },

  getMoviesInWatchlist: async (): Promise<Movie[]> =>
    apiClient.get('/movies/watchlist'),
};

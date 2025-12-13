import apiClient from '@/api';

interface GetWatchlistResponse {
  ToWatchMovie: Record<number, boolean>[];
}

export const watchlistsClient = {
  getWatchlist: async (): Promise<GetWatchlistResponse> =>
    apiClient.get('/watchlist/'),

  addToWatchlist: async (id: number): Promise<void> =>
    apiClient.post(`/watchlist/${id}`),
};

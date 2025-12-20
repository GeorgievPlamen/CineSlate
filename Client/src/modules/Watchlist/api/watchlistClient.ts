import apiClient from '@/api';
import { KVP } from '@/common/models/kvp';

interface WatchlistResponse {
  watchlist: KVP<number, boolean>[];
}

export const watchlistsClient = {
  getWatchlist: async (): Promise<WatchlistResponse> =>
    apiClient.get('/watchlist/'),

  addToWatchlist: async (id: number): Promise<void> =>
    apiClient.post(`/watchlist/${id}`),

  setWatched: async (id: number, watched: boolean = true): Promise<void> =>
    apiClient.put(`/watchlist/${id}?watched=${watched}`),
};

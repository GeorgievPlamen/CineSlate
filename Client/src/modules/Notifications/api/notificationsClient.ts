import apiClient from '@/api';
import { Paged } from '@/common/models/paged';
import { NotificationResponse } from '../models/notificationModels';

export const notificationsClient = {
  getMy: async (page: number, quantity: number): Promise<Paged<NotificationResponse>> =>
    apiClient.get(`/notifications/?page=${page}&quantity=${quantity}`),

  getNewCount: async (): Promise<number> => apiClient.get('/notifications/new-count'),

  setAllSeen: async (): Promise<boolean> => apiClient.put('/notifications/set-all-seen'),
};

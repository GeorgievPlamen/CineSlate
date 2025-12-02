import apiClient from '@/api/api';
import { Paged } from '@/models/paged';
import { ReviewWithMovieDetailsResponse } from '../models/review';

export const reviewsClient = {
  getReviewsByAuthorId: async (id: string, page: number): Promise<Paged<ReviewWithMovieDetailsResponse>> =>
    apiClient.get(`/reviews/user/${id}?page=${page}`),
};

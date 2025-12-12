import { Paged } from '@/common/models/paged';
import {
  Review,
  ReviewDetails,
  ReviewWithMovieDetailsResponse,
} from '../models/review';
import apiClient from '@/api';

export const reviewsClient = {
  getReviewsByAuthorId: async (
    id: string,
    page: number
  ): Promise<Paged<ReviewWithMovieDetailsResponse>> =>
    apiClient.get(`/reviews/user/${id}?page=${page}`),

  reviewDetailsById: async (reviewId: string): Promise<ReviewDetails> =>
    apiClient.get(`reviews/details/${reviewId}`),

  commentReview: async (reviewId: string, comment: string): Promise<Review> =>
    apiClient.post(`reviews/comment/${reviewId}`, comment),

  reviewsByMovieId: async (
    movieId: string,
    page: number
  ): Promise<Paged<Review>> =>
    apiClient.get(`/reviews/${movieId}?page=${page}`),

  ownedReviewsByMovieId: async (movieId: string): Promise<Review> =>
    apiClient.get(`reviews/own/${movieId}`),

  likeReview: async (reviewId: string): Promise<Review> =>
    apiClient.post(`reviews/like/${reviewId}`),

  addReview: async (
    rating: number,
    movieId: number,
    text: string,
    containsSpoilers: boolean
  ): Promise<{ location: string | null }> => {
    const response = await apiClient.post('/reviews/', {
      rating,
      movieId,
      text,
      containsSpoilers,
    });
    if (!response) return { location: null };

    // @ts-expect-error accepted
    return { reviewId: response.value };
  },

  updateReview: async (
    reviewId: string,
    rating: number,
    movieId: number,
    text: string,
    containsSpoilers: boolean
  ): Promise<{ reviewId: string | null }> => {
    const response = await apiClient.put('/reviews/', {
      reviewId,
      rating,
      movieId,
      text,
      containsSpoilers,
    });
    if (!response) return { reviewId: null };

    // @ts-expect-error accepted
    return { reviewId: response.value };
  },
};

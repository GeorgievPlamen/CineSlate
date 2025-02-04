import { z } from 'zod';

export const Review = z.object({
  id: z.string().optional(),
  rating: z
    .string({ message: 'Rating is required.' })
    .min(1, 'Rating is required.')
    .max(5),
  text: z.string().max(2000, 'Maximum character length is 2000.').optional(),
  movieId: z.number().optional(),
  authorId: z.string().optional(),
  authorUsername: z.string().optional(),
  containsSpoilers: z.boolean(),
  likes: z.number(),
  hasUserLiked: z.boolean(),
});

const ReviewDetailsProps = z.object({
  usersWhoLiked: z.array(
    z.object({
      value: z.string(),
      onlyName: z.string(),
    })
  ),
});

const ReviewDetails = Review.merge(ReviewDetailsProps);

export type Review = z.infer<typeof Review>;

export type ReviewDetails = z.infer<typeof ReviewDetails>;

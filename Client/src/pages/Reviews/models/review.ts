import { z } from 'zod';

export const Review = z.object({
  rating: z
    .string({ message: 'Rating is required.' })
    .min(1, 'Rating is required.')
    .max(5),
  text: z.string().max(2000, 'Maximum character length is 2000.').optional(),
  movieId: z.number().optional(),
  authorId: z.string().optional(),
  authorUsername: z.string().optional(),
  containsSpoilers: z.boolean(),
});

export type Review = z.infer<typeof Review>;

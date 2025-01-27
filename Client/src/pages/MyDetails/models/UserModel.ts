import { z } from 'zod';

export const UserModel = z.object({
  username: z.string({ message: 'Username is required.' }),
  bio: z.string().optional(),
  avatar: z.any(),
});

export type UserModel = z.infer<typeof UserModel>;

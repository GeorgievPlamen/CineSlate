import { z } from 'zod';

export const UserModel = z.object({
  username: z.string({ message: 'Username is required.' }),
  bio: z.string().optional(),
});

export type UserModel = z.infer<typeof UserModel>;

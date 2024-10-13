import { userErrors } from './userErrors';
import { User } from './userType';

export interface UserResponse {
  user?: User | null;
  errors?: userErrors[] | null | string;
}

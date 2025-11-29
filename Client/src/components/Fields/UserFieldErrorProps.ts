import { userErrors } from '../../features/Users/Models/userErrors';

export interface UserFieldErrorProps {
  readonly errors?: userErrors[] | null | string;
}

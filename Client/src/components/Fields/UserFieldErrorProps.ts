import { userErrors } from '../../features/Users/userErrors';

export interface UserFieldErrorProps {
  readonly errors?: userErrors[] | null | string;
}

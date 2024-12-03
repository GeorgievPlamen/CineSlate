import { userErrors } from '../../../pages/Users/Models/userErrors';

export interface UserFieldErrorProps {
  readonly errors?: userErrors[] | null | string;
}

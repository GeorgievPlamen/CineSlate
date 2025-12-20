import { userErrors } from "@/modules/Users/Models/userErrors";

export interface UserFieldErrorProps {
  readonly errors?: userErrors[] | null | string;
}

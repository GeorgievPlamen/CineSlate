import { ActionFunctionArgs } from 'react-router-dom';
import { isProblemDetails, getErrorDetails } from '../../api/errors';
import { userApi } from './userApi';
import { userErrors } from './userErrors';
import { User } from './userType';
import { SESSION_JWT } from '../../config';

export interface LoginResponse {
  user?: User | null;
  errors?: userErrors[] | null | string;
}

export async function loginAction({
  request,
}: ActionFunctionArgs): Promise<LoginResponse> {
  try {
    const input = await request.formData();
    const errors = validateLogin(input);

    if (errors.length > 0) return { errors };

    const user = await userApi.login(input);
    sessionStorage.setItem(SESSION_JWT, user?.token);

    return { user };
  } catch (error) {
    if (isProblemDetails(error)) return { errors: getErrorDetails(error) };
    else throw error;
  }
}

function validateLogin(formData: FormData) {
  const errors: userErrors[] = [];
  const email = formData.get('email') as string;
  const password = formData.get('password') as string;

  if (validateEmail(email) === false) errors.push(userErrors.InvalidEmail);
  if (validatePassword(password) === false)
    errors.push(userErrors.MissingPassword);

  return errors;
}

function validatePassword(password: string) {
  if (!password) return false;
  else return true;
}

function validateEmail(email: string | null | undefined): boolean {
  if (!email) return false;

  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email);
}

import { ActionFunctionArgs } from 'react-router-dom';
import { isProblemDetails, getErrorDetails } from '../../api/errors';
import { userApi } from './userApi';
import { userErrors } from './userErrors';
import { SESSION_JWT } from '../../config';
import { validate } from '../../utils/validate';
import { UserResponse } from './UserResponse';

export async function loginAction({
  request,
}: ActionFunctionArgs): Promise<UserResponse> {
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

  if (validate.email(email) === false) errors.push(userErrors.InvalidEmail);
  if (!password) errors.push(userErrors.MissingPassword);

  return errors;
}

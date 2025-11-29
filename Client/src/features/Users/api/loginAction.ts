import { ActionFunctionArgs } from 'react-router-dom';
import { userErrors } from '../Models/userErrors';
import { UserResponse } from '../Models/UserResponse';
import { getErrorDetails, isProblemDetails } from '../../../api/errors';
import { LOCAL_JWT, LOCAL_REFRESH } from '../../../config';
import { validate } from '../../../utils/validate';
import { userApi } from './userApi';

export async function loginAction({
  request,
}: ActionFunctionArgs): Promise<UserResponse> {
  try {
    const input = await request.formData();
    const errors = validateLogin(input);

    if (errors.length > 0) return { errors };

    const user = await userApi.login(input);

    localStorage.setItem(LOCAL_JWT, user?.token ?? '');

    localStorage.setItem(LOCAL_REFRESH, user?.refreshToken ?? '');

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

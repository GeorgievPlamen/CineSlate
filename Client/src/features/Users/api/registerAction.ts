import { ActionFunctionArgs } from 'react-router-dom';
import { userErrors } from '../Models/userErrors';
import { UserResponse } from '../Models/UserResponse';
import { isProblemDetails, getErrorDetails } from '../../../api/errors';
import { LOCAL_JWT, LOCAL_REFRESH } from '../../../config';
import { validate } from '../../../utils/validate';
import { usersClient } from './usersClient';

export async function registerAction({
  request,
}: ActionFunctionArgs): Promise<UserResponse> {
  try {
    const input = await request.formData();
    const errors = validateRegister(input);

    if (errors.length > 0) return { errors };

    await usersClient.register(input);

    const user = await usersClient.login(input);
    localStorage.setItem(LOCAL_JWT, user?.token ?? '');
    localStorage.setItem(LOCAL_REFRESH, user?.refreshToken ?? '');

    return { user };
  } catch (error) {
    if (isProblemDetails(error)) return { errors: getErrorDetails(error) };
    else throw error;
  }
}

function validateRegister(formData: FormData) {
  const errors: userErrors[] = [];
  const userName = formData.get('userName') as string;
  const email = formData.get('email') as string;
  const password = formData.get('password') as string;
  const confirmPassword = formData.get('confirmPassword') as string;

  if (!userName) errors.push(userErrors.MissingUsername);
  if (userName.length > 30) errors.push(userErrors.InvalidUsername);
  if (!validate.email(email)) errors.push(userErrors.InvalidEmail);
  if (!validate.password(password)) errors.push(userErrors.InvalidPassword);
  if (confirmPassword !== password) errors.push(userErrors.PasswordsMatch);

  return errors;
}

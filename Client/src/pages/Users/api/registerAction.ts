import { ActionFunctionArgs } from 'react-router-dom';
import { userErrors } from '../Models/userErrors';
import { UserResponse } from '../Models/UserResponse';
import { isProblemDetails, getErrorDetails } from '../../../app/api/errors';
import { SESSION_JWT } from '../../../app/config';
import { validate } from '../../../app/utils/validate';
import { userApi } from './userApi';

export async function registerAction({
  request,
}: ActionFunctionArgs): Promise<UserResponse> {
  try {
    const input = await request.formData();
    const errors = validateRegister(input);

    if (errors.length > 0) return { errors };

    await userApi.register(input);

    const user = await userApi.login(input);
    sessionStorage.setItem(SESSION_JWT, user?.token);

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

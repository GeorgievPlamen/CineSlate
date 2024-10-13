import { ActionFunctionArgs } from 'react-router-dom';
import { getErrorDetails, isProblemDetails } from '../../api/errors';
import { userErrors } from './userErrors';
import { validate } from '../../utils/validate';
import { userApi } from './userApi';
import { SESSION_JWT } from '../../config';
import { UserResponse } from './UserResponse';

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
  const firstName = formData.get('firstName') as string;
  const lastName = formData.get('lastName') as string;
  const email = formData.get('email') as string;
  const password = formData.get('password') as string;
  const confirmPassword = formData.get('confirmPassword') as string;

  if (!firstName) errors.push(userErrors.MissingFirstName);
  if (firstName.length > 30) errors.push(userErrors.InvalidFirstName);
  if (!lastName) errors.push(userErrors.MissingLastName);
  if (lastName.length > 30) errors.push(userErrors.InvalidLastName);
  if (!validate.email(email)) errors.push(userErrors.InvalidEmail);
  if (!validate.password(password)) errors.push(userErrors.InvalidPassword);
  if (confirmPassword !== password) errors.push(userErrors.PasswordsMatch);

  return errors;
}

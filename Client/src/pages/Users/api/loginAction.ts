import { ActionFunctionArgs } from 'react-router-dom';
import { userErrors } from '../Models/userErrors';
import { UserResponse } from '../Models/UserResponse';
import { getErrorDetails, isProblemDetails } from '../../../app/api/errors';
import { SESSION_JWT } from '../../../app/config';
import { validate } from '../../../app/utils/validate';
import { userApi } from './userApi';

export async function loginAction({
  request,
}: ActionFunctionArgs): Promise<UserResponse> {
  try {

    const input = await request.formData();
    const errors = validateLogin(input);

    if (errors.length > 0) return { errors };

    console.log(" will await")
    const user = await userApi.login(input);

    console.log("waiting")
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

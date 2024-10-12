import {
  ActionFunctionArgs,
  Form,
  useActionData,
  useNavigation,
} from 'react-router-dom';
import { getErrorDetails, isProblemDetails } from '../../api/errors';
import { userApi } from './userApi';
import { userErrors } from './userErrors';
import ValidationError from '../../components/ValidationError';
import Linebreak from '../../components/Linebreak';
import Spinner from '../../components/Spinner';
import { User } from './userType';
import { useAppDispatch } from '../../store/reduxHooks';
import { setUser } from './userSlice';

interface LoginResponse {
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
    sessionStorage.setItem('JWT', user?.token);

    return { user };
  } catch (error) {
    if (isProblemDetails(error)) return { errors: getErrorDetails(error) };
    throw error;
  }
}

function validateLogin(formData: FormData) {
  const errors: userErrors[] = [];
  const email = formData.get('email') as string;
  const password = formData.get('password') as string;

  if (validateEmail(email) === false) errors.push(userErrors.InvalidEmail);
  if (validatePassword(password) === false)
    errors.push(userErrors.PasswordMissing);

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

function Login() {
  const dispatch = useAppDispatch();
  const response = useActionData() as LoginResponse;

  console.log(response);

  if (response?.user) {
    dispatch(setUser(response.user));
  }

  const navigation = useNavigation();

  return (
    <Form
      noValidate
      method="post"
      className="border-whitesmoke bg-grayFrench dark:bg-bluePersian dark:shadow-light mt-10 flex w-80 flex-col items-center gap-3 rounded-xl border p-4 shadow-xl"
    >
      <div className="flex w-full flex-col">
        <label htmlFor="email" className="mb-1 ml-1 font-bold">
          Email
        </label>
        <input
          type="email"
          name="email"
          id="email"
          className="text-dark h-8 rounded-md px-2 focus:outline-none"
        />
        <ValidationError
          isError={response?.errors?.includes(userErrors.InvalidEmail)}
          message={userErrors.InvalidEmail}
        />
      </div>
      <div className="flex w-full flex-col">
        <label htmlFor="password" className="mb-1 ml-1 font-bold">
          Password
        </label>
        <input
          type="password"
          name="password"
          id="password"
          className="text-dark h-8 rounded-md px-2 focus:outline-none"
        />
        <ValidationError
          isError={response?.errors?.includes(userErrors.PasswordMissing)}
          message={userErrors.PasswordMissing}
        />
      </div>
      <ValidationError
        isError={response?.errors?.includes(userErrors.NotFound)}
        message={userErrors.NotFound}
      />
      <Linebreak />
      <button
        type="submit"
        className="text-whitesmoke flex h-8 w-full items-center justify-center rounded-full bg-indigo-600 hover:bg-indigo-500 active:bg-indigo-400"
      >
        {navigation.state === 'submitting' ? <Spinner /> : 'Sign in'}
      </button>
    </Form>
  );
}
export default Login;

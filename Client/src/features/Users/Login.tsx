import {
  ActionFunctionArgs,
  Form,
  useActionData,
  useNavigation,
} from 'react-router-dom';
import { getErrorDetails, isProblemDetails } from '../../api/errors';
import { userApi } from './userApi';
import { userErrors } from './userErrors';

export async function loginAction({ request }: ActionFunctionArgs) {
  try {
    const input = await request.formData();

    const email = input.get('email') as string;
    console.log(email);
    const isValid = validateEmail(email);
    console.log(isValid);
    if (!isValid) return userErrors.InvalidEmail;
    const result = await userApi.login(input);
    console.log(result);
    sessionStorage.setItem('JWT', result?.token);
    return { result };
  } catch (error) {
    if (isProblemDetails(error)) {
      return getErrorDetails(error);
    }
  }
}

function validateEmail(email: string | null | undefined): boolean {
  if (!email) return false; // Check if the email is null or undefined

  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email);
}

function Login() {
  const actionData = useActionData();
  console.log(actionData);

  const navigation = useNavigation();

  return (
    <Form
      noValidate
      method="post"
      className="border-whitesmoke bg-grayFrench dark:bg-bluePersian dark:shadow-light flex w-72 flex-col items-center gap-3 rounded border p-2 shadow-xl"
    >
      <div className="flex w-full flex-col">
        <label htmlFor="email" className="">
          Email
        </label>
        <input type="email" name="email" className="text-dark rounded-md" />
        {actionData === userErrors.InvalidEmail ? (
          <p className="text-error inline text-sm font-extralight">
            {userErrors.InvalidEmail}
          </p>
        ) : null}
      </div>
      <div className="flex w-full flex-col">
        <label htmlFor="password" className="">
          Password
        </label>
        <input
          type="password"
          name="password"
          className="text-dark rounded-md"
        />
      </div>
      <p className="">
        {actionData === userErrors.NotFound ? (
          <p className="text-error inline text-sm font-extralight">
            {userErrors.NotFound}
          </p>
        ) : null}
      </p>
      <button
        type="submit"
        className="border-whitesmoke text-whitesmoke h-8 w-full rounded-full border-2 bg-indigo-600 hover:bg-indigo-500 active:bg-indigo-400"
      >
        {navigation.state === 'submitting' ? 'Loading...' : 'Login'}
      </button>
    </Form>
  );
}
export default Login;

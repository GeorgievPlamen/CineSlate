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
      className="m-auto flex w-72 flex-col items-center gap-3 rounded border border-white"
    >
      {actionData === userErrors.NotFound ? (
        <p className="inline text-red-400">{userErrors.NotFound}</p>
      ) : null}
      <div className="mb-2 flex w-60 flex-col first:mt-2">
        <label htmlFor="email" className="mb-1">
          Email
        </label>
        <input type="email" name="email" className="rounded text-black" />
        {actionData === userErrors.InvalidEmail ? (
          <p className="inline text-red-400">{userErrors.InvalidEmail}</p>
        ) : null}
      </div>
      <div className="mb-2 flex w-60 flex-col">
        <label htmlFor="password" className="mb-1">
          Password
        </label>
        <input type="password" name="password" className="rounded text-black" />
      </div>
      <button
        type="submit"
        className="mb-3 mt-3 w-60 rounded bg-white text-black"
      >
        {navigation.state === 'submitting' ? 'Loading...' : 'Sign in'}
      </button>
    </Form>
  );
}
export default Login;

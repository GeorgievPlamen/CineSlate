import { ActionFunctionArgs, Form } from 'react-router-dom';
import { isProblemDetails } from '../../api/errors';
import { userApi } from './userApi';

export async function loginAction({ request }: ActionFunctionArgs) {
  try {
    const result = await userApi.login(await request.formData());
    console.log(result);
    sessionStorage.setItem('JWT', result?.token);
    return { result };
  } catch (error) {
    if (isProblemDetails(error)) {
      console.log(error.status);
      console.log(error.title);
      console.log(error.type);
    }
    console.log('exception');
    console.log(error);
  }
}

function Login() {
  return (
    <Form
      noValidate
      method="post"
      className="flex w-72 flex-col items-center gap-3 rounded border border-white first-letter:m-auto"
    >
      <div className="mb-2 flex w-60 flex-col first:mt-2">
        <label htmlFor="email" className="mb-1">
          Email
        </label>
        <input type="email" name="email" className="rounded text-black" />
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
        Sign in
      </button>
    </Form>
  );
}
export default Login;

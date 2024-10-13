import {
  Form,
  useActionData,
  useNavigate,
  useNavigation,
} from 'react-router-dom';
import { userErrors } from './userErrors';
import ValidationError from '../../components/ValidationError';
import Linebreak from '../../components/Linebreak';
import Spinner from '../../components/Spinner';
import { useAppDispatch } from '../../store/reduxHooks';
import { setUser } from './userSlice';
import { LoginResponse } from './loginAction';
import { useEffect } from 'react';
import { EyeIcon } from '@heroicons/react/16/solid';

function Login() {
  const response = useActionData() as LoginResponse;
  const navigation = useNavigation();
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (response?.user) {
      dispatch(setUser(response.user));
      navigate('/movies');
    }
  }, [dispatch, navigate, response]);

  return (
    <Form
      noValidate
      method="post"
      className="mt-10 flex w-80 flex-col items-center gap-3 rounded-xl border border-whitesmoke bg-grayFrench p-4 shadow-xl dark:bg-bluePersian dark:shadow-light"
    >
      <div className="flex w-full flex-col">
        <label htmlFor="email" className="mb-1 ml-1 font-bold">
          Email
        </label>
        <input
          type="email"
          name="email"
          id="email"
          className="h-8 rounded-md px-2 text-dark focus:outline-none"
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
          className="h-8 rounded-md px-2 text-dark focus:outline-none"
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
      <EyeIcon />
      <button
        type="submit"
        className="flex h-8 w-full items-center justify-center rounded-full bg-indigo-600 text-whitesmoke hover:bg-indigo-500 active:bg-indigo-400"
      >
        {navigation.state === 'submitting' ? <Spinner /> : 'Sign in'}
      </button>
    </Form>
  );
}
export default Login;

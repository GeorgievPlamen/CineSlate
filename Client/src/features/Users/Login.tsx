import { Form, NavLink, useActionData, useNavigate } from 'react-router-dom';
import { userErrors } from './userErrors';
import ValidationError from '../../components/ValidationError';
import Linebreak from '../../components/Linebreak';
import { useAppDispatch } from '../../store/reduxHooks';
import { setUser } from './userSlice';
import { LoginResponse } from './loginAction';
import { useEffect } from 'react';
import Heading2 from '../../components/Heading2';
import EmailField from '../../components/Fields/EmailField';
import PasswordField from '../../components/Fields/PasswordField';
import SubmitButton from '../../components/Buttons/SubmitButton';

function Login() {
  const response = useActionData() as LoginResponse;
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
      <Heading2 title="Login" />
      <EmailField errors={response?.errors} />
      <PasswordField errors={response?.errors} />
      <ValidationError
        isError={response?.errors?.includes(userErrors.NotFound)}
        message={userErrors.NotFound}
      />
      <Linebreak />
      <SubmitButton text="Sign in" />
      <div className="flex w-full justify-end text-center">
        <p className="pr-2 text-xs font-extralight">New?</p>
        <NavLink to={'/register'} className="text-xs font-bold">
          Create your account
        </NavLink>
      </div>
    </Form>
  );
}
export default Login;

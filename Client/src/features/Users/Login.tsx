import { Form, NavLink, useActionData } from 'react-router-dom';
import { userErrors } from './Models/userErrors';
import { UserResponse } from './Models/UserResponse';
import SubmitButton from '../../components/Buttons/SubmitButton';
import EmailField from '../../components/Fields/EmailField';
import PasswordField from '../../components/Fields/PasswordField';
import Heading2 from '../../components/Heading2';
import Linebreak from '../../components/Linebreak';
import ValidationError from '../../components/ValidationError';
import useHandleUserResponse from '../../hooks/useHandleUserResponse';

function Login() {
  const response = useActionData() as UserResponse;
  useHandleUserResponse(response);

  console.log(response);

  return (
    <Form
      noValidate
      method="post"
      className="border-whitesmoke bg-background mx-auto mt-10 flex w-80 flex-col items-center gap-3 rounded-xl border p-4"
    >
      <Heading2 title="Login" />
      <EmailField errors={response?.errors} />
      <PasswordField errors={response?.errors} />
      <ValidationError
        isError={response?.errors?.includes(userErrors.NotFound)}
        message={'User with email not found or password is wrong.'}
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

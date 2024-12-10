import { Form, NavLink, useActionData } from 'react-router-dom';
import { userErrors } from './Models/userErrors';
import { UserResponse } from './Models/UserResponse';
import SubmitButton from '../../app/components/Buttons/SubmitButton';
import EmailField from '../../app/components/Fields/EmailField';
import PasswordField from '../../app/components/Fields/PasswordField';
import Heading2 from '../../app/components/Heading2';
import Linebreak from '../../app/components/Linebreak';
import ValidationError from '../../app/components/ValidationError';
import useHandleUserResponse from '../../app/hooks/useHandleUserResponse';

function Login() {
  const response = useActionData() as UserResponse;
  useHandleUserResponse(response);

  return (
    <Form
      noValidate
      method="post"
      className="mx-auto mt-10 flex w-80 flex-col items-center gap-3 rounded-xl border border-whitesmoke bg-background p-4"
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

import { Form, NavLink, useActionData } from 'react-router-dom';
import { userErrors } from './Models/userErrors';
import { UserResponse } from './Models/UserResponse';
import SubmitButtonOld from '@/components/Buttons/SubmitButtonOld';
import EmailField from '@/components/Fields/EmailField';
import PasswordField from '@/components/Fields/PasswordField';
import Heading2 from '@/components/Heading2';
import Linebreak from '@/components/Linebreak';
import ValidationError from '@/components/ValidationError';

function LoginOld() {
  const response = useActionData() as UserResponse;
  useHandleUserResponseOld(response);

  return (
    <Form
      noValidate
      method="post"
      className="border-foreground bg-background mx-auto mt-10 flex w-80 flex-col items-center gap-3 rounded-xl border p-4"
    >
      <Heading2 title="Login" />
      <EmailField errors={response?.errors} />
      <PasswordField errors={response?.errors} />
      <ValidationError
        isError={response?.errors?.includes(userErrors.NotFound)}
        message={'User with email not found or password is wrong.'}
      />
      <Linebreak />
      <SubmitButtonOld text="Sign in" />
      <div className="flex w-full justify-end text-center">
        <p className="pr-2 text-xs font-extralight">New?</p>
        <NavLink to={'/register'} className="text-xs font-bold">
          Create your account
        </NavLink>
      </div>
    </Form>
  );
}
export default LoginOld;

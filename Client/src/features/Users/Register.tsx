import { Form, NavLink, useActionData } from 'react-router-dom';
import { UserResponse } from './Models/UserResponse';
import SubmitButton from '@/components/Buttons/SubmitButton';
import EmailField from '@/components/Fields/EmailField';
import NameField from '@/components/Fields/NameField';
import PasswordField from '@/components/Fields/PasswordField';
import Heading2 from '@/components/Heading2';
import Linebreak from '@/components/Linebreak';
import useHandleUserResponse from '@/hooks/useHandleUserResponse';

function Register() {
  const response = useActionData() as UserResponse;
  useHandleUserResponse(response);
  return (
    <Form
      noValidate
      method="post"
      className="border-whitesmoke bg-background mx-auto mb-20 flex w-11/12 flex-col items-center gap-3 rounded-xl border p-4 sm:mt-10 sm:w-80"
    >
      <Heading2 title="Register" />
      <NameField errors={response?.errors} />
      <EmailField errors={response?.errors} />
      <PasswordField errors={response?.errors} />
      <PasswordField errors={response?.errors} isConfirmPassword />
      <Linebreak />
      <SubmitButton text="Register" />
      <div className="flex w-full justify-end text-center">
        <p className="pr-2 text-xs font-extralight">Already have an account?</p>
        <NavLink to={'/login'} className="text-xs font-bold">
          Sign in
        </NavLink>
      </div>
    </Form>
  );
}
export default Register;

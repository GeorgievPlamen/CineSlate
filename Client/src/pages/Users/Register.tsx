import { Form, NavLink, useActionData } from 'react-router-dom';
import { UserResponse } from './Models/UserResponse';
import SubmitButton from '../../app/components/Buttons/SubmitButton';
import EmailField from '../../app/components/Fields/EmailField';
import NameField from '../../app/components/Fields/NameField';
import PasswordField from '../../app/components/Fields/PasswordField';
import Heading2 from '../../app/components/Heading2';
import Linebreak from '../../app/components/Linebreak';
import useHandleUserResponse from '../../app/hooks/useHandleUserResponse';

function Register() {
  const response = useActionData() as UserResponse;
  useHandleUserResponse(response);
  return (
    <Form
      noValidate
      method="post"
      className="mx-auto mt-10 flex w-80 flex-col items-center gap-3 rounded-xl border border-whitesmoke bg-background p-4"
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

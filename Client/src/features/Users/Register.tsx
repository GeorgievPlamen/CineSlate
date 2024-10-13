import { Form, NavLink, useActionData } from 'react-router-dom';
import EmailField from '../../components/Fields/EmailField';
import PasswordField from '../../components/Fields/PasswordField';
import Heading2 from '../../components/Heading2';
import SubmitButton from '../../components/Buttons/SubmitButton';
import Linebreak from '../../components/Linebreak';
import NameField from '../../components/Fields/NameField';
import { UserResponse } from './UserResponse';
import useHandleUserResponse from '../../hooks/useHandleUserResponse';

function Register() {
  const response = useActionData() as UserResponse;
  useHandleUserResponse(response);
  return (
    <Form
      noValidate
      method="post"
      className="mt-10 flex w-80 flex-col items-center gap-3 rounded-xl border border-whitesmoke bg-grayFrench p-4 shadow-xl dark:bg-bluePersian dark:shadow-light"
    >
      <Heading2 title="Register" />
      <NameField errors={response?.errors} />
      <NameField errors={response?.errors} isLastName />
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

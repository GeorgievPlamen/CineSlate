import EmailField from '@/components/Fields/EmailField';
import NameField from '@/components/Fields/NameField';
import PasswordField from '@/components/Fields/PasswordField';
import Heading2 from '@/components/Heading2';
import Linebreak from '@/components/Linebreak';
import SubmitButton from '@/components/Buttons/SubmitButton';
import { Link, useNavigate } from '@tanstack/react-router';
import { userErrors } from './Models/userErrors';
import { useUserStore } from '@/store/userStore';
import { registerAction } from './api/registerAction';
import { FormEvent, useState } from 'react';

function Register() {
  const [errors, setErrors] = useState<userErrors[] | null | string>();
  const setUser = useUserStore((state) => state.setUser);
  const navigate = useNavigate();

  async function handleSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    const res = await registerAction(new FormData(e.currentTarget));

    if (res.errors) {
      console.log(errors);
      setErrors(res.errors);
      return;
    }

    if (res.user) {
      setUser(res.user);
      navigate({
        to: '/',
      });
    }
  }

  return (
    <form
      noValidate
      method="post"
      className="border-foreground bg-background mx-auto mb-20 flex w-11/12 flex-col items-center gap-3 rounded-xl border p-4 sm:mt-10 sm:w-80"
      onSubmit={handleSubmit}
    >
      <Heading2 title="Register" />
      <NameField errors={errors} />
      <EmailField errors={errors} />
      <PasswordField errors={errors} />
      <PasswordField errors={errors} isConfirmPassword />
      <Linebreak />
      <SubmitButton text="Register" />
      <div className="flex w-full justify-end text-center">
        <p className="pr-2 text-xs font-extralight">Already have an account?</p>
        <Link to={'/login'} className="text-xs font-bold">
          Sign in
        </Link>
      </div>
    </form>
  );
}
export default Register;

import { userErrors } from './Models/userErrors';
import EmailField from '@/components/Fields/EmailField';
import PasswordField from '@/components/Fields/PasswordField';
import Heading2 from '@/components/Heading2';
import Linebreak from '@/components/Linebreak';
import ValidationError from '@/components/ValidationError';
import { Link, useNavigate } from '@tanstack/react-router';
import SubmitButton from '@/components/Buttons/SubmitButton';
import { loginAction } from './api/loginAction';
import { FormEvent, useState } from 'react';
import { useUserStore } from '@/store/userStore';

export default function Login() {
  const [errors, setErrors] = useState<userErrors[] | null | string>();
  const setUser = useUserStore((state) => state.setUser);
  const navigate = useNavigate();

  async function handleSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    const res = await loginAction(new FormData(e.currentTarget));

    if (res.errors) {
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
      className="border-foreground bg-background mx-auto mt-10 flex w-80 flex-col items-center gap-3 rounded-xl border p-4"
      onSubmit={handleSubmit}
    >
      <Heading2 title="Login" />
      <EmailField errors={errors} />
      <PasswordField errors={errors} />
      <ValidationError
        isError={errors?.includes(userErrors.NotFound)}
        message={'User with email not found or password is wrong.'}
      />
      <Linebreak />
      <SubmitButton text="Sign in" />
      <div className="flex w-full justify-end text-center">
        <p className="pr-2 text-xs font-extralight">New?</p>
        <Link to={'/register'} className="text-xs font-bold">
          Create your account
        </Link>
      </div>
    </form>
  );
}

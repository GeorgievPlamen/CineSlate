import { EyeIcon } from '@heroicons/react/16/solid';
import { useState } from 'react';
import ValidationError from '../ValidationError';
import { userErrors } from '../../features/Users/userErrors';
import { UserFieldErrorProps } from './UserFieldErrorProps';

function PasswordField({ errors }: UserFieldErrorProps) {
  const [passwordInputType, setPasswordInputType] = useState('password');

  function handlePasswordVisability() {
    if (passwordInputType === 'password') {
      setPasswordInputType('text');
    } else {
      setPasswordInputType('password');
    }
  }

  function getMessageBasedOn(errors: string | userErrors[] | null | undefined) {
    if (errors?.includes(userErrors.PasswordMissing)) {
      return userErrors.PasswordMissing.toString();
    }

    if (errors?.includes(userErrors.InvalidPassword)) {
      return userErrors.InvalidPassword.toString();
    }

    return 'Unknown Error';
  }

  return (
    <div className="flex w-full flex-col">
      <label htmlFor="password" className="mb-1 ml-1 font-bold">
        Password
      </label>
      <div className="relative flex items-center">
        <input
          type={passwordInputType}
          name="password"
          id="password"
          className="h-8 w-full rounded-md pl-2 pr-8 text-dark focus:outline-none"
        />
        <EyeIcon
          type="button"
          onClick={() => handlePasswordVisability()}
          aria-description="Show or hide text inside the password input field."
          className="absolute right-1 size-6 cursor-pointer text-gray-400"
        />
      </div>
      <ValidationError
        isError={
          errors?.includes(userErrors.PasswordMissing) ||
          errors?.includes(userErrors.InvalidPassword)
        }
        message={getMessageBasedOn(errors)}
      />
    </div>
  );
}
export default PasswordField;

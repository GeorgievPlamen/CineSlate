import { EyeIcon } from '@heroicons/react/16/solid';
import { useState } from 'react';
import ValidationError from '../ValidationError';
import { UserFieldErrorProps } from './UserFieldErrorProps';
import { userErrors } from '@/features/Users/Models/userErrors';

interface Props extends UserFieldErrorProps {
  readonly isConfirmPassword?: boolean;
}
function PasswordField({ errors, isConfirmPassword }: Props) {
  const fieldName = isConfirmPassword ? 'confirmPassword' : 'password';
  const [passwordInputType, setPasswordInputType] = useState('password');
  function handlePasswordVisability() {
    if (passwordInputType === 'password') {
      setPasswordInputType('text');
    } else {
      setPasswordInputType('password');
    }
  }

  function getMessageBasedOn(errors: string | userErrors[] | null | undefined) {
    if (errors?.includes(userErrors.MissingPassword)) {
      return userErrors.MissingPassword.toString();
    }

    if (!isConfirmPassword && errors?.includes(userErrors.InvalidPassword)) {
      return userErrors.InvalidPassword.toString();
    }

    if (isConfirmPassword && errors?.includes(userErrors.PasswordsMatch)) {
      return userErrors.PasswordsMatch.toString();
    }

    return '';
  }

  return (
    <div className="flex w-full flex-col">
      <label htmlFor={fieldName} className="mb-1 ml-1 font-bold">
        {isConfirmPassword ? 'Confirm password' : 'Password'}
      </label>
      <div className="relative flex items-center">
        <input
          type={passwordInputType}
          name={fieldName}
          id={fieldName}
          className="h-8 w-full rounded-md pl-2 pr-8 text-dark focus:outline-primary focus:outline-2 bg-foreground"
        />
        <EyeIcon
          type="button"
          onClick={() => handlePasswordVisability()}
          aria-description="Show or hide text inside the password input field."
          className="absolute right-1 size-6 cursor-pointer text-gray-400"
        />
      </div>
      <ValidationError
        isError={getMessageBasedOn(errors).length > 0}
        message={getMessageBasedOn(errors)}
      />
    </div>
  );
}
export default PasswordField;

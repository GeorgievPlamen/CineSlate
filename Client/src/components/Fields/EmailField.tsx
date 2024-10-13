import { userErrors } from '../../features/Users/userErrors';
import ValidationError from '../ValidationError';
import { UserFieldErrorProps } from './UserFieldErrorProps';

function EmailField({ errors }: UserFieldErrorProps) {
  return (
    <div className="flex w-full flex-col">
      <label htmlFor="email" className="mb-1 ml-1 font-bold">
        Email
      </label>
      <input
        type="email"
        name="email"
        id="email"
        className="h-8 rounded-md px-2 text-dark focus:outline-none"
      />
      <ValidationError
        isError={errors?.includes(userErrors.InvalidEmail)}
        message={userErrors.InvalidEmail}
      />
    </div>
  );
}
export default EmailField;

import { userErrors } from '@/modules/Users/Models/userErrors';
import ValidationError from '../ValidationError';
import { UserFieldErrorProps } from './UserFieldErrorProps';

function EmailField({ errors }: UserFieldErrorProps) {
  function getMessageBasedOn(errors: string | userErrors[] | null | undefined) {
    if (errors?.includes(userErrors.InvalidEmail)) {
      return userErrors.InvalidEmail.toString();
    }

    if (errors?.includes(userErrors.EmailInUse)) {
      return userErrors.EmailInUse.toString();
    }

    return '';
  }
  return (
    <div className="flex w-full flex-col">
      <label htmlFor="email" className="mb-1 ml-1 font-bold">
        Email
      </label>
      <input
        type="email"
        name="email"
        id="email"
        className="h-8 w-full rounded-md pl-2 pr-8 text-dark focus:outline-primary focus:outline-2 bg-foreground"
      />
      <ValidationError
        isError={getMessageBasedOn(errors).length > 0}
        message={getMessageBasedOn(errors)}
      />
    </div>
  );
}
export default EmailField;

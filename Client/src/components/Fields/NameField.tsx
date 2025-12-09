import { userErrors } from '@/features/Users/Models/userErrors';
import ValidationError from '../ValidationError';
import { UserFieldErrorProps } from './UserFieldErrorProps';

function NameField({ errors }: UserFieldErrorProps) {
  const fieldName = 'userName';

  function getMessageBasedOn(errors: string | userErrors[] | null | undefined) {
    if (errors?.includes(userErrors.MissingUsername)) {
      return userErrors.MissingUsername.toString();
    }

    if (errors?.includes(userErrors.InvalidUsername)) {
      return userErrors.InvalidUsername.toString();
    }

    return '';
  }

  return (
    <div className="flex w-full flex-col">
      <label htmlFor={fieldName} className="mb-1 ml-1 font-bold">
        Username
      </label>
      <input
        type="text"
        name={fieldName}
        id={fieldName}
        className="h-8 w-full rounded-md pl-2 pr-8 text-dark focus:outline-primary focus:outline-2 bg-foreground"
      />
      <ValidationError
        isError={getMessageBasedOn(errors).length > 0}
        message={getMessageBasedOn(errors)}
      />
    </div>
  );
}
export default NameField;

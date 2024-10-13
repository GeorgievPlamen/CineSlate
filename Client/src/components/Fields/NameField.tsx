import { userErrors } from '../../features/Users/userErrors';
import ValidationError from '../ValidationError';
import { UserFieldErrorProps } from './UserFieldErrorProps';

interface Props extends UserFieldErrorProps {
  readonly type: string;
}

function NameField({ type, errors }: Props) {
  function getMessageBasedOn(errors: string | userErrors[] | null | undefined) {
    if (errors?.includes(userErrors.MissingFirstName)) {
      return userErrors.MissingFirstName.toString();
    }

    if (errors?.includes(userErrors.MissingLastName)) {
      return userErrors.MissingLastName.toString();
    }

    return '';
  }

  return (
    <div className="flex w-full flex-col">
      <label htmlFor={type} className="mb-1 ml-1 font-bold">
        {type}
      </label>
      <input
        type="text"
        name={type}
        id={type}
        className="h-8 rounded-md px-2 text-dark focus:outline-none"
      />
      <ValidationError
        isError={getMessageBasedOn(errors).length > 0}
        message={getMessageBasedOn(errors)}
      />
    </div>
  );
}
export default NameField;

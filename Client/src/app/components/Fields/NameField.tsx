import { userErrors } from '../../../features/Users/Models/userErrors';
import ValidationError from '../ValidationError';
import { UserFieldErrorProps } from './UserFieldErrorProps';

interface Props extends UserFieldErrorProps {
  readonly isLastName?: boolean;
}

function NameField({ isLastName, errors }: Props) {
  const fieldName = isLastName ? 'lastName' : 'firstName';

  function getMessageBasedOn(errors: string | userErrors[] | null | undefined) {
    if (!isLastName && errors?.includes(userErrors.MissingFirstName)) {
      return userErrors.MissingFirstName.toString();
    }

    if (!isLastName && errors?.includes(userErrors.InvalidFirstName)) {
      return userErrors.InvalidFirstName.toString();
    }

    if (isLastName && errors?.includes(userErrors.MissingLastName)) {
      return userErrors.MissingLastName.toString();
    }

    if (isLastName && errors?.includes(userErrors.InvalidLastName)) {
      return userErrors.InvalidLastName.toString();
    }

    return '';
  }

  return (
    <div className="flex w-full flex-col">
      <label htmlFor={fieldName} className="mb-1 ml-1 font-bold">
        {isLastName ? 'Last Name' : 'First Name'}
      </label>
      <input
        type="text"
        name={fieldName}
        id={fieldName}
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

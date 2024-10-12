interface Props {
  readonly isError: boolean;
  readonly message: string;
}

function ValidationError({ isError, message }: Props) {
  if (isError)
    return (
      <p className="text-error inline text-sm font-extralight">{message}</p>
    );
}
export default ValidationError;

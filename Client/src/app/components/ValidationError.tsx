interface Props {
  readonly isError: boolean | undefined;
  readonly message: string;
}

function ValidationError({ isError, message }: Props) {
  if (isError)
    return (
      <p
        className="inline text-sm font-extralight text-error"
        aria-live="polite"
      >
        {message}
      </p>
    );
}
export default ValidationError;

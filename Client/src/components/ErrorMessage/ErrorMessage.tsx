interface Props {
  message?: string;
}

export default function ErrorMessage({
  message = 'Something wrong happened.',
}: Props) {
  return (
    <div className="flex h-80 w-full items-center justify-center">
      <article className="w-fit rounded border border-destructive bg-dark px-4 py-2">
        <p className="font-bold text-destructive">{message} </p>
      </article>
    </div>
  );
}

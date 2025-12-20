import Spinner from '../Spinner';

interface Props {
  readonly text: string;
  isLoading?: boolean;
  className?: string;
  onClick?: () => void;
}

function SubmitButton({ text, onClick, className, isLoading = false }: Props) {
  return (
    <button
      {...(onClick !== undefined ? { onClick: () => onClick() } : null)}
      type="submit"
      className={
        'flex h-8 w-full items-center justify-center rounded-full bg-primary text-foreground hover:outline hover:outline-foreground active:bg-opacity-80' +
        ' ' +
        className
      }
    >
      {isLoading ? <Spinner /> : text}
    </button>
  );
}
export default SubmitButton;

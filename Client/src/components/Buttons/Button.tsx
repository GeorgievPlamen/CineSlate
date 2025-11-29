import { ReactNode } from 'react';
import Spinner from '../Spinner';

interface Props {
  onClick?: () => void;
  isLoading?: boolean;
  children?: ReactNode;
  className?: string;
}

export default function Button({
  isLoading,
  children,
  className,
  onClick,
}: Props) {
  return (
    <button
      onClick={onClick}
      type="submit"
      className={
        'flex h-8 items-center justify-center rounded-full bg-primary text-whitesmoke hover:outline hover:outline-1 hover:outline-whitesmoke active:bg-opacity-80' +
        ' ' +
        className
      }
    >
      {isLoading ? <Spinner /> : children}
    </button>
  );
}

import { useNavigation } from 'react-router-dom';
import Spinner from '../Spinner';

interface Props {
  readonly text: string;
  className?: string;
  onClick?: () => void;
}

function SubmitButton({ text, onClick, className }: Props) {
  const navigation = useNavigation();

  return (
    <button
      {...(onClick !== undefined ? { onClick: () => onClick() } : null)}
      type="submit"
      className={
        'flex h-8 w-full items-center justify-center rounded-full bg-primary text-whitesmoke hover:outline hover:outline-1 hover:outline-whitesmoke active:bg-opacity-80' +
        ' ' +
        className
      }
    >
      {navigation.state === 'submitting' ? <Spinner /> : text}
    </button>
  );
}
export default SubmitButton;

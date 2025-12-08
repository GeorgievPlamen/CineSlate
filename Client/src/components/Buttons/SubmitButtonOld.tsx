import { useNavigation } from 'react-router-dom';
import Spinner from '../Spinner';

interface Props {
  readonly text: string;
  className?: string;
  onClick?: () => void;
}

function SubmitButtonOld({ text, onClick, className }: Props) {
  const navigation = useNavigation();

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
      {navigation.state === 'submitting' ? <Spinner /> : text}
    </button>
  );
}
export default SubmitButtonOld;

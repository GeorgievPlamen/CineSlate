import { useNavigation } from 'react-router-dom';
import Spinner from '../Spinner';

interface Props {
  readonly text: string;
}

function SubmitButton({ text }: Props) {
  const navigation = useNavigation();

  return (
    <button
      type="submit"
      className="flex h-8 w-full items-center justify-center rounded-full bg-primary text-whitesmoke hover:outline hover:outline-1 hover:outline-whitesmoke active:bg-opacity-80"
    >
      {navigation.state === 'submitting' ? <Spinner /> : text}
    </button>
  );
}
export default SubmitButton;

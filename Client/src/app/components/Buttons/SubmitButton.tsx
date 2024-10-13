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
      className="flex h-8 w-full items-center justify-center rounded-full bg-indigo-600 text-whitesmoke hover:bg-indigo-500 active:bg-indigo-400"
    >
      {navigation.state === 'submitting' ? <Spinner /> : text}
    </button>
  );
}
export default SubmitButton;

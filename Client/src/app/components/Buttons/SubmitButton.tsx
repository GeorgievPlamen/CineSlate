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
      className="bg-primary-hover flex h-8 w-full items-center justify-center rounded-full text-whitesmoke hover:bg-opacity-80 active:bg-opacity-50"
    >
      {navigation.state === 'submitting' ? <Spinner /> : text}
    </button>
  );
}
export default SubmitButton;

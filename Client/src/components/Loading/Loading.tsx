import Spinner from '../Spinner';

export default function Loading() {
  return (
    <div className="flex h-80 w-full items-center justify-center">
      <Spinner />
    </div>
  );
}

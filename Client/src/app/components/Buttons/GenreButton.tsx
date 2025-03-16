import { useNavigate, useSearchParams } from 'react-router-dom';

interface Props {
  name: string;
  genreId: number;
}

export default function GenreButton({ name, genreId }: Props) {
  const navigate = useNavigate();
  const [searchParams, setSearchParams] = useSearchParams();
  const genreIds = searchParams.getAll('genreIds');

  function handleClick() {
    if (genreIds.length === 0) {
      navigate('/movies?genreIds=' + genreId);
      return;
    }

    if (genreIds.includes(String(genreId))) {
      const newParams = new URLSearchParams();

      genreIds
        .filter((id) => id !== String(genreId))
        .forEach((id) => newParams.append('genreIds', id));

      setSearchParams(newParams);

      return;
    }

    searchParams.append('genreIds', String(genreId));
    setSearchParams(searchParams);
  }

  return (
    <button
      onClick={handleClick}
      className={
        'm-2 h-8 rounded-full px-2 text-sm hover:outline hover:outline-1 active:bg-opacity-80 ' +
        (genreIds.includes(String(genreId)) ? 'bg-primary' : 'bg-background')
      }
    >
      {name}
    </button>
  );
}

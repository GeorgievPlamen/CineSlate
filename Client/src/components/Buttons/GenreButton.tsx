import { useNavigate } from '@tanstack/react-router';

interface Props {
  name: string;
  genreId: number;
  currentGenreIds?: number[];
}

export default function GenreButton({ name, genreId, currentGenreIds }: Props) {
  const navigate = useNavigate();

  function handleClick() {
    let newIds: number[] = [];

    if (!currentGenreIds) {
      newIds.push(genreId);
    } else if (currentGenreIds.includes(genreId)) {
      newIds = newIds.concat(currentGenreIds.filter((x) => x !== genreId));
    } else {
      newIds = newIds.concat(currentGenreIds);
      newIds.push(genreId);
    }

    navigate({
      to: '/movies',
      search: {
        genreIds: newIds.length > 0 ? newIds : undefined,
        search: undefined,
      },
    });
  }

  return (
    <button
      onClick={handleClick}
      className={
        'm-2 h-8 rounded-full px-2 text-sm hover:outline active:bg-opacity-80 ' +
        (currentGenreIds?.includes(genreId) ? 'bg-primary' : 'bg-background')
      }
    >
      {name}
    </button>
  );
}

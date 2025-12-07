import { getRouteApi, useNavigate } from '@tanstack/react-router';

interface Props {
  name: string;
  genreId: number;
}

const { useSearch } = getRouteApi('/movies/');

export default function GenreButton({ name, genreId }: Props) {
  const navigate = useNavigate();
  const { genreIds } = useSearch({ select: (params) => params });

  function handleClick() {
    let newIds: number[] = [];

    if (!genreIds) {
      newIds.push(genreId);
    } else if (genreIds.includes(genreId)) {
      newIds = newIds.concat(genreIds.filter((x) => x !== genreId));
    } else {
      newIds = newIds.concat(genreIds);
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
        (genreIds?.includes(genreId) ? 'bg-primary' : 'bg-background')
      }
    >
      {name}
    </button>
  );
}

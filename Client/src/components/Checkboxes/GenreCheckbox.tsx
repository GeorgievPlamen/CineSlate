import { useNavigate } from '@tanstack/react-router';
import { Checkbox } from '../ui/checkbox';
import { useState } from 'react';

interface Props {
  name: string;
  genreId: number;
  currentGenreIds?: number[];
}

function GenreCheckbox({ name, genreId, currentGenreIds }: Props) {
  const navigate = useNavigate();
  const [isChecked, setIsChecked] = useState(false);

  function handleClick() {
    let newIds: number[] = [];

    if (!currentGenreIds) {
      newIds.push(genreId);
      setIsChecked(true);
    } else if (currentGenreIds.includes(genreId)) {
      setIsChecked(false);
      newIds = newIds.concat(currentGenreIds.filter((x) => x !== genreId));
    } else {
      newIds = newIds.concat(currentGenreIds);
      setIsChecked(true);
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
      className="flex items-center gap-2 cursor-pointer"
      onClick={handleClick}
    >
      <Checkbox
        name="test"
        className="border-muted-foreground rounded-sm cursor-pointer"
        checked={isChecked}
      />
      <span className="text-sm w-28 text-start">{name}</span>
    </button>
  );
}

export default GenreCheckbox;

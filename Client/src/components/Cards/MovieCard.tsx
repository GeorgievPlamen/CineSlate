import { IMG_PATH_W500 } from '@/config';
import SquarePlusIcon from '@/Icons/SquarePlusIcon';
import { Link } from '@tanstack/react-router';
import Tooltip from '../Tooltip/Tooltip';

interface Props {
  title: string;
  posterPath: string;
  releaseDate: Date;
  id: number;
  rating: number;
  onAddToWatchlistClick: () => Promise<void>;
}

export default function MovieCard({
  title,
  posterPath,
  releaseDate,
  id,
  rating,
  onAddToWatchlistClick,
}: Props) {
  return (
    <article
      className="mx-auto relative flex w-60 flex-col rounded-lg border border-grey bg-background shadow shadow-dark hover:border-primary active:border-opacity-80"
      id={`${id}`}
    >
      <Link
        to="/movies/$id"
        className="absolute inset-0 z-0"
        params={{ id: `${id}` }}
      />
      <img
        className="mb-2 rounded-t-lg"
        src={IMG_PATH_W500 + posterPath}
        alt="poster"
      />
      <div className="mx-2 mb-1 flex justify-between font-primary text-sm">
        <p>⭐{rating}</p>
        <p className="text-muted-foreground">{releaseDate.toString()}</p>
      </div>
      <div className="flex justify-between h-full">
        <p className="mx-2 flex h-full items-center font-heading text-lg">
          {title}
        </p>
        <button
          onClick={onAddToWatchlistClick}
          className="mr-1 text-primary hover:text-primary-hover active:text-primary-active z-10"
        >
          <Tooltip content="Add to watchlist">
            <SquarePlusIcon />
          </Tooltip>
        </button>
      </div>
    </article>
  );
}

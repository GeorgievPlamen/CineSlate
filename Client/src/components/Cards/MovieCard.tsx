import { IMG_PATH_W500 } from '@/config';
import { Link } from '@tanstack/react-router';
import { Skeleton } from '../ui/skeleton';

interface Props {
  title: string;
  posterPath: string;
  releaseDate: Date;
  id: number;
  rating: number;
}

export default function MovieCard({
  title,
  posterPath,
  releaseDate,
  id,
  rating,
}: Props) {
  return (
    <article
      className="transition-transform duration-300 hover:scale-105 mx-auto relative flex w-60 flex-col rounded-lg border border-grey bg-panel shadow hover:border-primary active:border-opacity-80 h-full"
      id={`${id}`}
    >
      <Link
        to="/movies/$id"
        className="absolute inset-0 z-0"
        params={{ id: `${id}` }}
      />
      <img
        className="h-full mb-1 rounded-t-lg "
        src={IMG_PATH_W500 + posterPath}
        alt="poster"
      />
      <div className="flex flex-col gap-2">
        <div className="flex">
          <span className="mx-2 flex h-full items-center truncate">
            {title}
          </span>
        </div>
        <p className="mx-2 mb-1 flex h-full items-end justify-between font-primary">
          <span className="text-muted-foreground  text-xs">
            {releaseDate.toString()}
          </span>
          <span className="text-sm ">⭐{rating.toFixed(1)}</span>
        </p>
      </div>
    </article>
  );
}

export const MovieCardSkeleton = () => <Skeleton className="min-h-114  w-60" />;

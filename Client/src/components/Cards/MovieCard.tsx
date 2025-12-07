import { useNavigate } from 'react-router-dom';
import { IMG_PATH_W500 } from '@/config';
import SquarePlusIcon from '@/Icons/SquarePlusIcon';

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
  const nav = useNavigate();
  return (
    <article
      onClick={() => nav(`/movies/${id}`)}
      className="mx-auto flex w-60 flex-col rounded-lg border border-grey bg-background shadow shadow-dark hover:border-primary active:border-opacity-80"
      id={`${id}`}
    >
      <img
        className="mb-2 rounded-t-lg"
        src={IMG_PATH_W500 + posterPath}
        alt="poster"
      />
      <div className="mx-2 mb-1 flex justify-between font-primary text-sm">
        <p>⭐{rating}</p>
        <p>{releaseDate.toString()}</p>
      </div>
      <div className="flex justify-between">
        <p className="mx-2 flex h-full items-center font-heading text-lg">
          {title}
        </p>
        <button className="mr-1 text-primary hover:text-opacity-80 active:text-opacity-50">
          <SquarePlusIcon />
        </button>
      </div>
    </article>
  );
}

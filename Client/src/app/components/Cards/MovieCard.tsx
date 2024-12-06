import { IMG_PATH_W500 } from '../../config';

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
      className="border-grey mx-auto flex w-60 flex-col rounded-lg border bg-background shadow shadow-dark hover:border-primary active:border-opacity-80"
      id={`${id}`}
    >
      <img
        className="mb-2 rounded-t-lg"
        src={IMG_PATH_W500 + posterPath}
        alt="poster"
      />
      <div className="mx-2 mb-1 flex justify-between font-roboto text-sm">
        <p>⭐{rating}</p>
        <p>{releaseDate.toString()}</p>
      </div>
      <p className="mx-2 font-arvo text-lg">{title}</p>
    </article>
  );
}

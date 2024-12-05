import { IMG_PATH_W500 } from '../../config';

interface Props {
  title: string;
  posterPath: string;
  releaseDate: string;
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
    <article className="flex w-60 flex-col" id={`${id}`}>    
      <img
        className="mb-2 rounded-lg border border-slate-500 shadow hover:border-primary active:border-opacity-80"
        src={IMG_PATH_W500 + posterPath}
        alt="poster"
      />
      <div className="mb-1 flex justify-between font-roboto text-sm">
        <p>⭐{rating}</p>
        <p>{releaseDate}</p>
      </div>
      <p className="font-arvo text-lg">{title}</p>
    </article>
  );
}

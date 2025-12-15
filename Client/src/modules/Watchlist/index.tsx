import { IMG_PATH_W500 } from '@/config';
import { getRouteApi, Link } from '@tanstack/react-router';
import { Popcorn } from 'lucide-react';

const routeApi = getRouteApi('/watchlist/');

export default function Watchlist() {
  const watchlist = routeApi.useLoaderData();

  return (
    // TODO: style watchlist card, add setWatched button, Date Watched and Date Added to watchlist placeholders
    <section className="flex flex-col items-center justify-center">
      <div className="flex items-center gap-2">
        <h2 className="font-heading my-4 text-2xl">Movies To Watch</h2>
        <Popcorn />
      </div>
      {watchlist?.map((w) => (
        <Link
          to={'/movies/$id'}
          params={{ id: `${w.id}` }}
          className={
            'flex rounded-2xl border border-grey bg-background p-1 hover:border-primary '
          }
        >
          <img
            src={IMG_PATH_W500 + w.posterPath}
            alt={`${w.title}-movie-poster`}
            className="h-20 w-20 rounded-full object-cover"
          />
          <div className="mx-4 my-2 w-80">
            <div className="mb-2 flex flex-col justify-between">
              <p className="text-xl">{w.title}</p>
              <p className="font-primary text-sm text-muted-foreground">
                {w.rating}
              </p>
            </div>
          </div>
        </Link>
      ))}
    </section>
  );
}

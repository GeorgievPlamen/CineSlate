import Button from '@/components/Buttons/Button';
import { IMG_PATH_W500 } from '@/config';
import { getRouteApi, Link } from '@tanstack/react-router';
import { Popcorn } from 'lucide-react';

const routeApi = getRouteApi('/watchlist/');

export default function Watchlist() {
  const watchlist = routeApi.useLoaderData();

  return (
    // TODO: style watchlist card, add setWatched button, Date Watched and Date Added to watchlist placeholders
    <section className="flex flex-col m-auto items-center justify-cente w-5/6 max-w-160 gap-6">
      <div className="flex items-center gap-2">
        <h2 className="font-heading my-4 text-2xl">Movies To Watch</h2>
        <Popcorn />
      </div>
      {watchlist?.map((w) => (
        <div className="flex rounded-2xl border border-grey bg-background min-w-70 w-full">
          <img
            src={IMG_PATH_W500 + w.posterPath}
            alt="poster"
            className="w-28 rounded-l-2xl border-r border-r-grey object-cover"
          />
          <div className="mx-4 my-2 w-full">
            <div className="mb-2 flex justify-between flex-col md:flex-row">
              <p className="text-xl">
                <Link
                  to={'/movies/$id'}
                  params={{ id: `${w.id}` }}
                  className={'font-heading hover:text-primary'}
                >
                  {w.title}
                </Link>
                <Link
                  to={'/'}
                  className="ml-2 text-lg text-muted-foreground hover:text-primary"
                >
                  {w.releaseDate.toString().split('-')[0]}
                </Link>
              </p>
              <div className="flex gap-2">
                <p>⭐{w.rating}</p>
              </div>
            </div>
            <div className='h-full'>
              <Button className="self-end">Watched?</Button>
            </div>
          </div>
        </div>
      ))}
    </section>
  );
}

import Button from '@/components/Buttons/Button';
import { IMG_PATH_W500 } from '@/config';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { getRouteApi, Link } from '@tanstack/react-router';
import { Popcorn } from 'lucide-react';
import { watchlistsClient } from './api/watchlistClient';
import Spinner from '@/components/Spinner';

const routeApi = getRouteApi('/watchlist/');

export default function Watchlist() {
  const watchlist = routeApi.useLoaderData();
  const queryClient = useQueryClient();

  const { data: watchlistWatched, isLoading } = useQuery({
    queryKey: ['getWatchlist'],
    queryFn: () => watchlistsClient.getWatchlist(),
  });

  const { mutateAsync } = useMutation({
    mutationFn: (movieId: number) => watchlistsClient.setWatched(movieId, true),
    onSuccess: async () =>
      queryClient.invalidateQueries({ queryKey: ['getWatchlist'] }),
  });

  return (
    // TODO set watched
    <section className="flex flex-col m-auto items-center justify-cente w-5/6 max-w-160 gap-6 mb-14">
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
          <div className="mx-4 my-2 w-full flex flex-col">
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
            <div className="h-full flex items-end justify-between">
              {isLoading ? (
                <Spinner />
              ) : watchlistWatched?.watchlist.find((x) => x.key === w.id)
                  ?.value ? (
                <p className="text-sm text-muted-foreground">Already watched</p>
              ) : (
                <Button className="px-2" onClick={() => mutateAsync(w.id)}>
                  Watched?
                </Button>
              )}
              <p className="text-xs text-muted-foreground">
                Added on: <span className="text-foreground">2024-05-05</span>
              </p>
            </div>
          </div>
        </div>
      ))}
    </section>
  );
}

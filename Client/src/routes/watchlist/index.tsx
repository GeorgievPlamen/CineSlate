import { ProblemDetails } from '@/api/errors';
import { moviesClient } from '@/modules/Movies/api/moviesClient';
import Watchlist from '@/modules/Watchlist';
import { watchlistsClient } from '@/modules/Watchlist/api/watchlistClient';
import { isAuthenticated } from '@/store/userStore';
import { createFileRoute, Link } from '@tanstack/react-router';

export const Route = createFileRoute('/watchlist/')({
  loader: async () => {
    try {
      const { watchlist } = await watchlistsClient.getWatchlist();
      const moviesFromWatchlist = await moviesClient.getMoviesInWatchlist();

      const moviesWatchlist = moviesFromWatchlist.map((x) => {
        const hasWatched = watchlist.find((w) => w.key === x.id)?.value ?? false;
        return { ...x, hasWatched: hasWatched };
      });

      return moviesWatchlist;
    } catch (error) {
      const err = error as ProblemDetails;

      if (err.status.code === 404) {
        return [];
      }
    }
  },
  component: RouteComponent,
});

function RouteComponent() {
  if (!isAuthenticated())
    return (
      <div className="w-full flex justify-center mt-10">
        <p>
          <Link to="/login" className="underline">
            Sign in
          </Link>{' '}
          to view your watchlist.
        </p>
      </div>
    );

  return <Watchlist />;
}

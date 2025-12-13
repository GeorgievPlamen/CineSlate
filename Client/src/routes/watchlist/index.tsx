import { ProblemDetails } from '@/api/errors';
import Watchlist from '@/modules/Watchlist';
import { watchlistsClient } from '@/modules/Watchlist/api/watchlistClient';
import { isAuthenticated } from '@/store/userStore';
import { createFileRoute, Link } from '@tanstack/react-router';

export const Route = createFileRoute('/watchlist/')({
  loader: async () => {
    try {
      const watchlist = await watchlistsClient.getWatchlist();
      console.log(watchlist);
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

import { ProblemDetails } from '@/api/errors';
import { moviesClient } from '@/modules/Movies/api/moviesClient';
import Watchlist from '@/modules/Watchlist';
import { isAuthenticated } from '@/store/userStore';
import { createFileRoute, redirect } from '@tanstack/react-router';

export const Route = createFileRoute('/watchlist/')({
  beforeLoad: () => {
    if (!isAuthenticated()) {
      throw redirect({ to: '/login' });
    }
  },
  loader: async ({ context }) =>
    context.queryClient.ensureQueryData({
      queryKey: ['getMoviesInWatchlist'],
      queryFn: () => moviesClient.getMoviesInWatchlist(),
    }),
  component: RouteComponent,
  errorComponent: (err) => {
    const error = err.error as unknown as ProblemDetails;

    if (error.status.code === 404) {
      return (
        <div className="w-fit mt-12 m-auto flex">
          <h2 className="font-heading my-4 text-2xl">
            You have no movies in your watchlist.
          </h2>
        </div>
      );
    } else {
      throw err;
    }
  },
});

function RouteComponent() {
  return <Watchlist />;
}

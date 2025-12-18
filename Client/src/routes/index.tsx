import Home from '@/modules/Home';
import { MoviesBy, moviesClient } from '@/modules/Movies/api/moviesClient';
import { ReviewsBy, reviewsClient } from '@/modules/Review/api/reviewsClient';
import { createFileRoute } from '@tanstack/react-router';

export const Route = createFileRoute('/')({
  loader: async ({ context }) => {
    const { values: movies } = await context.queryClient.ensureQueryData({
      queryKey: ['movies-nowplaying-home'],
      queryFn: () => moviesClient.getPagedMovies(MoviesBy.NowPlaying, 1),
    });

    const { values: reviews } = await context.queryClient.ensureQueryData({
      queryKey: ['reviews-latest-home'],
      queryFn: () => reviewsClient.reviewsBy(1, ReviewsBy.Latest),
    });

    return { movies, reviews };
  },
  component: Index,
});

function Index() {
  return <Home />;
}

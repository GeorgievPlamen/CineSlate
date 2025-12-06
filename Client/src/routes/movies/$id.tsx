import MovieDetails from '@/features/Movies/Details/MovieDetails';
import { createFileRoute } from '@tanstack/react-router';

export const Route = createFileRoute('/movies/$id')({
  component: RouteComponent,
});

function RouteComponent() {
  const { id } = Route.useParams();
  return <MovieDetails />;
}

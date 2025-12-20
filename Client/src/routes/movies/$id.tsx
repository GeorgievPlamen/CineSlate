import MovieDetails from '@/modules/Movies/Details';
import { createFileRoute } from '@tanstack/react-router';

export const Route = createFileRoute('/movies/$id')({
  component: RouteComponent,
});

function RouteComponent() {
  return <MovieDetails />;
}

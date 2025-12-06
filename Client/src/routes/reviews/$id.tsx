import ReviewDetails from '@/features/Reviews/Details/ReviewDetails';
import { createFileRoute } from '@tanstack/react-router';

export const Route = createFileRoute('/reviews/$id')({
  component: RouteComponent,
});

function RouteComponent() {
  const { id } = Route.useParams();
  return <ReviewDetails />;
}

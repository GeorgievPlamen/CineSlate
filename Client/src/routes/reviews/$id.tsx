import ReviewDetails from '@/modules/Review/ReviewDetails';
import { createFileRoute } from '@tanstack/react-router';

export const Route = createFileRoute('/reviews/$id')({
  component: RouteComponent,
});

function RouteComponent() {
  return <ReviewDetails />;
}

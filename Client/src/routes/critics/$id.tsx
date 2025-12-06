import CriticDetails from '@/features/CriticDetails/CriticDetails';
import { createFileRoute } from '@tanstack/react-router';

export const Route = createFileRoute('/critics/$id')({
  component: RouteComponent,
});

function RouteComponent() {
  const { id } = Route.useParams();
  return <CriticDetails />;
}

import CriticDetails from '@/modules/Critics/Details';
import { createFileRoute } from '@tanstack/react-router';

export const Route = createFileRoute('/critics/$id')({
  component: RouteComponent,
});

function RouteComponent() {
  return <CriticDetails />;
}

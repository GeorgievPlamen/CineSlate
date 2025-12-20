import MyDetails from '@/modules/MyDetails';
import { isAuthenticated } from '@/store/userStore';
import { createFileRoute, redirect } from '@tanstack/react-router';

export const Route = createFileRoute('/my-details/')({
  beforeLoad: () => {
    if (!isAuthenticated()) {
      throw redirect({ to: '/login' });
    }
  },
  component: RouteComponent,
});

function RouteComponent() {
  return <MyDetails />;
}

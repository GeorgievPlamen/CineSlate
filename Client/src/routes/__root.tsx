import { LOCAL_REFRESH } from '@/config';
import Footer from '@/modules/Layout/Footer';
import Header from '@/modules/Layout/Header';
import Main from '@/modules/Layout/Main';
import { QueryClient } from '@tanstack/react-query';
import { createRootRouteWithContext } from '@tanstack/react-router';

export interface RouterContext {
  queryClient: QueryClient;
}

export const Route = createRootRouteWithContext<RouterContext>()({
  component: RootComponent,
});

function RootComponent() {
  return (
    <div className="flex min-h-screen flex-col">
      <Header />
      <Main />
      <Footer />
    </div>
  );
}

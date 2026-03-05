import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { RouterProvider, createRouter } from '@tanstack/react-router';
import { routeTree } from './routeTree.gen';
import RealtimeProvider from './common/Realtime/RealtimeProvider';
import AuthBootstrap from './common/AuthBootstrap';
import logger from './common/logger';

declare module '@tanstack/react-router' {
  interface Register {
    router: typeof router;
  }
}

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: false,
    },
  },
});

const apiRoute = import.meta.env.VITE_CINESLATE_API_URL;
logger.log(apiRoute);
const router = createRouter({ routeTree, context: { queryClient } });
const rootElement = document.getElementById('root')!;

if (!rootElement.innerHTML) {
  const root = createRoot(rootElement);
  root.render(
    <StrictMode>
      <AuthBootstrap>
        <RealtimeProvider>
          <QueryClientProvider client={queryClient}>
            <RouterProvider router={router} />
          </QueryClientProvider>
        </RealtimeProvider>
      </AuthBootstrap>
    </StrictMode>
  );
}

import { LOCAL_REFRESH } from '@/config';
import Footer from '@/modules/Layout/Footer';
import Header from '@/modules/Layout/Header';
import Main from '@/modules/Layout/Main';
import { usersClient } from '@/modules/Users/api/usersClient';
import { useUserStore } from '@/store/userStore';
import { QueryClient } from '@tanstack/react-query';
import { createRootRouteWithContext } from '@tanstack/react-router';

export interface RouterContext {
  queryClient: QueryClient;
}

export const Route = createRootRouteWithContext<RouterContext>()({
  beforeLoad: async () => {
    const refreshToken = localStorage.getItem(LOCAL_REFRESH);

    if (!refreshToken) return;

    try {
      const user = await usersClient.refresh(refreshToken);

      console.log(user);

      if (!user?.refreshToken) {
        localStorage.removeItem(LOCAL_REFRESH);
        return;
      }

      localStorage.setItem(LOCAL_REFRESH, user.refreshToken);
      useUserStore.setState(() => ({ user: user }));
    } catch (err) {
      localStorage.removeItem(LOCAL_REFRESH);
      console.log(err);
    }
  },
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

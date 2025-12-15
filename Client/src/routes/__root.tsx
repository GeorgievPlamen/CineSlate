import { LOCAL_REFRESH } from '@/config';
import Footer from '@/modules/Layout/Footer';
import Header from '@/modules/Layout/Header';
import Main from '@/modules/Layout/Main';
import { usersClient } from '@/modules/Users/api/usersClient';
import { useUserStore } from '@/store/userStore';
import { createRootRoute } from '@tanstack/react-router';

export const Route = createRootRoute({
  beforeLoad: async () => {
    const refreshToken = localStorage.getItem(LOCAL_REFRESH);

    if (!refreshToken) return;

    const user = await usersClient.refresh(refreshToken);

    if (!user?.refreshToken) {
      localStorage.removeItem(LOCAL_REFRESH);
      return;
    }

    localStorage.setItem(LOCAL_REFRESH, user.refreshToken);
    useUserStore.setState(() => ({ user: user }));
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

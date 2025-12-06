import Background from '@/modules/Layout/Background';
import Footer from '@/modules/Layout/Footer';
import Header from '@/modules/Layout/Header';
import Main from '@/modules/Layout/Main';
import { createRootRoute } from '@tanstack/react-router';

export const Route = createRootRoute({
  component: RootComponent,
});

function RootComponent() {
  return (
    <div className="flex min-h-screen flex-col">
      <Header />
      <Main />
      <Footer />
      <Background />
    </div>
  );
}

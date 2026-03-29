import { Outlet } from '@tanstack/react-router';

function Main() {
  return (
    <main className="grow bg-background z-0">
      <Outlet />
    </main>
  );
}
export default Main;

import { Outlet } from '@tanstack/react-router';

function Main() {
  return (
    <main className="mt-[69px] grow">
      <Outlet />
    </main>
  );
}
export default Main;

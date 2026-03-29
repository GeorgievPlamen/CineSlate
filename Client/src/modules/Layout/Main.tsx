import { Outlet } from '@tanstack/react-router';

function Main() {
  return (
    <main className="grow overflow-y-auto scrollbar overflow-x-hidden">
      <Outlet />
    </main>
  );
}
export default Main;

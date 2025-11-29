import { Outlet } from 'react-router-dom';

function Main() {
  return (
    <main className="mt-[69px] grow">
      <Outlet />
    </main>
  );
}
export default Main;

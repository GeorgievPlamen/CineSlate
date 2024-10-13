import { Outlet } from 'react-router-dom';

function Main() {
  return (
    <main className="my-4 flex flex-grow flex-col items-center gap-4 md:mx-auto md:w-2/3">
      <Outlet />
    </main>
  );
}
export default Main;

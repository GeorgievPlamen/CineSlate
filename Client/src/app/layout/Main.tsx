import { Outlet } from 'react-router-dom';

function Main() {
  return (
    <main className="my-4 mt-20 flex-grow">
      <Outlet />
    </main>
  );
}
export default Main;

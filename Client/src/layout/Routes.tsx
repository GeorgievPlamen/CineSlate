import { createBrowserRouter, Link } from 'react-router-dom';
import Hello from '../features/Hello';
import Login, { loginAction } from '../features/Users/Login';
import Layout from './Layout';

const router = createBrowserRouter([
  {
    element: <Layout />,
    errorElement: (
      <>
        <h2>Oops... :(</h2>
        <Link to="/">Reload</Link>
      </>
    ),
    children: [
      {
        path: '/',
        index: true,
        element: <Hello />,
      },
      {
        path: '/login',
        element: <Login />,
        action: loginAction,
      },
    ],
  },
]);

export default router;

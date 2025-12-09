import { createBrowserRouter, Link, RouterProvider } from 'react-router-dom';
import { registerAction } from './features/Users/api/registerAction';
import Register from './features/Users/Register';
import Layout from './layout/Layout';

export default function App() {
  return (
    <RouterProvider
      router={createBrowserRouter([
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
              path: '/register',
              element: <Register />,
              action: registerAction,
            },
          ],
        },
      ])}
    />
  );
}

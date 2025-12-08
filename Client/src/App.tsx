import { createBrowserRouter, Link, RouterProvider } from 'react-router-dom';
import Critics from './features/Critics/Critics';
import { loginAction } from './features/Users/api/loginAction';
import { registerAction } from './features/Users/api/registerAction';
import Login from './features/Users/Login';
import Register from './features/Users/Register';
import Layout from './layout/Layout';
import CriticDetails from './features/CriticDetails/CriticDetails';
import MyDetails from './features/MyDetails/MyDetails';
import ReviewDetails from './features/Reviews/Details/ReviewDetails';

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
              path: '/critics',
              element: <Critics />,
            },
            {
              path: '/critics/:id',
              element: <CriticDetails />,
            },
            {
              path: '/login',
              element: <Login />,
              action: loginAction,
            },
            {
              path: '/register',
              element: <Register />,
              action: registerAction,
            },
            {
              path: '/my-details',
              element: <MyDetails />,
            },
            {
              path: '/reviews/:id',
              element: <ReviewDetails />,
            },
          ],
        },
      ])}
    />
  );
}

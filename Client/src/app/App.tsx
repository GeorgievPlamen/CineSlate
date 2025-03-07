import {
  createBrowserRouter,
  Link,
  Navigate,
  RouterProvider,
} from 'react-router-dom';
import Critics from '../pages/Critics/Critics';
import Home from '../pages/Home';
import Movies from '../pages/Movies/Movies';
import { loginAction } from '../pages/Users/api/loginAction';
import { registerAction } from '../pages/Users/api/registerAction';
import Login from '../pages/Users/Login';
import Register from '../pages/Users/Register';
import Layout from './layout/Layout';
import MovieDetails from '../pages/Movies/Details/MovieDetails';
import CriticDetails from '../pages/CriticDetails/CriticDetails';
import MyDetails from '../pages/MyDetails/MyDetails';
import ReviewDetails from '../pages/Reviews/Details/ReviewDetails';

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
              path: '*',
              element: <Navigate to={'/'} />,
            },
            {
              path: '/',
              index: true,
              element: <Home />,
            },
            {
              path: '/movies',
              element: <Movies />,
            },
            {
              path: '/movies/:id',
              element: <MovieDetails />,
            },
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

import { createBrowserRouter, Link, RouterProvider } from 'react-router-dom';
import Critics from '../pages/Critics/Critics';
import Home from '../pages/Home';
import Movies from '../pages/Movies/Movies';
import Quizzess from '../pages/Quizzes/Quizzess';
import { loginAction } from '../pages/Users/api/loginAction';
import { registerAction } from '../pages/Users/api/registerAction';
import Login from '../pages/Users/Login';
import Register from '../pages/Users/Register';
import Layout from './layout/Layout';
import MovieDetails from '../pages/Movies/Details/MovieDetails';

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
              index: true,
              element: <Home />,
            },
            {
              path: '/',
              index: true,
              element: <Home />,
            },
            {
              path: '/movies',
              index: true,
              element: <Movies />,
            },
            {
              path: '/movies/:id',
              element: <MovieDetails />,
            },
            {
              path: '/critics',
              index: true,
              element: <Critics />,
            },
            {
              path: '/quizzes',
              index: true,
              element: <Quizzess />,
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
          ],
        },
      ])}
    />
  );
}

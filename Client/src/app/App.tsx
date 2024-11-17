import { createBrowserRouter, Link, RouterProvider } from 'react-router-dom';
import Critics from '../features/Critics/Critics';
import Home from '../features/Home';
import Movies from '../features/Movies/Movies';
import Quizzess from '../features/Quizzes/Quizzess';
import Stories from '../features/Stories/Stories';
import { loginAction } from '../features/Users/api/loginAction';
import { registerAction } from '../features/Users/api/registerAction';
import Login from '../features/Users/Login';
import Register from '../features/Users/Register';
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
              path: '/critics',
              index: true,
              element: <Critics />,
            },
            {
              path: '/stories',
              index: true,
              element: <Stories />,
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

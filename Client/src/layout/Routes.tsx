import { createBrowserRouter, Link } from 'react-router-dom';
import Home from '../features/Home';
import Login from '../features/Users/Login';
import Layout from './Layout';
import Movies from '../features/Movies/Movies';
import Critics from '../features/Critics/Critics';
import Stories from '../features/Stories/Stories';
import Quizzess from '../features/Quizzes/Quizzess';
import { loginAction } from '../features/Users/loginAction';
import Register from '../features/Users/Register';

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
      },
    ],
  },
]);

export default router;

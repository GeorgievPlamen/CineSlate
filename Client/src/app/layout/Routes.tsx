import { createBrowserRouter, Link } from 'react-router-dom';
import Layout from './Layout';
import Critics from '../../features/Critics/Critics';
import Home from '../../features/Home';
import Movies from '../../features/Movies/Movies';
import Quizzess from '../../features/Quizzes/Quizzess';
import Stories from '../../features/Stories/Stories';
import Login from '../../features/Users/Login';
import { loginAction } from '../../features/Users/loginAction';
import Register from '../../features/Users/Register';
import { registerAction } from '../../features/Users/registerAction';

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
        action: registerAction,
      },
    ],
  },
]);

export default router;

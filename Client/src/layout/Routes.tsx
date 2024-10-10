import { createBrowserRouter } from "react-router-dom";
import Hello from "../pages/Hello";
import Login, { loginAction } from "../pages/Users/Login";
import Layout from "./Layout";

const router = createBrowserRouter([
  {
    element: <Layout />,
    children: [
      {
        path: "/",
        index: true,
        element: <Hello />,
      },
      {
        path: "/login",
        element: <Login />,
        action: loginAction,
      },
    ],
  },
]);

export default router;

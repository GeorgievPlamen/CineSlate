import { createBrowserRouter } from "react-router-dom";
import Hello from "../pages/Hello";
import Login from "../pages/Users/Login";
import Layout from "./Layout";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    children: [
      {
        path: "/",
        element: <Hello />,
      },
      {
        path: "/login",
        element: <Login />,
      },
    ],
  },
]);

export default router;

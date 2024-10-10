import axios from "axios";
import { ActionFunctionArgs, Form } from "react-router-dom";

export async function loginAction({ request }: ActionFunctionArgs) {
  let post;
  try {
    post = await axios.post(
      "http://localhost:8080/api/users/login",
      await request.formData(),
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
  } catch (error) {
    console.log(error);
  }

  console.log(post);

  return { post };
}

function Login() {
  return (
    <Form
      method="post"
      className="flex flex-col gap-3 border border-white w-72  m-auto rounded  items-center"
    >
      <div className="flex flex-col mb-2 first:mt-2 w-60">
        <label htmlFor="email" className="mb-1">
          Email
        </label>
        <input type="email" name="email" className=" rounded text-black" />
      </div>
      <div className="flex flex-col mb-2 w-60">
        <label htmlFor="password" className="mb-1">
          Password
        </label>
        <input type="password" name="password" className="rounded text-black" />
      </div>
      <button
        type="submit"
        className="bg-white text-black rounded w-60 mt-3 mb-3"
      >
        Sign in
      </button>
    </Form>
  );
}
export default Login;

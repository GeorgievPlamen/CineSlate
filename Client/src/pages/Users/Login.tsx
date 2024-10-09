import { Form } from "react-router-dom";

function Login() {
  return (
    <Form className="flex flex-col gap-3 border border-white w-72  m-auto rounded  items-center">
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

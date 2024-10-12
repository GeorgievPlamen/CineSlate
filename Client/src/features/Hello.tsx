import { movies } from "../api/api";

function Hello() {
  return (
    <>
      <div className="text-3xl text-center">Hello there 👋</div>
      <button onClick={() => movies()}>Movies</button>
    </>
  );
}

export default Hello;

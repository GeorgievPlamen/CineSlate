import { movies } from '../../api/api';

function Movies() {
  return <button onClick={() => movies()}>Get Movies</button>;
}
export default Movies;

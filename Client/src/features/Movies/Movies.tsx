import { movies } from '../../app/api/api';
import { useGetHealthQuery } from '../../app/api/cineslateApi';

function Movies() {
  const { data } = useGetHealthQuery();

  console.log(data);
  return <button onClick={() => movies()}>Get Movies</button>;
}
export default Movies;

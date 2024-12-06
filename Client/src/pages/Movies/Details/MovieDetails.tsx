import { useParams } from 'react-router-dom';

export default function MovieDetails() {
  const params = useParams();
  return <div>MovieId {params.id}</div>;
}

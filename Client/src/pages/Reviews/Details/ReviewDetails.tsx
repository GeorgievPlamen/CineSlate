import { useParams } from 'react-router-dom';

function ReviewDetails() {
  const { id } = useParams();
  return <div>{id}</div>;
}

export default ReviewDetails;

import { useGetReviewsByAuthorIdQuery } from '../CriticDetails/api/criticDetailsApi';
import { useUser } from '../Users/userSlice';

function MyDetails() {
  const user = useUser();
  const { data: reviewData } = useGetReviewsByAuthorIdQuery({
    id: user.id ?? '',
    page: 1,
  });
  console.log(reviewData?.values);

  return <div>MyDetails</div>;
}
export default MyDetails;

import { useGetReviewsByAuthorIdQuery } from '../CriticDetails/api/criticDetailsApi';
import { useUser } from '../Users/userSlice';
import { useUpdateUserMutation } from './api/myDetailsApi';

function MyDetails() {
  const user = useUser();
  const [updateUser] = useUpdateUserMutation();
  const { data: reviewData } = useGetReviewsByAuthorIdQuery({
    id: user.id ?? '',
    page: 1,
  });

  console.log(reviewData?.values);

  return (
    <>
      <div>MyDetails</div>
    </>
  );
}

export default MyDetails;

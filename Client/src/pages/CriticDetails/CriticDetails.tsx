import { useParams } from 'react-router-dom';
import ReviewCard from '../../app/components/Cards/ReviewCard';
import Button from '../../app/components/Buttons/Button';
import { useState } from 'react';
import { useGetReviewsByAuthorIdQuery } from './api/criticDetailsApi';

function CriticDetails() {
  const { user } = useParams();
  const userData = user?.split('.') ?? '';

  const [reviewsPage, setReviewsPage] = useState(1);
  const { data: reviewData, isFetching: isReviewsFetching } =
    useGetReviewsByAuthorIdQuery({
      id: userData[2],
      page: reviewsPage,
    });

  console.log(reviewData);

  // TODO get movies for reviews, style the page to have the movie and the review on a card

  return (
    <>
      <p>{userData[0]}</p>
      <p>{}</p>
      <section className="my-10 flex flex-col gap-10">
        {reviewData?.values.map((r) => (
          <ReviewCard key={r.authorId} review={r} />
        ))}
        {reviewData?.hasNextPage && (
          <Button
            onClick={() => setReviewsPage((prev) => prev + 1)}
            className="w-fit px-10"
            isLoading={isReviewsFetching}
          >
            Load More
          </Button>
        )}
      </section>
    </>
  );
}
export default CriticDetails;

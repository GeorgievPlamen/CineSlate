import { useParams } from 'react-router-dom';
import Button from '../../app/components/Buttons/Button';
import { useEffect, useState } from 'react';
import { useGetReviewsByAuthorIdQuery } from './api/criticDetailsApi';
import { BACKUP_PROFILE } from '../../app/config';
import MovieReviewCard from '../../app/components/Cards/MovieReviewCard';
import { Critic, useCriticById, useSetCritics } from '../Critics/criticsSlice';
import { useLazyGetUsersByIdQuery } from '../Users/api/userApiRTK';

function CriticDetails() {
  const { id } = useParams();
  const [getUsersByIds] = useLazyGetUsersByIdQuery();

  const [reviewsPage, setReviewsPage] = useState(1);
  const { data: reviewData, isFetching: isReviewsFetching } =
    useGetReviewsByAuthorIdQuery({
      id: id ?? '',
      page: reviewsPage,
    });

  const critic = useCriticById(id);

  const { dispatchCritics } = useSetCritics();

  useEffect(() => {
    async function GetUsers() {
      const users = await getUsersByIds({ ids: [id ?? ''] });
      if (users.data) dispatchCritics([...users.data] as Critic[]);
    }

    if (!critic && id) {
      GetUsers();
    }
  }, [critic, dispatchCritics, getUsersByIds, id]);

  return (
    <article className="m-auto flex w-2/3 flex-col">
      <section className="mt-5">
        <div className="flex items-center justify-between gap-2">
          <div className="flex w-1/4 gap-2">
            <img src={BACKUP_PROFILE} alt="profile-pic" className="h-32 w-32" />
            <div className="flex flex-col">
              <h2 className="mt-5 font-arvo text-xl">
                {critic?.username.split('#')[0]}
              </h2>
              <p className="font-roboto text-sm text-grey">{critic?.bio}</p>
            </div>
          </div>
          <div className="p-2">
            <p className="text-center font-arvo text-lg">
              {reviewData?.totalCount}
            </p>
            <p className="text-xs font-light text-grey">Reviews</p>
          </div>
        </div>
      </section>
      <section className="m-auto w-2/3">
        <h3 className="my-4 ml-2 font-arvo text-lg">Recent Reviews</h3>
        <div className="mb-20 flex flex-col gap-6">
          {reviewData?.values.map((r) => (
            <MovieReviewCard key={r.movieId} review={r} />
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
        </div>
      </section>
    </article>
  );
}
export default CriticDetails;

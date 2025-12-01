import { useParams } from 'react-router-dom';
import Button from '../../components/Buttons/Button';
import { useState } from 'react';
import { useGetReviewsByAuthorIdQuery } from './api/criticDetailsApi';
import { BACKUP_PROFILE } from '../../config';
import MovieReviewCard from '../../components/Cards/MovieReviewCard';
import { useQuery } from '@tanstack/react-query';
import { usersApi } from '../Users/api/usersApi';
import appContants from '@/common/appConstants';

function CriticDetails() {
  const { id } = useParams();
  const [reviewsPage, setReviewsPage] = useState(1);
  const { data: reviewData, isFetching: isReviewsFetching } =
    useGetReviewsByAuthorIdQuery({
      id: id ?? '',
      page: reviewsPage,
    });

  const { data } = useQuery({
      queryKey: ['getUsersByIds', id],
      queryFn: () => usersApi.postGetUsersByIdQuery([id ?? '']),
      enabled: !!id,
      staleTime: appContants.STALE_TIME
    })

  const critic = data?.[0];
  // TODO: render not found critic 

  return (
    <article className="m-auto flex w-2/3 flex-col">
      <section className="mt-5">
        <div className="flex items-center justify-between gap-2">
          <div className="flex w-1/4 gap-2">
            <img
              src={
                critic?.pictureBase64?.length &&
                critic?.pictureBase64?.length > 0
                  ? critic.pictureBase64
                  : BACKUP_PROFILE
              }
              alt="profile-pic"
              className="h-32 w-32 rounded-full object-cover"
            />
            <div className="flex flex-col">
              <h2 className="font-arvo mt-5 text-xl">
                {critic?.username.split('#')[0]}
              </h2>
              <p className="font-roboto text-grey text-sm">{critic?.bio}</p>
            </div>
          </div>
          <div className="p-2">
            <p className="font-arvo text-center text-lg">
              {reviewData?.totalCount}
            </p>
            <p className="text-grey text-xs font-light">Reviews</p>
          </div>
        </div>
      </section>
      <section className="m-auto w-2/3">
        <h3 className="font-arvo my-4 ml-2 text-lg">Recent Reviews</h3>
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

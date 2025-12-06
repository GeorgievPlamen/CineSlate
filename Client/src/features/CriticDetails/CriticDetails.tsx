import { useParams } from 'react-router-dom';
import Button from '@/components/Buttons/Button';
import { BACKUP_PROFILE } from '@/config';
import MovieReviewCard from '@/components/Cards/MovieReviewCard';
import { useInfiniteQuery, useQuery } from '@tanstack/react-query';
import { usersClient } from '../Users/api/usersClient';
import appContants from '@/common/appConstants';
import { reviewsClient } from '../Reviews/api/reviewsClient';
import ToPagedData from '@/utils/toPagedData';

function CriticDetails() {
  const { id } = useParams();

  const { data } = useQuery({
    queryKey: ['getUsersByIds', id],
    queryFn: () => usersClient.getUsersByIds([id ?? '']),
    enabled: !!id,
    staleTime: appContants.STALE_TIME,
  });

  const {
    data: reviewsData,
    fetchNextPage,
    isFetching,
  } = useInfiniteQuery({
    queryKey: ['reviewByAuthorId', id],
    queryFn: ({ pageParam }) =>
      reviewsClient.getReviewsByAuthorId(id ?? '', pageParam),
    initialPageParam: 1,
    getNextPageParam: (lastPage) => lastPage.currentPage + 1,
    select: (data) => ToPagedData(data),
    enabled: !!id,
  });

  const critic = data?.[0];

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
              {reviewsData?.totalCount}
            </p>
            <p className="text-grey text-xs font-light">Reviews</p>
          </div>
        </div>
      </section>
      <section className="m-auto w-2/3">
        <h3 className="font-arvo my-4 ml-2 text-lg">Recent Reviews</h3>
        <div className="mb-20 flex flex-col gap-6">
          {reviewsData?.values.map((r) => (
            <MovieReviewCard key={r.movieId} review={r} />
          ))}
          {reviewsData?.hasNextPage && (
            <Button
              onClick={fetchNextPage}
              className="w-fit px-10"
              isLoading={isFetching}
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

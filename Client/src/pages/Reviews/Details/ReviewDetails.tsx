import { NavLink, useParams } from 'react-router-dom';
import { useReviewDetailsByIdQuery } from '../api/reviewsApi';
import Loading from '../../../app/components/Loading/Loading';
import { useMovieDetailsQuery } from '../../Movies/api/moviesApi';
import { BACKUP_PROFILE, IMG_PATH } from '../../../app/config';
import { useState } from 'react';
import CommentCard from '../../../app/components/Cards/CommentCard';
import { useGetUsersByIdQuery } from '../../Users/api/userApiRTK';
import LikesButton from '../../../app/components/Buttons/LikesButton';
import Backdrop from '../../../app/components/Backdrop/Backdrop';
import AddComment from './AddComment';

function ReviewDetails() {
  const { id } = useParams();
  const [imageIsLoading, setImageIsLoading] = useState(true);

  const {
    data: reviewData,
    isLoading: isReviewLoading,
    refetch,
  } = useReviewDetailsByIdQuery({
    reviewId: id ?? '',
  });

  const { data: movieData, isLoading: isMovieLoading } = useMovieDetailsQuery(
    {
      id: `${reviewData?.movieId}`,
    },
    { skip: reviewData?.movieId === undefined }
  );

  const { data: usersData } = useGetUsersByIdQuery({
    ids: [reviewData?.authorId ?? ''],
  });

  const user = usersData?.[0];

  if (isReviewLoading || isMovieLoading) return <Loading />;

  return (
    <article className="mx-auto my-10 flex w-full flex-col items-center justify-center">
      <Backdrop path={movieData?.backdropPath} />
      <div className="flex flex-col sm:flex-row">
        <section>
          <img
            className={
              'mx-auto mb-4 w-52 rounded-lg border border-grey' +
              ` ${imageIsLoading ? 'hidden' : ''}`
            }
            src={IMG_PATH + movieData?.posterPath}
            alt="poster"
            onLoad={() => setImageIsLoading(false)}
          />
          <div className="mx-auto flex w-fit flex-col items-center gap-4 sm:mx-4">
            <NavLink
              className={'hover:text-primary'}
              to={`/movies/${reviewData?.movieId}`}
            >
              <h2 className="mb-2 w-full max-w-52 text-wrap text-center font-arvo text-xl font-bold">
                {movieData?.title}
              </h2>
            </NavLink>
            <div className="flex items-center justify-center gap-2">
              <p className="text-xs font-light">
                {movieData?.releaseDate.toString()}
              </p>
              <p className="text-md font-bold">⭐{movieData?.rating}</p>
            </div>
          </div>
        </section>
        <section className="ml-10 w-2/3">
          <div className="min-h-20 min-w-32">
            <div className="flex gap-4">
              <h3 className="my-4 font-arvo text-lg">By: </h3>
              <div className="flex gap-2">
                <img
                  src={
                    user?.pictureBase64?.length &&
                    user?.pictureBase64?.length > 0
                      ? user.pictureBase64
                      : BACKUP_PROFILE
                  }
                  alt="profile-pic"
                  className="h-16 w-16 rounded-full object-cover"
                />
                <NavLink
                  className={'hover:text-primary'}
                  to={`/critics/${reviewData?.authorId}`}
                >
                  <h2 className="mt-5 min-w-44 font-arvo text-xl">
                    {user?.username.split('#')[0]}
                  </h2>
                </NavLink>
              </div>
            </div>
          </div>
          <h4 className="mb-4 font-arvo text-lg">⭐{reviewData?.rating}</h4>
          <div className="flex gap-2 rounded-2xl border bg-background p-2">
            <p className="font-roboto">
              {reviewData?.text && reviewData?.text.length > 0
                ? reviewData?.text
                : 'Did not share...'}
            </p>
          </div>
          <div className="m-4">
            <LikesButton reviewId={id ?? ''} />
          </div>
        </section>
      </div>
      <section className="w-1/3">
        {reviewData?.hasUserCommented ? (
          <>{/* <SubmitButton className="mb-2" text={'Remove Comment'} /> */}</>
        ) : (
          <AddComment reviewId={id ?? ''} refetchComments={refetch} />
        )}
        {Object.entries(reviewData?.comments ?? '').map(([id, comment]) => (
          <CommentCard key={id} comment={comment} />
        ))}
      </section>
    </article>
  );
}

export default ReviewDetails;

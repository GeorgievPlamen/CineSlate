import { NavLink, useParams } from 'react-router-dom';
import { useMovieDetailsQuery } from '../api/moviesApi';
import { IMG_PATH } from '../../../app/config';
import Backdrop from '../../../app/components/Backdrop/Backdrop';
import { useEffect, useState } from 'react';
import Loading from '../../../app/components/Loading/Loading';
import ErrorMessage from '../../../app/components/ErrorMessage/ErrorMessage';
import ReviewCard from '../../../app/components/Cards/ReviewCard';
import GenreButton from '../../../app/components/Buttons/GenreButton';
import AddReview from './AddReview';
import useAuth from '../../../app/hooks/useAuth';
import { useReviewsByMovieIdQuery } from '../../Reviews/api/reviewsApi';
import Button from '../../../app/components/Buttons/Button';
import { Review } from '../../Reviews/models/review';
import { useLazyGetUsersByIdQuery } from '../../Users/api/userApiRTK';

export default function MovieDetails() {
  const { id } = useParams();
  const [reviews, setReviews] = useState<Review[]>([]);
  const [reviewsPage, setReviewsPage] = useState(1);
  const [imageIsLoading, setImageIsLoading] = useState(true);
  const [getUsersByIds, { data: usersData }] = useLazyGetUsersByIdQuery();

  const {
    data,
    isError,
    refetch: refetchMovieDetails,
  } = useMovieDetailsQuery({ id });

  const {
    data: reviewData,
    isFetching: isReviewsFetching,
    refetch: refetchReviews,
  } = useReviewsByMovieIdQuery({
    movieId: Number(id),
    page: reviewsPage,
  });

  const { isAuthenticated } = useAuth();

  useEffect(() => {
    if (isAuthenticated) refetchReviews();
  }, [refetchReviews, isAuthenticated]);

  useEffect(() => {
    if (!reviewData) return;

    const authorIds = [
      ...reviewData.values
        .filter((x) => x.authorId !== undefined)
        .map((x) => x.authorId),
    ];

    getUsersByIds({ ids: authorIds as string[] });

    setReviews((prev) =>
      prev.length < reviewData.currentPage * 20
        ? [...reviewData.values]
        : [...prev, ...reviewData.values]
    );
  }, [getUsersByIds, reviewData]);

  if (isError) return <ErrorMessage />;

  return (
    <>
      <Backdrop path={data?.backdropPath} />
      {imageIsLoading && <Loading />}
      <article className="mt-20">
        <article className="mx-auto flex w-full flex-col items-center justify-center">
          <div className="flex">
            <div className="flex flex-col items-center justify-center">
              <img
                className={
                  'mb-4 w-80 rounded-lg border border-grey' +
                  ` ${imageIsLoading ? 'hidden' : ''}`
                }
                src={IMG_PATH + data?.posterPath}
                alt="poster"
                onLoad={() => setImageIsLoading(false)}
              />
              {isAuthenticated ? (
                <AddReview
                  onSuccess={() => {
                    refetchMovieDetails();
                    refetchReviews();
                  }}
                />
              ) : (
                <p>
                  <NavLink to="../login" className="underline">
                    Sign in
                  </NavLink>{' '}
                  to review
                </p>
              )}
            </div>
            <section className="mx-10 my-5 w-1/2 max-w-[700px]">
              <div className="flex w-fit items-center gap-4">
                <h2 className="mb-2 font-arvo text-3xl font-bold">
                  {data?.title}
                </h2>
                <p className="text-sm font-light">
                  {data?.releaseDate.toString()}
                </p>
                <p className="text-xl font-bold">
                  ⭐
                  {data?.rating === 0
                    ? 'Be the first to review!'
                    : data?.rating}
                </p>
              </div>
              <p className="font-roboto">{data?.description}</p>
              <p className="my-4 font-serif text-xl italic">{data?.tagline}</p>
              <section className="mt-4 h-full flex-row gap-2">
                {data?.genres.map((g) => (
                  <GenreButton key={g.id} name={g.value} />
                ))}
              </section>
            </section>
          </div>
          <section className="my-10 flex flex-col gap-10">
            {reviews.map((r) => (
              <ReviewCard
                key={r.authorId}
                review={r}
                authorPicture={
                  usersData?.find((x) => x.id === r.authorId)?.pictureBase64
                }
              />
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
        </article>
      </article>
    </>
  );
}

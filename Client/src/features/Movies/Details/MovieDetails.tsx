import { NavLink, useParams } from 'react-router-dom';

import { useQuery } from '@tanstack/react-query';
import { moviesClient } from '../api/moviesClient';
import { usersClient } from '@/features/Users/api/usersClient';
import { reviewsClient } from '@/features/Reviews/api/reviewsClient';
import Backdrop from '@/components/Backdrop/Backdrop';
import Button from '@/components/Buttons/Button';
import GenreButtonOld from '@/components/Buttons/GenreButtonOld';
import ReviewCard from '@/components/Cards/ReviewCard';
import ErrorMessage from '@/components/ErrorMessage/ErrorMessage';
import Loading from '@/components/Loading/Loading';
import { IMG_PATH } from '@/config';
import { useState, useEffect } from 'react';
import AddReview from './AddReview';
import { Review } from '@/features/Reviews/models/review';
import useAuth from '@/hooks/useAuth';

export default function MovieDetails() {
  const { id } = useParams();
  const [reviews, setReviews] = useState<Review[]>([]);
  const [reviewsPage, setReviewsPage] = useState(1);
  const [imageLoaded, setImageLoaded] = useState(false);
  const [authorIds, setAuthorIds] = useState<string[]>([]);

  const { data: usersData } = useQuery({
    queryKey: ['getUsersByIds', authorIds],
    queryFn: () => usersClient.getUsersByIds([...authorIds]),
    enabled: authorIds.length > 0,
  });

  const { isAuthenticated } = useAuth();

  const {
    data,
    isError,
    isLoading,
    refetch: refetchMovieDetails,
  } = useQuery({
    queryKey: ['movieDetails', id],
    queryFn: () => moviesClient.getMovieDetails(`${id}`),
  });

  const {
    data: reviewData,
    isFetching: isReviewsFetching,
    refetch: refetchReviews,
  } = useQuery({
    queryKey: ['', id, reviewsPage],
    queryFn: () => reviewsClient.reviewsByMovieId(id ?? '', reviewsPage),
  });

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

    setAuthorIds(authorIds as string[]);

    setReviews((prev) =>
      prev.length < reviewData.currentPage * 20
        ? [...reviewData.values]
        : [...prev, ...reviewData.values]
    );
  }, [reviewData]);

  if (isError) return <ErrorMessage />;

  if (isLoading) return <Loading />;

  return (
    <>
      <Backdrop path={data?.backdropPath} />
      <article className="mt-20">
        <article className="mx-auto flex w-full flex-col items-center justify-center">
          <div className="flex flex-col sm:flex-row">
            <div className="flex flex-col items-center justify-center">
              <img
                className={
                  'border-grey mb-4 w-80 rounded-lg border transition-opacity duration-200 ' +
                  (imageLoaded ? 'opacity-100' : 'opacity-0')
                }
                src={IMG_PATH + data?.posterPath}
                alt="poster"
                onLoad={() => setImageLoaded(true)}
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
            <section className="mx-auto my-5 w-1/2 max-w-[700px]">
              <div className="flex w-fit items-center gap-4">
                <h2 className="font-heading mb-2 text-3xl font-bold">
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
              <p className="font-primary">{data?.description}</p>
              <p className="my-4 font-serif text-xl italic">{data?.tagline}</p>
              <section className="mt-4 h-full flex-row gap-2">
                {data?.genres.map((g) => (
                  <GenreButtonOld key={g.id} name={g.value} genreId={g.id} />
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

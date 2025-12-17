import { useMutation, useQuery } from '@tanstack/react-query';
import Backdrop from '@/components/Backdrop/Backdrop';
import Button from '@/components/Buttons/Button';
import ErrorMessage from '@/components/ErrorMessage/ErrorMessage';
import Loading from '@/components/Loading/Loading';
import { IMG_PATH } from '@/config';
import { useState, useEffect } from 'react';
import useAuth from '@/hooks/useAuth';
import AddReview from './AddReview';
import { getRouteApi, Link, useNavigate } from '@tanstack/react-router';
import GenreButton from '@/components/Buttons/GenreButton';
import ReviewCard from '@/components/Cards/ReviewCard';
import { reviewsClient } from '@/modules/Review/api/reviewsClient';
import { Review } from '@/modules/Review/models/review';
import { usersClient } from '@/modules/Users/api/usersClient';
import { moviesClient } from '../api/moviesClient';
import { watchlistsClient } from '@/modules/Watchlist/api/watchlistClient';

const { useParams } = getRouteApi('/movies/$id');

export default function MovieDetails() {
  const { id } = useParams();
  const [reviews, setReviews] = useState<Review[]>([]);
  const [reviewsPage, setReviewsPage] = useState(1);
  const [imageLoaded, setImageLoaded] = useState(false);
  const [authorIds, setAuthorIds] = useState<string[]>([]);
  const navigate = useNavigate();

  const { data: usersData } = useQuery({
    queryKey: ['getUsersByIds', authorIds],
    queryFn: () => usersClient.getUsersByIds([...authorIds]),
    enabled: authorIds.length > 0,
  });

  const { mutateAsync: addToWatchlistAsync } = useMutation({
    mutationFn: (id: number) => watchlistsClient.addToWatchlist(id),
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
    queryKey: ['reviewsByMovieId', id, reviewsPage],
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

  async function handleAddToWatchlist(id: number) {
    if (!isAuthenticated) {
      navigate({ to: '/watchlist' });
      return;
    }

    await addToWatchlistAsync(id);
  }

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
                  onSuccess={async () => {
                    await refetchMovieDetails();
                    await refetchReviews();
                  }}
                />
              ) : (
                <p>
                  <Link to="/login" className="underline">
                    Sign in
                  </Link>{' '}
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
                  <GenreButton key={g.id} name={g.value} genreId={g.id} />
                ))}
                <div className="w-full flex justify-start">
                  <Button
                    onClick={async () => await handleAddToWatchlist(Number(id))}
                    className="px-2 mt-8"
                  >
                    Add to watchlist
                  </Button>
                </div>
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

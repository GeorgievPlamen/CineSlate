import { useState } from 'react';
import Button from '../Buttons/Button';
import { BACKUP_PROFILE, IMG_PATH_W500 } from '@/config';
import { Link } from '@tanstack/react-router';
import { Review } from '@/modules/Review/models/review';
import { moviesClient } from '@/modules/Movies/api/moviesClient';
import { useQuery } from '@tanstack/react-query';
import { usersClient } from '@/modules/Users/api/usersClient';
import appContants from '@/common/appConstants';
import { base64ToImage } from '@/lib/utils';

interface Props {
  review: Review;
}

export default function MovieReviewCard({ review }: Props) {
  const [revealed, setRevealed] = useState(!false);

  const { data: movie } = useQuery({
    queryKey: ['movie-details-review-card', review.movieId],
    queryFn: () => moviesClient.getMovieDetails(`${review.movieId}`),
    staleTime: appContants.STALE_TIME,
  });

  const { data: critics } = useQuery({
    queryKey: ['getUsersByIds', review.authorId],
    queryFn: () => usersClient.getUsersByIds([review.authorId ?? '']),
    staleTime: appContants.STALE_TIME,
  });

  const critic = critics?.[0];

  return (
    <div className="flex rounded-2xl border border-grey bg-background w-120 h-full">
      <img
        src={IMG_PATH_W500 + movie?.posterPath}
        alt="poster"
        className="w-28 rounded-l-2xl border-r border-r-grey object-cover"
      />
      <div className="mx-4 my-2 w-full relative">
        <div className="mb-2 flex flex-col md:flex-row justify-between">
          <p className="text-lg">
            <Link
              to={'/movies/$id'}
              params={{ id: `${review.movieId}` }}
              className={'font-heading hover:text-primary'}
            >
              {movie?.title}
            </Link>
            <Link
              to={'/'}
              className="ml-2 text-sm text-muted-foreground hover:text-primary"
            >
              {movie?.releaseDate.toString()}
            </Link>
          </p>
          <div className="flex">
            <p>⭐{review.rating}</p>
          </div>
        </div>
        {review.containsSpoilers && !revealed && (
          <div className="flex items-center">
            <p className="font-primary">Contains spoilers:</p>
            <Button className="ml-2 p-4" onClick={() => setRevealed(true)}>
              Reveal
            </Button>
          </div>
        )}
        {revealed && (
          <p className="min-h-20 font-primary text-sm pb-11">
            {review.text && review.text.length > 0
              ? review.text
              : 'Did not share...'}
          </p>
        )}
        <div className="flex justify-self-end  gap-2 fixed bottom-0 pb-2">
          <h2 className="font-heading mt-5 text-md">
            <span className="text-xs text-muted-foreground">By: </span>
            {critic?.username.split('#')[0]}
          </h2>
          <img
            src={
              critic?.pictureBase64?.length && critic?.pictureBase64?.length > 0
                ? base64ToImage(critic.pictureBase64)
                : BACKUP_PROFILE
            }
            alt="profile-pic"
            className="h-16 w-16 rounded-full object-cover"
          />
        </div>
      </div>
    </div>
  );
}

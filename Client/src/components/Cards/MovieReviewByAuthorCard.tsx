import { useState } from 'react';
import Button from '../Buttons/Button';
import { IMG_PATH_W500 } from '@/config';
import LikesButton from '../Buttons/LikesButton';
import { Link } from '@tanstack/react-router';
import { ReviewWithMovieDetailsResponse } from '@/modules/Review/models/review';

interface Props {
  review: ReviewWithMovieDetailsResponse;
}

export default function MovieReviewByAuthorCard({ review }: Props) {
  const [revealed, setRevealed] = useState(
    !review.reviewResponse.containsSpoilers
  );

  return (
    <div className="flex rounded-2xl border border-grey bg-panel min-w-70">
      <img
        src={IMG_PATH_W500 + review.posterPath}
        alt="poster"
        className="w-28 rounded-l-2xl border-r border-r-grey object-cover"
      />
      <div className="mx-4 my-2 w-full">
        <div className="mb-2 flex justify-between flex-col">
          <p className="text-xl w-full flex justify-between">
            <Link
              to={'/movies/$id'}
              params={{ id: `${review.movieId}` }}
              className={'font-heading hover:text-primary text-semibold'}
            >
              {review.title}
            </Link>
            <p className="ml-2 text-lg text-muted-foreground">
              {review.releaseDate.split('-')[0]}
            </p>
          </p>
          <div className="flex gap-2 items-center">
            <Link
              to={'/reviews/$id'}
              params={{
                id: review.reviewResponse.id ?? '',
              }}
              className={'hover:text-primary text-sm underline text-muted-foreground'}
            >
              To Review
            </Link>
            <p>⭐{review.reviewResponse.rating}</p>
          </div>
        </div>
        {review.reviewResponse.containsSpoilers && !revealed && (
          <div className="flex items-center">
            <p className="font-primary">Contains spoilers:</p>
            <Button className="ml-2 p-4" onClick={() => setRevealed(true)}>
              Reveal
            </Button>
          </div>
        )}
        {revealed && (
          <p className="min-h-20 font-primary">
            {review.reviewResponse.text && review.reviewResponse.text.length > 0
              ? review.reviewResponse.text
              : 'Did not share...'}
          </p>
        )}
        <LikesButton reviewId={review?.reviewResponse?.id ?? ''} />
      </div>
    </div>
  );
}

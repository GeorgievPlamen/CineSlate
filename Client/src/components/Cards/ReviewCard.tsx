import { useState } from 'react';
import Button from '../Buttons/Button';
import { BACKUP_PROFILE } from '@/config';
import LikesButton from '../Buttons/LikesButton';
import { Link } from '@tanstack/react-router';
import { Review } from '@/modules/Review/models/review';
import { base64ToImage } from '@/lib/utils';

interface Props {
  review: Review;
  authorPicture?: string;
}

export default function ReviewCard({ review, authorPicture }: Props) {
  const [revealed, setRevealed] = useState(!review.containsSpoilers);

  const username = review.authorUsername?.split('#') ?? '';

  return (
    <div className="flex rounded-2xl border border-grey bg-background p-1">
      <img
        src={
          authorPicture && authorPicture.length > 0
            ? base64ToImage(authorPicture)
            : BACKUP_PROFILE
        }
        alt="profile-pic"
        className="h-20 w-20 rounded-full object-cover"
      />
      <div className="mx-4 my-2 w-80">
        <div className="mb-2 flex justify-between">
          <p className="text-xl">
            <Link
              className={'font-heading hover:text-primary'}
              to={'/critics/$id'}
              params={{ id: review?.authorId ?? '' }}
            >
              {username[0]}
            </Link>
            <span className="text-xs text-muted-foreground">
              {' '}
              #{username[1]}
            </span>
          </p>
          <div className="flex gap-2">
            <Link
              to="/reviews/$id"
              params={{ id: review?.id ?? '' }}
              className={'hover:text-primary'}
            >
              To Review
            </Link>
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
          <p className="min-h-10 font-primary">
            {review.text && review.text.length > 0
              ? review.text
              : 'Did not share...'}
          </p>
        )}
        <LikesButton reviewId={review.id ?? ''} />
      </div>
    </div>
  );
}

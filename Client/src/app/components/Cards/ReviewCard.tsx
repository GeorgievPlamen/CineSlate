import { useState } from 'react';
import { Review } from '../../../pages/Reviews/models/review';
import Button from '../Buttons/Button';
import { NavLink } from 'react-router-dom';
import { BACKUP_PROFILE } from '../../config';

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
            ? authorPicture
            : BACKUP_PROFILE
        }
        alt="profile-pic"
        className="h-20 w-20 rounded-full object-cover"
      />
      <div className="mx-4 my-2 w-80">
        <div className="mb-2 flex justify-between">
          <p className="text-xl">
            <NavLink
              className={'hover:text-primary'}
              to={`/critics/${review.authorId}`}
            >
              {username[0]}
            </NavLink>
            <span className="text-xs text-grey"> #{username[1]}</span>
          </p>
          <p>⭐{review.rating}</p>
        </div>
        {review.containsSpoilers && !revealed && (
          <div className="flex items-center">
            <p className="font-roboto">Contains spoilers:</p>
            <Button className="ml-2 p-4" onClick={() => setRevealed(true)}>
              Reveal
            </Button>
          </div>
        )}
        {revealed && (
          <p className="font-roboto">
            {review.text && review.text.length > 0
              ? review.text
              : 'Did not share...'}
          </p>
        )}
      </div>
    </div>
  );
}

import { useState } from 'react';
import Button from '../Buttons/Button';
import { NavLink } from 'react-router-dom';
import { IMG_PATH_W500 } from '../../config';
import { ReviewWithMovieDetailsResponse } from '../../../pages/CriticDetails/api/criticDetailsApi';

interface Props {
  review: ReviewWithMovieDetailsResponse;
}

export default function MovieReviewCard({ review }: Props) {
  const [revealed, setRevealed] = useState(
    !review.reviewResponse.containsSpoilers
  );

  return (
    <div className="flex rounded-2xl border border-grey bg-background">
      <img
        src={IMG_PATH_W500 + review.posterPath}
        alt="poster"
        className="w-20 rounded-l-2xl border-r border-r-grey"
      />
      <div className="mx-4 my-2 w-full">
        <div className="mb-2 flex justify-between">
          <p className="text-xl">
            <NavLink
              to={`/movies/${review.movieId}`}
              className={'hover:text-primary'}
            >
              {review.title}
            </NavLink>
            <NavLink
              to={''}
              className="ml-2 text-lg text-grey hover:text-primary"
            >
              {review.releaseDate.split('-')[0]}
            </NavLink>
          </p>
          <p>⭐{review.reviewResponse.rating}</p>
        </div>
        {review.reviewResponse.containsSpoilers && !revealed && (
          <div className="flex items-center">
            <p className="font-roboto">Contains spoilers:</p>
            <Button className="ml-2 p-4" onClick={() => setRevealed(true)}>
              Reveal
            </Button>
          </div>
        )}
        {revealed && (
          <p className="font-roboto">
            {review.reviewResponse.text && review.reviewResponse.text.length > 0
              ? review.reviewResponse.text
              : 'Did not share...'}
          </p>
        )}
      </div>
    </div>
  );
}

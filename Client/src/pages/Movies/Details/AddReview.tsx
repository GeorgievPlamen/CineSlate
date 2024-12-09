import { useState } from 'react';
import Star from '../../../app/assets/icons/Star';

export default function AddReview() {
  const [rating, setRating] = useState(0);
  const [subtitle, setSubtitle] = useState('');
  const [isHovering, setIsHovering] = useState(false);
  const [hoverRating, setHoverRating] = useState(0);
  const [hoverSubtitle, setHoverSubtitle] = useState('');

  function onRatingSelection(selectedRating: number, subtitle: string) {
    if (selectedRating === rating) {
      setRating(0);
      setSubtitle('');
    } else {
      setRating(selectedRating);
      setSubtitle(subtitle);
    }
  }

  function onHover(rating: number, subtitle: string) {
    setHoverRating(rating);
    setHoverSubtitle(subtitle);
  }

  // TODO text area - share your thoughts, spoilers checkbox, submit

  return (
    <form
      className="flex w-full flex-col items-center"
      onSubmit={(e) => e.preventDefault()}
    >
      <fieldset
        aria-label="Rate this product"
        className="mb-1 flex gap-2"
        onMouseEnter={() => setIsHovering(true)}
        onMouseLeave={() => setIsHovering(false)}
      >
        <button
          onClick={() => onRatingSelection(1, "— 'Terrible! Don't watch.'")}
          onMouseEnter={() => onHover(1, "— 'Terrible! Don't watch.'")}
        >
          {isHovering ? (
            <Star {...(hoverRating > 0 && { fill: 'yellow' })} />
          ) : (
            <Star {...(rating > 0 && { fill: 'yellow' })} />
          )}
        </button>
        <button
          onClick={() => onRatingSelection(2, "— 'Bad experience, avoid.'")}
          onMouseEnter={() => onHover(2, "— 'Bad experience, avoid.'")}
        >
          {isHovering ? (
            <Star {...(hoverRating > 1 && { fill: 'yellow' })} />
          ) : (
            <Star {...(rating > 1 && { fill: 'yellow' })} />
          )}
        </button>
        <button
          onClick={() => onRatingSelection(3, "— 'It was okay.'")}
          onMouseEnter={() => onHover(3, "— 'It was okay.'")}
        >
          {isHovering ? (
            <Star {...(hoverRating > 2 && { fill: 'yellow' })} />
          ) : (
            <Star {...(rating > 2 && { fill: 'yellow' })} />
          )}
        </button>
        <button
          onClick={() => onRatingSelection(4, "— 'Great movie, must watch.'")}
          onMouseEnter={() => onHover(4, "— 'Great movie, must watch.'")}
        >
          {isHovering ? (
            <Star {...(hoverRating > 3 && { fill: 'yellow' })} />
          ) : (
            <Star {...(rating > 3 && { fill: 'yellow' })} />
          )}
        </button>
        <button
          onClick={() => onRatingSelection(5, "— 'One of the best!'")}
          onMouseEnter={() => onHover(5, "— 'One of the best!'")}
        >
          {isHovering ? (
            <Star {...(hoverRating > 4 && { fill: 'yellow' })} />
          ) : (
            <Star {...(rating > 4 && { fill: 'yellow' })} />
          )}
        </button>
      </fieldset>
      <p className="font-serif italic">
        {isHovering ? hoverSubtitle : subtitle}
      </p>
    </form>
  );
}

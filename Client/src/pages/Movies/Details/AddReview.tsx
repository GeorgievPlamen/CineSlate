import { useState } from 'react';
import Star from '../../../app/assets/icons/Star';

export default function AddReview() {
  const [rating, setRating] = useState(0);
  const [isHovering, setIsHovering] = useState(false);
  const [hoverRating, setHoverRating] = useState(0);
  const [hoverSubtitle, setHoverSubtitle] = useState('');
  const [isTextAreaSelected, setIsTextAreaSelected] = useState(false);

  console.log(isTextAreaSelected);

  function onRatingSelection(selectedRating: number) {
    if (selectedRating === rating) {
      setRating(0);
    } else {
      setRating(selectedRating);
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
        className="mb-3 flex gap-2"
        onMouseEnter={() => setIsHovering(true)}
        onMouseLeave={() => setIsHovering(false)}
      >
        <button
          onClick={() => onRatingSelection(1)}
          onMouseEnter={() => onHover(1, "— 'Terrible! Don't watch.'")}
        >
          {isHovering ? (
            <Star {...(hoverRating > 0 && { fill: 'yellow' })} />
          ) : (
            <Star {...(rating > 0 && { fill: 'yellow' })} />
          )}
        </button>
        <button
          onClick={() => onRatingSelection(2)}
          onMouseEnter={() => onHover(2, "— 'Bad experience, avoid.'")}
        >
          {isHovering ? (
            <Star {...(hoverRating > 1 && { fill: 'yellow' })} />
          ) : (
            <Star {...(rating > 1 && { fill: 'yellow' })} />
          )}
        </button>
        <button
          onClick={() => onRatingSelection(3)}
          onMouseEnter={() => onHover(3, "— 'It was okay.'")}
        >
          {isHovering ? (
            <Star {...(hoverRating > 2 && { fill: 'yellow' })} />
          ) : (
            <Star {...(rating > 2 && { fill: 'yellow' })} />
          )}
        </button>
        <button
          onClick={() => onRatingSelection(4)}
          onMouseEnter={() => onHover(4, "— 'Great movie, must watch.'")}
        >
          {isHovering ? (
            <Star {...(hoverRating > 3 && { fill: 'yellow' })} />
          ) : (
            <Star {...(rating > 3 && { fill: 'yellow' })} />
          )}
        </button>
        <button
          onClick={() => onRatingSelection(5)}
          onMouseEnter={() => onHover(5, "— 'One of the best!'")}
        >
          {isHovering ? (
            <Star {...(hoverRating > 4 && { fill: 'yellow' })} />
          ) : (
            <Star {...(rating > 4 && { fill: 'yellow' })} />
          )}
        </button>
      </fieldset>
      {isHovering && <p className="mb-2 font-serif italic">{hoverSubtitle}</p>}
      <textarea
        onFocus={() => setIsTextAreaSelected(true)}
        onBlur={() => setIsTextAreaSelected(false)}
        placeholder={isTextAreaSelected ? '' : 'Share your thoughts?'}
        className={
          'placeholder- h-10 w-80 resize-none rounded-lg border border-grey bg-background px-2 pt-1 text-center font-thin outline-none transition-[height]' +
          ` ${isTextAreaSelected && 'h-28'}`
        }
      />
    </form>
  );
}

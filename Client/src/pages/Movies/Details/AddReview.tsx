import { useState } from 'react';
import Star from '../../../app/assets/icons/Star';
import SubmitButton from '../../../app/components/Buttons/SubmitButton';
import MobileCheckbox from '../../../app/components/Checkboxes/MobileCheckbox';
import { useAddReviewMutation } from '../api/moviesApi';
import { useParams } from 'react-router-dom';

// TODO refactor with react-hook-form or some other form lib

export default function AddReview() {
  const [rating, setRating] = useState(0);
  const [isHovering, setIsHovering] = useState(false);
  const [hoverRating, setHoverRating] = useState(0);
  const [hoverSubtitle, setHoverSubtitle] = useState('');
  const [isTextAreaSelected, setIsTextAreaSelected] = useState(false);
  const [containsSpoilers, setContainsSpoilers] = useState(false);
  const [text, setText] = useState<string>('');
  const { id: movieId } = useParams();

  const [addReview] = useAddReviewMutation();

  async function handleSubmit() {
    try {
      const result = await addReview({
        movieId: Number(movieId),
        containsSpoilers: containsSpoilers,
        rating: rating,
        text: text,
      });
      console.log(result);
    } catch (error) {
      const err = error as Error;

      console.log(err.message);
    }
  }

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

  return (
    <form
      className="flex w-full flex-col items-center"
      onSubmit={(e) => {
        e.preventDefault();
      }}
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
        onChange={(e) => setText(e.target.value)}
        onFocus={() => setIsTextAreaSelected(true)}
        onBlur={() => setIsTextAreaSelected(false)}
        placeholder={isTextAreaSelected ? '' : 'Share your thoughts?'}
        className={
          'mb-4 h-10 w-80 resize-none rounded-lg border border-grey bg-background px-2 pt-1 text-center font-thin outline-none transition-[height]' +
          ` ${isTextAreaSelected && 'h-28'}`
        }
      />
      <div className="flex h-10 items-center justify-center gap-2">
        <label htmlFor="containsSpoilers">Spoilers?</label>
        <MobileCheckbox
          name="containsSpoilers"
          checked={containsSpoilers}
          setChecked={setContainsSpoilers}
        />
      </div>
      <SubmitButton text="Add review" onClick={handleSubmit} />
    </form>
  );
}

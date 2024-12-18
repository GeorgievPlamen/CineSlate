import { useState } from 'react';
import Star from '../../../app/assets/icons/Star';
import SubmitButton from '../../../app/components/Buttons/SubmitButton';
import MobileCheckbox from '../../../app/components/Checkboxes/MobileCheckbox';
import { useParams } from 'react-router-dom';
import extractIdFromLocation from '../../../app/utils/extractIdFromLocation';
import { useAddReviewMutation } from '../../Reviews/api/reviewsApi';
import { FieldValues, useForm } from 'react-hook-form';

interface Props {
  onSuccess: () => void;
}

export default function AddReview({ onSuccess }: Props) {
  const [isHovering, setIsHovering] = useState(false);
  const [hoverRating, setHoverRating] = useState(0);
  const [hoverSubtitle, setHoverSubtitle] = useState('');
  const [isTextAreaSelected, setIsTextAreaSelected] = useState(false);
  const [addReview] = useAddReviewMutation();
  const { id: movieId } = useParams();
  const {
    register,
    handleSubmit,
    watch,
    formState: { errors },
  } = useForm();

  function onHover(rating: number, subtitle: string) {
    setHoverRating(rating);
    setHoverSubtitle(subtitle);
  }

  async function handleOnSubmit(formData: FieldValues) {
    console.log(formData.containsSpoilers);
    // TODO handle error response

    const { data } = await addReview({
      movieId: Number(movieId),
      containsSpoilers: formData.containsSpoilers,
      rating: formData.rating,
      text: formData.text,
    });

    // TODO add zod for validation
    // TODO build review endpoint to get review with id and fill in form
    // TODO edit review
    if (data?.location) {
      const id = extractIdFromLocation(data?.location);

      console.log(id);
      onSuccess();
    }
  }

  return (
    <form
      className="flex w-full flex-col items-center"
      onSubmit={handleSubmit(handleOnSubmit)}
    >
      <fieldset
        aria-label="Rate this product"
        className="mb-3 flex gap-4"
        onMouseEnter={() => setIsHovering(true)}
        onMouseLeave={() => setIsHovering(false)}
      >
        <label
          className="relative"
          onMouseEnter={() => onHover(1, "— 'Terrible! Don't watch.'")}
        >
          <input
            type="radio"
            value={1}
            {...register('rating')}
            className="appearance-none border-transparent bg-transparent checked:bg-transparent checked:bg-none checked:hover:bg-transparent focus:ring-0 focus:ring-offset-0 checked:focus:bg-transparent"
          />
          {isHovering ? (
            <Star
              className="absolute -left-1 top-0.5"
              {...(hoverRating > 0 && { fill: 'yellow' })}
            />
          ) : (
            <Star
              className="absolute -left-1 top-0.5"
              {...(watch('rating') > 0 && { fill: 'yellow' })}
            />
          )}
        </label>
        <label
          className="relative"
          onMouseEnter={() => onHover(2, "— 'Bad experience, avoid.'")}
        >
          <input
            type="radio"
            value={2}
            {...register('rating')}
            className="appearance-none border-transparent bg-transparent checked:bg-transparent checked:bg-none checked:hover:bg-transparent focus:ring-0 focus:ring-offset-0 checked:focus:bg-transparent"
          />
          {isHovering ? (
            <Star
              className="absolute -left-1 top-0.5"
              {...(hoverRating > 1 && { fill: 'yellow' })}
            />
          ) : (
            <Star
              className="absolute -left-1 top-0.5"
              {...(watch('rating') > 1 && { fill: 'yellow' })}
            />
          )}
        </label>
        <label
          className="relative"
          onMouseEnter={() => onHover(3, "— 'It was okay.'")}
        >
          <input
            type="radio"
            value={3}
            {...register('rating')}
            className="appearance-none border-transparent bg-transparent checked:bg-transparent checked:bg-none checked:hover:bg-transparent focus:ring-0 focus:ring-offset-0 checked:focus:bg-transparent"
          />
          {isHovering ? (
            <Star
              className="absolute -left-1 top-0.5"
              {...(hoverRating > 2 && { fill: 'yellow' })}
            />
          ) : (
            <Star
              className="absolute -left-1 top-0.5"
              {...(watch('rating') > 2 && { fill: 'yellow' })}
            />
          )}
        </label>
        <label
          className="relative"
          onMouseEnter={() => onHover(4, "— 'Great movie, must watch.'")}
        >
          <input
            type="radio"
            value={4}
            {...register('rating')}
            className="appearance-none border-transparent bg-transparent checked:bg-transparent checked:bg-none checked:hover:bg-transparent focus:ring-0 focus:ring-offset-0 checked:focus:bg-transparent"
          />
          {isHovering ? (
            <Star
              className="absolute -left-1 top-0.5"
              {...(hoverRating > 3 && { fill: 'yellow' })}
            />
          ) : (
            <Star
              className="absolute -left-1 top-0.5"
              {...(watch('rating') > 3 && { fill: 'yellow' })}
            />
          )}
        </label>
        <label
          className="relative"
          onMouseEnter={() => onHover(5, "— 'One of the best!'")}
        >
          <input
            type="radio"
            value={5}
            {...register('rating')}
            className="appearance-none border-transparent bg-transparent checked:bg-transparent checked:bg-none checked:hover:bg-transparent focus:ring-0 focus:ring-offset-0 checked:focus:bg-transparent"
          />
          {isHovering ? (
            <Star
              className="absolute -left-1 top-0.5"
              {...(hoverRating > 4 && { fill: 'yellow' })}
            />
          ) : (
            <Star
              className="absolute -left-1 top-0.5"
              {...(watch('rating') > 4 && { fill: 'yellow' })}
            />
          )}
        </label>
      </fieldset>
      {isHovering && <p className="mb-2 font-serif italic">{hoverSubtitle}</p>}
      <textarea
        placeholder={isTextAreaSelected ? '' : 'Share your thoughts?'}
        className={
          'mb-4 h-10 w-80 resize-none rounded-lg border border-grey bg-background px-2 pt-1 text-center font-thin outline-none transition-[height]' +
          ` ${isTextAreaSelected && 'h-28'}`
        }
        {...register('text')}
        onFocus={() => setIsTextAreaSelected(true)}
        onBlur={() => setIsTextAreaSelected(false)}
      />
      <div className="flex h-10 items-center justify-center gap-2">
        <label htmlFor="containsSpoilers">Spoilers?</label>
        <MobileCheckbox name="containsSpoilers" register={register} />
      </div>
      <SubmitButton text="Add review" />
    </form>
  );
}

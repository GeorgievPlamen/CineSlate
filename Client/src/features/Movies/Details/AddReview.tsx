import { useEffect, useState } from 'react';

import { useMutation, useQuery } from '@tanstack/react-query';
import { reviewsClient } from '@/features/Reviews/api/reviewsClient';
import Star from '@/assets/icons/Star';
import SubmitButton from '@/components/Buttons/SubmitButton';
import MobileCheckbox from '@/components/Checkboxes/MobileCheckbox';
import Loading from '@/components/Loading/Loading';
import ValidationError from '@/components/ValidationError';
import { Review } from '@/features/Reviews/models/review';
import { zodResolver } from '@hookform/resolvers/zod';
import { useForm, FieldValues } from 'react-hook-form';
import { useParams } from 'react-router-dom';

interface Props {
  onSuccess: () => void;
}

export default function AddReview({ onSuccess }: Props) {
  const [isHovering, setIsHovering] = useState(false);
  const [hoverRating, setHoverRating] = useState(0);
  const [hoverSubtitle, setHoverSubtitle] = useState('');
  const [isTextAreaSelected, setIsTextAreaSelected] = useState(false);
  const { id: movieId } = useParams();
  const { mutateAsync: addReview } = useMutation({
    mutationFn: ({
      rating,
      movieId,
      text,
      containsSpoilers,
    }: {
      rating: number;
      movieId: number;
      text: string;
      containsSpoilers: boolean;
    }) => reviewsClient.addReview(rating, movieId, text, containsSpoilers),
  });
  const { mutateAsync: updateReview } = useMutation({
    mutationFn: ({
      reviewId,
      rating,
      movieId,
      text,
      containsSpoilers,
    }: {
      reviewId: string;
      rating: number;
      movieId: number;
      text: string;
      containsSpoilers: boolean;
    }) =>
      reviewsClient.updateReview(
        reviewId,
        rating,
        movieId,
        text,
        containsSpoilers
      ),
  });

  const {
    data: ownReviewData,
    isFetching: isOwnReviewFetching,
    refetch: refetchOwnedReview,
  } = useQuery({
    queryKey: ['', movieId],
    queryFn: () => reviewsClient.ownedReviewsByMovieId(movieId ?? ''),
  });

  const {
    register,
    handleSubmit,
    getValues,
    setValue,
    formState: { errors },
  } = useForm<Review>({
    resolver: zodResolver(Review),
  });

  useEffect(() => {
    if (ownReviewData) {
      setValue('rating', `${ownReviewData.rating}`);
      setValue('text', ownReviewData.text);
      setValue('containsSpoilers', ownReviewData.containsSpoilers);
    }
  }, [ownReviewData, setValue]);

  function onHover(rating: number, subtitle: string) {
    setHoverRating(rating);
    setHoverSubtitle(subtitle);
  }

  async function handleOnSubmit(formData: FieldValues) {
    let isSuccess;

    if (ownReviewData) {
      const { location } = await updateReview({
        reviewId: ownReviewData.id ?? '',
        movieId: Number(movieId),
        containsSpoilers: formData.containsSpoilers,
        rating: formData.rating,
        text: formData.text,
      });

      isSuccess = location !== undefined;
    } else {
      const { location } = await addReview({
        movieId: Number(movieId),
        containsSpoilers: formData.containsSpoilers,
        rating: formData.rating,
        text: formData.text,
      });

      isSuccess = location !== undefined;

      await refetchOwnedReview();
    }

    if (isSuccess) {
      onSuccess();
    }
  }

  if (isOwnReviewFetching) return <Loading />;

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
              className="absolute top-0.5 -left-1"
              {...(hoverRating > 0 && { fill: 'yellow' })}
            />
          ) : (
            <Star
              className="absolute top-0.5 -left-1"
              {...(Number(getValues('rating')) > 0 && { fill: 'yellow' })}
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
              className="absolute top-0.5 -left-1"
              {...(hoverRating > 1 && { fill: 'yellow' })}
            />
          ) : (
            <Star
              className="absolute top-0.5 -left-1"
              {...(Number(getValues('rating')) > 1 && { fill: 'yellow' })}
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
              className="absolute top-0.5 -left-1"
              {...(hoverRating > 2 && { fill: 'yellow' })}
            />
          ) : (
            <Star
              className="absolute top-0.5 -left-1"
              {...(Number(getValues('rating')) > 2 && { fill: 'yellow' })}
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
              className="absolute top-0.5 -left-1"
              {...(hoverRating > 3 && { fill: 'yellow' })}
            />
          ) : (
            <Star
              className="absolute top-0.5 -left-1"
              {...(Number(getValues('rating')) > 3 && { fill: 'yellow' })}
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
              className="absolute top-0.5 -left-1"
              {...(hoverRating > 4 && { fill: 'yellow' })}
            />
          ) : (
            <Star
              className="absolute top-0.5 -left-1"
              {...(Number(getValues('rating')) > 4 && { fill: 'yellow' })}
            />
          )}
        </label>
      </fieldset>
      {isHovering && <p className="mb-2 font-serif italic">{hoverSubtitle}</p>}
      <textarea
        placeholder={isTextAreaSelected ? '' : 'Share your thoughts?'}
        className={
          'border-grey bg-background mb-4 h-10 w-80 resize-none rounded-lg border px-2 pt-1 text-center font-thin transition-[height] outline-none' +
          ` ${isTextAreaSelected && 'h-28'}`
        }
        {...register('text')}
        onFocus={() => setIsTextAreaSelected(true)}
        onBlur={() => {
          setTimeout(() => {
            setIsTextAreaSelected(false);
          }, 50);
        }}
      />
      <div className="flex h-10 items-center justify-center gap-2">
        <label htmlFor="containsSpoilers">Spoilers?</label>
        <MobileCheckbox
          isChecked={ownReviewData?.containsSpoilers}
          name="containsSpoilers"
          register={register}
        />
      </div>
      <SubmitButton
        className="mb-2"
        text={ownReviewData === undefined ? 'Add review' : 'Update review'}
      />
      <ValidationError
        isError={errors !== undefined}
        message={errors.rating?.message ?? ''}
      />
    </form>
  );
}

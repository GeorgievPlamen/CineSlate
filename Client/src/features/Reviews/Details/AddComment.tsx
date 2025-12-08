import { useForm } from 'react-hook-form';
import { useMutation } from '@tanstack/react-query';
import { reviewsClient } from '../api/reviewsClient';
import SubmitButton from '@/components/Buttons/SubmitButton';

interface Props {
  reviewId: string;
  refetchComments: () => void;
}

export default function AddComment({ reviewId, refetchComments }: Props) {
  const { register, handleSubmit } = useForm<{ comment: string }>();
  const commentReviewMutation = useMutation({
    mutationFn: ({
      reviewId,
      comment,
    }: {
      reviewId: string;
      comment: string;
    }) => reviewsClient.commentReview(reviewId, comment),
  });

  return (
    <form
      onSubmit={handleSubmit(({ comment }) => {
        commentReviewMutation
          .mutateAsync({ reviewId, comment: '"' + comment + '"' })
          .then(() => refetchComments());
      })}
    >
      <textarea
        placeholder={'Share your thoughts?'}
        className="border-grey bg-background my-4 h-20 w-full resize-none rounded-lg border px-2 pt-1 text-center font-thin transition-[height] outline-none"
        {...register('comment')}
      />
      <SubmitButton
        className="mb-2"
        text={'Add Comment'}
        isLoading={commentReviewMutation.isPending}
      />
    </form>
  );
}

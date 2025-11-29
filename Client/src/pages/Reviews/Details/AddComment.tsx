import { useForm } from 'react-hook-form';
import SubmitButton from '../../../app/components/Buttons/SubmitButton';
import { useCommentReviewMutation } from '../api/reviewsApi';

interface Props {
  reviewId: string;
  refetchComments: () => void;
}

export default function AddComment({ reviewId, refetchComments }: Props) {
  const { register, handleSubmit } = useForm<{ comment: string }>();
  const [commentReview] = useCommentReviewMutation();

  return (
    <form
      onSubmit={handleSubmit(({ comment }) => {
        commentReview({ reviewId, comment: '"' + comment + '"' }).then(() =>
          refetchComments()
        );
      })}
    >
      <textarea
        placeholder={'Share your thoughts?'}
        className="border-grey bg-background my-4 h-20 w-full resize-none rounded-lg border px-2 pt-1 text-center font-thin transition-[height] outline-none"
        {...register('comment')}
      />
      <SubmitButton className="mb-2" text={'Add Comment'} />
    </form>
  );
}

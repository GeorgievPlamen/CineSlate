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
        className="my-4 h-20 w-full resize-none rounded-lg border border-grey bg-background px-2 pt-1 text-center font-thin outline-none transition-[height]"
        {...register('comment')}
      />
      <SubmitButton className="mb-2" text={'Add Comment'} />
    </form>
  );
}

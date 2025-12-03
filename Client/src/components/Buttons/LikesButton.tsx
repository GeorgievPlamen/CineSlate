import { HeartIconOutlined, HeartIcon } from '../../Icons/HeartIcon';
import Tooltip from '../Tooltip/Tooltip';
import { useLikeReviewMutation } from '../../features/Reviews/api/reviewsApi';
import { reviewsClient } from '@/features/Reviews/api/reviewsClient';
import { useQuery } from '@tanstack/react-query';

interface Props {
  reviewId: string;
}

export default function LikesButton({ reviewId }: Props) {
  const [likeReview] = useLikeReviewMutation();
  const { data, refetch } = useQuery({
    queryKey: ['reviewDetailsById', reviewId],
    queryFn: () => reviewsClient.reviewDetailsById(reviewId ?? ''),
  });

  async function handleLike() {
    const { error } = await likeReview({ reviewId: reviewId });
    if (!error) await refetch();
  }

  return (
    <div className="flex gap-2">
      <button onClick={handleLike}>
        {data?.hasUserLiked ? (
          <HeartIcon className="hover:text-opacity-80 active:text-opacity-50" />
        ) : (
          <HeartIconOutlined className="hover:text-opacity-80 active:text-opacity-50" />
        )}
      </button>
      {data?.usersWhoLiked && (
        <Tooltip content={data.usersWhoLiked.map((u) => u.onlyName).join(' ')}>
          <p className="hover:underline">{data?.likes}</p>
        </Tooltip>
      )}
    </div>
  );
}

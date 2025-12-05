import Tooltip from '../Tooltip/Tooltip';
import { reviewsClient } from '@/features/Reviews/api/reviewsClient';
import { HeartIcon, HeartIconOutlined } from '@/Icons/HeartIcon';
import { useMutation, useQuery } from '@tanstack/react-query';

interface Props {
  reviewId: string;
}

export default function LikesButton({ reviewId }: Props) {
  const { mutateAsync: likeReview } = useMutation({
    mutationFn: () => reviewsClient.likeReview(reviewId),
  });
  const { data, refetch } = useQuery({
    queryKey: ['reviewDetailsById', reviewId],
    queryFn: () => reviewsClient.reviewDetailsById(reviewId ?? ''),
  });

  async function handleLike() {
    const { id } = await likeReview();
    if (!id) await refetch();
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

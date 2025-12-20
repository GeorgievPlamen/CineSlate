import Tooltip from '../Tooltip/Tooltip';
import { HeartIcon, HeartIconOutlined } from '@/Icons/HeartIcon';
import { reviewsClient } from '@/modules/Review/api/reviewsClient';
import { useMutation, useQuery } from '@tanstack/react-query';

interface Props {
  reviewId: string;
}

export default function LikesButton({ reviewId }: Props) {
  const { mutateAsync: likeReview } = useMutation({
    mutationFn: () => reviewsClient.likeReview(reviewId),
    onSuccess: () => refetch(),
  });

  const { data, refetch } = useQuery({
    queryKey: ['reviewDetailsById', reviewId],
    queryFn: () => reviewsClient.reviewDetailsById(reviewId ?? ''),
  });

  return (
    <div className="flex gap-2">
      <button onClick={() => likeReview()}>
        {data?.hasUserLiked ? (
          <HeartIcon className="hover:opacity-80 active:opacity-50" />
        ) : (
          <HeartIconOutlined className="hover:opacity-80 active:opacity-50" />
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

import { useState } from 'react';
import { HeartIconOutlined, HeartIcon } from '../../Icons/HeartIcon';
import Tooltip from '../Tooltip/Tooltip';

interface Props {
  reviewId: string;
}
export default function TooltipButtonLikes({ reviewId }: Props) {
  const [likes, setLikes] = useState(1);
  const [hasLiked, setHasLiked] = useState(false);

  // TODO fetch likes
  // TODO fetch people who liked on tooltip hover

  return (
    <div className="flex gap-2">
      <button onClick={() => setHasLiked(!hasLiked)}>
        {hasLiked ? (
          <HeartIcon className="hover:text-opacity-80 active:text-opacity-50" />
        ) : (
          <HeartIconOutlined className="hover:text-opacity-80 active:text-opacity-50" />
        )}
      </button>
      <Tooltip content="">
        <p
          className="hover:underline"
          onMouseEnter={() => console.log('fetch data')}
        >
          {likes}
        </p>
      </Tooltip>
    </div>
  );
}

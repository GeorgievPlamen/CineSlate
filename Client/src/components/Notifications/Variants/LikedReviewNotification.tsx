import Spinner from '@/components/Spinner';
import { BACKUP_PROFILE, IMG_PATH } from '@/config';
import { base64ToImage } from '@/lib/utils';
import { moviesClient } from '@/modules/Movies/api/moviesClient';
import { NotificationResponse } from '@/modules/Notifications/models/notificationModels';
import { usersClient } from '@/modules/Users/api/usersClient';
import { useQuery } from '@tanstack/react-query';
import { useNavigate } from '@tanstack/react-router';
import { useState } from 'react';

interface Props {
  notification: NotificationResponse;
}

export default function LikedReviewNotification({ notification }: Props) {
  const userId = notification.data['user-id'];
  const movieId = notification.data['movie-id'];
  const authorId = notification.data['author-id'];
  const reviewId = notification.data['review-id'];

  const [imageIsLoading, setImageIsLoading] = useState(true);
  const nav = useNavigate();

  const isValid = !!userId && !!movieId && !!authorId && !!reviewId;

  const { data: usersData, isLoading: isUserLoading } = useQuery({
    queryKey: ['getUsersByIds', userId],
    queryFn: () => usersClient.getUsersByIds([userId]),
    enabled: isValid,
  });

  const { data: movieData, isLoading: isMovieLoading } = useQuery({
    queryKey: ['movieDetails', movieId],
    queryFn: () => moviesClient.getMovieDetails(movieId),
    enabled: isValid,
  });

  const isLoading = isUserLoading || isMovieLoading;

  if (!isValid) {
    return <div className="text-red-500">Invalid notification</div>;
  }

  if (isLoading) {
    return <Spinner />;
  }

  return (
    <button
      onClick={() => nav({ to: '/reviews/$id', params: { id: reviewId } })}
      className="flex items-center justify-around gap-2 text-start"
    >
      <img
        src={
          usersData?.[0].pictureBase64?.length &&
          usersData?.[0].pictureBase64?.length > 0
            ? base64ToImage(usersData?.[0].pictureBase64)
            : BACKUP_PROFILE
        }
        alt="profile-pic"
        className="h-14 w-14 rounded-full object-cover"
      />
      <p>
        <span className="text-secondary">
          {usersData?.[0].username.split('#')[0]}
        </span>{' '}
        has liked your review of{' '}
        <span className="font-bold">{movieData?.title}</span>
      </p>
      <img
        className={'w-14 rounded-lg ' + ` ${imageIsLoading ? 'hidden' : ''}`}
        src={IMG_PATH + movieData?.posterPath}
        alt="poster"
        onLoad={() => setImageIsLoading(false)}
      />
      {/* </Link> */}
    </button>
  );
}

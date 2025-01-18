import { useState } from 'react';
import Button from '../../app/components/Buttons/Button';
import MovieReviewCard from '../../app/components/Cards/MovieReviewCard';
import { BACKUP_PROFILE } from '../../app/config';
import { useGetReviewsByAuthorIdQuery } from '../CriticDetails/api/criticDetailsApi';
import { useDispatchUser, useUser } from '../Users/userSlice';
import { useUpdateUserMutation } from './api/myDetailsApi';
import EditIcon from '../../app/Icons/EditIcon';
import { CheckIcon } from '@heroicons/react/16/solid';
import TextField from '../../app/components/Fields/TextField';
import { useForm } from 'react-hook-form';
import { UserModel } from './models/UserModel';
import { zodResolver } from '@hookform/resolvers/zod';

function MyDetails() {
  const user = useUser();
  const [updateUser] = useUpdateUserMutation();
  const [reviewsPage, setReviewsPage] = useState(1);
  const [editing, setEditing] = useState(false);
  const { setMyBio } = useDispatchUser();

  const { data: reviewData, isFetching: isReviewsFetching } =
    useGetReviewsByAuthorIdQuery(
      {
        id: user?.id ?? '',
        page: reviewsPage,
      },
      { skip: !user.id }
    );

  const { register, getValues } = useForm<UserModel>({
    resolver: zodResolver(UserModel),
  });

  async function handleEdit() {
    if (editing) {
      const { bio } = getValues();

      if (bio && user.id) {
        await updateUser({ bio: bio, id: user?.id });
        setMyBio(bio);
      }
    }

    setEditing(!editing);
  }

  return (
    <article className="m-auto flex w-2/3 flex-col">
      <section className="mt-5">
        <div className="flex items-center justify-between gap-2">
          <div className="relative flex w-1/4 gap-2">
            <img src={BACKUP_PROFILE} alt="profile-pic" className="h-32 w-32" />
            <Button
              className="absolute bottom-1 left-24 min-h-8 min-w-8"
              onClick={handleEdit}
            >
              {editing ? <CheckIcon /> : <EditIcon />}
            </Button>
            <div className="flex flex-col">
              <h2 className="mb-2 mt-5 min-w-44 font-arvo text-xl">
                {user?.username.split('#')[0]}
              </h2>
              {editing ? (
                <TextField
                  defaultValue={user?.bio}
                  register={register('bio')}
                  className="font-roboto text-sm text-grey"
                />
              ) : (
                <p className="font-roboto text-sm text-grey">{user?.bio}</p>
              )}
            </div>
          </div>
          <div className="p-2">
            <p className="text-center font-arvo text-lg">
              {reviewData?.totalCount}
            </p>
            <p className="text-xs font-light text-grey">Reviews</p>
          </div>
        </div>
      </section>
      <section className="m-auto w-2/3">
        <h3 className="my-4 ml-2 font-arvo text-lg">Recent Reviews</h3>
        <div className="mb-20 flex flex-col gap-6">
          {reviewData?.values.map((r) => (
            <MovieReviewCard key={r.movieId} review={r} />
          ))}
          {reviewData?.hasNextPage && (
            <Button
              onClick={() => setReviewsPage((prev) => prev + 1)}
              className="w-fit px-10"
              isLoading={isReviewsFetching}
            >
              Load More
            </Button>
          )}
        </div>
      </section>
    </article>
  );
}

export default MyDetails;

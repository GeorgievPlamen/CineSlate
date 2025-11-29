import { useState } from 'react';
import { useGetLatestUsersQuery } from '../Users/api/userApiRTK';
import Loading from '../../app/components/Loading/Loading';
import ErrorMessage from '../../app/components/ErrorMessage/ErrorMessage';
import UserCard from '../../app/components/Cards/UserCard';
import Button from '../../app/components/Buttons/Button';
import { useSetCritics } from './criticsSlice';

function Critics() {
  const [page, setPage] = useState(1);
  const { data, isFetching, isError } = useGetLatestUsersQuery({ page });

  useSetCritics(data?.values);

  if (isFetching) return <Loading />;
  if (isError) return <ErrorMessage />;

  return (
    <section className="flex flex-col items-center justify-center">
      <h2 className="font-arvo my-4 text-2xl">Our latest members</h2>
      <article className="flex w-1/2 max-w-fit flex-row flex-wrap justify-center gap-4">
        {data?.values.map((u) => <UserCard user={u} key={u.username} />)}
      </article>
      {data?.hasNextPage && (
        <Button
          onClick={() => setPage((prev) => prev + 1)}
          className="my-4 w-fit px-10"
          isLoading={isFetching}
        >
          Load More
        </Button>
      )}
    </section>
  );
}
export default Critics;

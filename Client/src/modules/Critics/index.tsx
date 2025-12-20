import { useState } from 'react';
import Button from '@/components/Buttons/Button';
import ErrorMessage from '@/components/ErrorMessage/ErrorMessage';
import Loading from '@/components/Loading/Loading';
import { useQuery } from '@tanstack/react-query';
import appContants from '@/common/appConstants';
import UserCard from '@/components/Cards/UserCard';
import { usersClient } from '../Users/api/usersClient';

export default function Critics() {
  const [page, setPage] = useState(1);
  const {data , isFetching, isError} = useQuery({
    queryKey: ["latestUsers", page],
    queryFn: () => usersClient.getLatestUsers(page),
    staleTime: appContants.STALE_TIME
  })

  if (isFetching) return <Loading />;
  if (isError) return <ErrorMessage />;

  return (
    <section className="flex flex-col items-center justify-center">
      <h2 className="font-heading my-4 text-2xl">Our latest members</h2>
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

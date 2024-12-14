import { NavLink, useParams } from 'react-router-dom';
import { useMovieDetailsQuery } from '../api/moviesApi';
import { IMG_PATH } from '../../../app/config';
import Backdrop from '../../../app/components/Backdrop/Backdrop';
import { useState } from 'react';
import Loading from '../../../app/components/Loading/Loading';
import ErrorMessage from '../../../app/components/ErrorMessage/ErrorMessage';
import ReviewCard from '../../../app/components/Cards/ReviewCard';
import GenreButton from '../../../app/components/Buttons/GenreButton';
import AddReview from './AddReview';
import useAuth from '../../../app/hooks/useAuth';

export default function MovieDetails() {
  const { id } = useParams();
  const [imageIsLoading, setImageIsLoading] = useState(true);
  const { data, isFetching, isError, refetch } = useMovieDetailsQuery({ id });
  const { isAuthenticated } = useAuth();

  if (isFetching) return <Loading />;

  if (isError) return <ErrorMessage />;

  return (
    <>
      <Backdrop path={data?.backdropPath} />
      {imageIsLoading && <Loading />}
      <article className="mt-20">
        <article className="mx-auto flex w-full flex-col items-center justify-center">
          <div className="flex">
            <div className="flex flex-col items-center justify-center">
              <img
                className={
                  'mb-4 w-80 rounded-lg border border-grey' +
                  ` ${imageIsLoading ? 'hidden' : ''}`
                }
                src={IMG_PATH + data?.posterPath}
                alt="poster"
                onLoad={() => setImageIsLoading(false)}
              />
              {isAuthenticated ? (
                <AddReview onSuccess={refetch} />
              ) : (
                <p>
                  <NavLink to="../login" className="underline">
                    Sign in
                  </NavLink>{' '}
                  to review
                </p>
              )}
            </div>
            <section className="mx-10 my-5 w-1/2 max-w-[700px]">
              <div className="flex w-fit items-center gap-4">
                <h2 className="mb-2 font-arvo text-3xl font-bold">
                  {data?.title}
                </h2>
                <p className="text-sm font-light">
                  {data?.releaseDate.toString()}
                </p>
                <p className="text-xl font-bold">
                  ⭐
                  {data?.rating === 0
                    ? 'Be the first to review!'
                    : data?.rating}
                </p>
              </div>
              <p className="font-roboto">{data?.description}</p>
              <p className="my-4 font-serif text-xl italic">{data?.tagline}</p>
              <section className="mt-4 h-full flex-row gap-2">
                {data?.genres.map((g) => (
                  <GenreButton key={g.id} name={g.value} />
                ))}
              </section>
            </section>
          </div>
          <section className="my-10 flex flex-col gap-10">
            {data?.reviews.map((r) => <ReviewCard key={r.authorId} r={r} />)}
          </section>
        </article>
      </article>
    </>
  );
}

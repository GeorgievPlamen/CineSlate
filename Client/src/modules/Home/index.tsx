import Backdrop from '@/components/Backdrop/Backdrop';
import MovieCard, { MovieCardSkeleton } from '@/components/Cards/MovieCard';
import MovieReviewCard, {
  MovieReviewCardSkeleton,
} from '@/components/Cards/MovieReviewCard';
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from '@/components/ui/carousel';
import { useQuery } from '@tanstack/react-query';
import AutoScroll from 'embla-carousel-auto-scroll';
import { MoviesBy, moviesClient } from '../Movies/api/moviesClient';
import { ReviewsBy, reviewsClient } from '../Review/api/reviewsClient';

function Home() {
  const { data: movies, isLoading: isMoviesLoading } = useQuery({
    queryKey: ['movies-nowplaying-home'],
    queryFn: () => moviesClient.getPagedMovies(MoviesBy.NowPlaying, 1),
  });

  const { data: reviews, isLoading: isReviewsLoading } = useQuery({
    queryKey: ['reviews-latest-home'],
    queryFn: () => reviewsClient.reviewsBy(1, ReviewsBy.Latest),
  });

  const randomId = Number((Math.random() * 19).toFixed());
  const randomMovieId = movies?.values[randomId]?.id;

  const { data: randomMovieDetails } = useQuery({
    queryKey: ['movie-details-home', randomMovieId],
    queryFn: () => moviesClient.getMovieDetails(`${randomMovieId}`),
  });

  return (
    <div className="mx-auto">
      <Backdrop path={randomMovieDetails?.backdropPath} />
      <div className="flex flex-col items-center justify-center rounded-xl pt-8">
        <div className="max-w-4xl w-80 md:w-full">
          <h1 className="font-heading text-primary mb-8 text-center text-xl font-bold md:text-2xl">
            Find Your Next Favorite Movie Instantly! 🍿
          </h1>

          <section className="mb-8">
            <p className="text-muted-foreground mb-4">
              <span className="font-heading text-secondary text-lg font-bold italic">
                Welcome to CineSlate
              </span>{' '}
              , the fastest and easiest way to discover great movies to watch!
              Whether you’re in the mood for an action-packed thriller, a
              heartwarming drama, or a mind-bending sci-fi epic, CineSlate helps
              you find the perfect film in seconds.
            </p>
            <ul className="text-muted-foreground ml-6 list-disc space-y-2 mb-6">
              <li>
                <span className="text-foreground font-bold">
                  Effortless Movie Discovery:
                </span>{' '}
                Instantly find any movie by title.
              </li>
              <li>
                <span className="text-foreground font-bold">Trending Now:</span>{' '}
                See the most popular movies everyone’s watching.
              </li>
              <li>
                <span className="text-foreground font-bold">
                  Filter by Genre:
                </span>{' '}
                Find exactly what you're in the mood for.
              </li>
              <li>
                <span className="text-foreground font-bold">
                  Fast & Responsive:
                </span>{' '}
                No waiting, no clutter—just movies!
              </li>
            </ul>
            <Carousel
              className="w-full"
              opts={{
                loop: true,
                align: 'center',
                dragFree: true,
              }}
              plugins={[
                AutoScroll({
                  stopOnMouseEnter: true,
                  stopOnInteraction: false,
                }),
              ]}
            >
              <CarouselContent className="-ml-1">
                {isMoviesLoading
                  ? Array.from({ length: 10 }).map(() => (
                      <CarouselItem
                        key={crypto.randomUUID()}
                        className="basis-1/1 md:basis-1/3"
                      >
                        <MovieCardSkeleton />
                      </CarouselItem>
                    ))
                  : movies?.values.map((m) => (
                      <CarouselItem
                        key={m.id}
                        className="basis-1/1 md:basis-1/3"
                      >
                        <MovieCard
                          id={m.id}
                          posterPath={m.posterPath}
                          rating={m.rating}
                          releaseDate={m.releaseDate}
                          title={m.title}
                        />
                      </CarouselItem>
                    ))}
              </CarouselContent>
            </Carousel>
          </section>
          <section className="mb-12">
            <p className="text-muted-foreground mb-4 text">
              <span className="font-heading text-secondary text-lg font-bold italic">
                Everyone's a Critic
              </span>{' '}
              – Share Your Voice! CineSlate is powered by movie lovers like you!
              No paid critics, no professional reviewers—just real opinions from
              real people.
            </p>
            <ul className="text-muted-foreground ml-6 list-disc space-y-2 mb-6">
              <li>
                <span className="text-foreground font-bold">
                  Write Reviews:
                </span>{' '}
                Share your thoughts on any movie.
              </li>
              <li>
                <span className="text-foreground font-bold">
                  Rate & Comment:
                </span>{' '}
                Engage in discussions and see what others think.
              </li>
              <li>
                <span className="text-foreground font-bold">
                  Follow Users & Critics:
                </span>{' '}
                Keep up with your favorite reviewers and recommendations.
              </li>
            </ul>
            {isReviewsLoading ? (
              <Carousel className="w-full hidden md:block">
                <CarouselPrevious />
                <CarouselNext />
                <CarouselContent className="-ml-1">
                  {Array.from({ length: 10 }).map(() => (
                    <CarouselItem
                      key={crypto.randomUUID()}
                      className="lg:basis-6/10 basis-3/4"
                    >
                      <MovieReviewCardSkeleton />
                    </CarouselItem>
                  ))}
                </CarouselContent>
              </Carousel>
            ) : (
              reviews &&
              reviews?.values?.length > 0 && (
                <Carousel className="w-full hidden md:block">
                  <CarouselPrevious />
                  <CarouselNext />
                  <CarouselContent className="-ml-1">
                    {reviews?.values.map((r) => (
                      <CarouselItem
                        key={r.id}
                        className="lg:basis-6/10 basis-3/4"
                      >
                        <MovieReviewCard review={r} />
                      </CarouselItem>
                    ))}
                  </CarouselContent>
                </Carousel>
              )
            )}
          </section>
        </div>
      </div>
    </div>
  );
}

export default Home;

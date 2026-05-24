import MovieCard, { MovieCardSkeleton } from '@/components/Cards/MovieCard';
import MovieReviewCard, {
  MovieReviewCardSkeleton,
} from '@/components/Cards/MovieReviewCard';
import {
  Carousel,
  CarouselContent,
  CarouselItem,
} from '@/components/ui/carousel';
import { useQuery } from '@tanstack/react-query';
import AutoScroll from 'embla-carousel-auto-scroll';
import { MoviesBy, moviesClient } from '../Movies/api/moviesClient';
import { ReviewsBy, reviewsClient } from '../Review/api/reviewsClient';
import MovieHero from '@/components/Backdrop/MovieHero';
import Button from '@/components/Buttons/Button';
import ButtonOutlined from '@/components/Buttons/ButtonOutlined';
import { Link, useNavigate } from '@tanstack/react-router';

function Home() {
  const { data: movies, isLoading: isMoviesLoading } = useQuery({
    queryKey: ['movies-nowplaying-home'],
    queryFn: () => moviesClient.getPagedMovies(MoviesBy.Popular, 1),
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

  const nav = useNavigate();

  return (
    <div className="mx-auto">
      <MovieHero path={randomMovieDetails?.backdropPath} />
      <div className="flex flex-col justify-between md:justify-end borde w-full mb-4 md:mb-12 min-h-50 md:min-h-80 lg:min-h-100 xl:min-h-120">
        <Link
          to="/movies/$id"
          params={{
            id: `${randomMovieDetails?.id}`,
          }}
        >
          <h2 className="line-clamp-2 mb-4 ml-2 text-[2rem] md:text-[3rem] md:text-start lg:text-[4rem] font-heading-stylized text-center max-h-40 lg:max-h-80 max-w-140 xl:max-w-180 hover:text-primary-hover active:text-primary-active">
            {randomMovieDetails?.title}
          </h2>
        </Link>
        <div className="flex gap-4 items-center md:justify-start md:ml-6 justify-center">
          <Button
            onClick={() =>
              nav({
                to: '/register',
              })
            }
            className="px-2 cursor-pointer"
          >
            Join the community
          </Button>
          <ButtonOutlined
            onClick={() =>
              nav({
                to: '/movies',
              })
            }
            className="px-2 font-light cursor-pointer"
          >
            Explore movies
          </ButtonOutlined>
        </div>
      </div>
      <div className="mb-8">
        <h2 className="font-heading font-semibold text-li prose ml-6 mb-3 text-xl">
          RECENT REVIEWS
        </h2>
        {isReviewsLoading ? (
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
                speed: 1,
              }),
            ]}
          >
            <CarouselContent className="-ml-1">
              {Array.from({ length: 10 }).map(() => (
                <CarouselItem
                  key={crypto.randomUUID()}
                  className="basis-1/1 min-[600px]:basis-5/6  md:basis-2/3 lg:basis-5/10 xl:basis-4/10 min-[101rem]:basis-3/10"
                >
                  <MovieReviewCardSkeleton />
                </CarouselItem>
              ))}
            </CarouselContent>
          </Carousel>
        ) : (
          reviews &&
          reviews?.values?.length > 0 && (
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
                  speed: 1,
                }),
              ]}
            >
              <CarouselContent className="-ml-1">
                {reviews?.values.map((r) => (
                  <CarouselItem
                    key={r.id}
                    className="basis-4/3 min-[500px]:basis-1/1 min-[600px]:basis-5/6  md:basis-2/3 lg:basis-5/10 xl:basis-4/10 min-[101rem]:basis-3/10 min-[130rem]:basis-2/8  min-[152rem]:basis-2/10"
                  >
                    <MovieReviewCard review={r} />
                  </CarouselItem>
                ))}
              </CarouselContent>
            </Carousel>
          )
        )}
      </div>
      <div>
        <h2 className="font-heading font-semibold text-li prose ml-6 mb-3 text-xl">
          TRENDING NOW
        </h2>
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
              direction: 'backward',
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
                    className="basis-1/2 md:basis-1/3 lg:basis-2/8  min-[101rem]:basis-2/12"
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
      </div>
    </div>
  );
}

export default Home;

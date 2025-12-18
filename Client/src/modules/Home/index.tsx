import MovieCard from '@/components/Cards/MovieCard';
import ReviewCard from '@/components/Cards/ReviewCard';
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from '@/components/ui/carousel';
import { useUserStore } from '@/store/userStore';
import { getRouteApi, Link } from '@tanstack/react-router';
import AutoScroll from 'embla-carousel-auto-scroll';

const route = getRouteApi('/');
function Home() {
  const user = useUserStore((state) => state.user);
  const { movies, reviews } = route.useLoaderData();

  console.log(movies);
  console.log(reviews);

  // TODO MovieReviewCard
  // TODO pick one of the movies at random and use the background image
  // TODO smaller text
  return (
    <div className="flex flex-col items-center justify-center rounded-xl p-8">
      <div className="max-w-4xl">
        <h1 className="font-heading text-primary mb-12 text-center text-3xl font-bold md:text-4xl">
          Find Your Next Favorite Movie Instantly! 🍿
        </h1>

        <section className="mb-12">
          <p className="text-muted-foreground mb-4 text-lg">
            <span className="font-heading text-secondary text-xl font-bold italic">
              Welcome to CineSlate
            </span>{' '}
            , the fastest and easiest way to discover great movies to watch!
            Whether you’re in the mood for an action-packed thriller, a
            heartwarming drama, or a mind-bending sci-fi epic, CineSlate helps
            you find the perfect film in seconds.
          </p>
          <ul className="text-muted-foreground ml-6 list-disc space-y-2">
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
        </section>
        <Carousel
          className="w-full"
          opts={{
            loop: true,
            align: 'center',
            dragFree: true,
          }}
          plugins={[
            AutoScroll({
              startDelay: 0,
              stopOnMouseEnter: true,
              stopOnInteraction: false,
            }),
          ]}
        >
          <CarouselContent className="-ml-1">
            {movies.map((m) => (
              <CarouselItem key={m.id} className="md:basis-1/2 lg:basis-1/3">
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
        <section className="mb-12">
          <p className="text-muted-foreground mb-4 text-lg">
            <span className="font-heading text-secondary text-xl font-bold italic">
              Everyone's a Critic
            </span>{' '}
            – Share Your Voice! CineSlate is powered by movie lovers like you!
            No paid critics, no professional reviewers—just real opinions from
            real people.
          </p>
          <ul className="text-muted-foreground ml-6 list-disc space-y-2">
            <li>
              <span className="text-foreground font-bold">Write Reviews:</span>{' '}
              Share your thoughts on any movie.
            </li>
            <li>
              <span className="text-foreground font-bold">Rate & Comment:</span>{' '}
              Engage in discussions and see what others think.
            </li>
            <li>
              <span className="text-foreground font-bold">
                Follow Users & Critics:
              </span>{' '}
              Keep up with your favorite reviewers and recommendations.
            </li>
          </ul>
          <Carousel className="w-full">
            <CarouselPrevious />
            <CarouselNext />
            <CarouselContent className="-ml-1">
              {reviews.map((r, index) => (
                <CarouselItem key={index} className="md:basis-1/2 lg:basis-1/3">
                  <ReviewCard review={r} />
                </CarouselItem>
              ))}
            </CarouselContent>
          </Carousel>
        </section>
      </div>
    </div>
  );
}

export default Home;

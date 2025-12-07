import { useUserStore } from '@/store/userStore';
import { Link } from '@tanstack/react-router';

function Home() {
  const user = useUserStore((state) => state.user);

  return (
    <div className="flex flex-col items-center justify-center rounded-xl p-8">
      <div className="max-w-4xl">
        <h1 className="font-heading text-primary mb-12 text-center text-3xl font-bold md:text-4xl">
          Find Your Next Favorite Movie Instantly! 🎬🍿
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
        </section>

        <section className="mb-12">
          <p className="text-muted-foreground mb-4 text-lg">
            <span className="font-heading text-secondary text-xl font-bold italic">
              Community-Driven
            </span>{' '}
            – The best reviews rise to the top, shaped by the CineSlate
            community.
          </p>
          <ul className="text-muted-foreground ml-6 list-disc space-y-2">
            <li>
              <span className="text-foreground font-bold">
                Create &amp; Share
              </span>{' '}
              Watchlists of your favorite films.
            </li>
            <li>
              <span className="text-foreground font-bold">Stay Updated</span>{' '}
              with notifications on upcoming releases and discussions.
            </li>
          </ul>
          <p className="text-muted-foreground mt-4 text-lg">
            Start exploring, reviewing, and discussing your next favorite film
            today!{' '}
            <span className="text-foreground font-bold">
              <Link
                to={user?.username?.length > 0 ? '/my-details' : '/login'}
                activeProps={{
                  className: 'outline  outline-foreground',
                }}
                className={
                  'mx-2 rounded px-2 py-1 text-foreground hover:bg-primary active:bg-opacity-80'
                }
              >
                Sign up
              </Link>
              now and join the CineSlate community!
            </span>
          </p>
        </section>
      </div>
    </div>
  );
}

export default Home;

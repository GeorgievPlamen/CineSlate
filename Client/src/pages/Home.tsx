import { NavLink } from 'react-router-dom';
import { useAppSelector } from '../app/store/reduxHooks';

function Home() {
  const user = useAppSelector((state) => state.users.user);

  return (
    <div className="flex flex-col items-center justify-center rounded-xl p-8">
      <div className="max-w-4xl">
        <h1 className="font-arvo text-primary mb-12 text-center text-3xl font-bold md:text-4xl">
          Find Your Next Favorite Movie Instantly! 🎬🍿
        </h1>

        <section className="mb-12">
          <p className="text-grey mb-4 text-lg">
            <span className="font-arvo text-secondary text-xl font-bold italic">
              Welcome to CineSlate
            </span>{' '}
            , the fastest and easiest way to discover great movies to watch!
            Whether you’re in the mood for an action-packed thriller, a
            heartwarming drama, or a mind-bending sci-fi epic, CineSlate helps
            you find the perfect film in seconds.
          </p>
          <ul className="text-grey ml-6 list-disc space-y-2">
            <li>
              <span className="text-whitesmoke font-bold">
                Effortless Movie Discovery:
              </span>{' '}
              Instantly find any movie by title.
            </li>
            <li>
              <span className="text-whitesmoke font-bold">Trending Now:</span>{' '}
              See the most popular movies everyone’s watching.
            </li>
            <li>
              <span className="text-whitesmoke font-bold">
                Filter by Genre:
              </span>{' '}
              Find exactly what you're in the mood for.
            </li>
            <li>
              <span className="text-whitesmoke font-bold">
                Fast & Responsive:
              </span>{' '}
              No waiting, no clutter—just movies!
            </li>
          </ul>
        </section>

        <section className="mb-12">
          <p className="text-grey mb-4 text-lg">
            <span className="font-arvo text-secondary text-xl font-bold italic">
              Everyone's a Critic
            </span>{' '}
            – Share Your Voice! CineSlate is powered by movie lovers like you!
            No paid critics, no professional reviewers—just real opinions from
            real people.
          </p>
          <ul className="text-grey ml-6 list-disc space-y-2">
            <li>
              <span className="text-whitesmoke font-bold">Write Reviews:</span>{' '}
              Share your thoughts on any movie.
            </li>
            <li>
              <span className="text-whitesmoke font-bold">Rate & Comment:</span>{' '}
              Engage in discussions and see what others think.
            </li>
            <li>
              <span className="text-whitesmoke font-bold">
                Follow Users & Critics:
              </span>{' '}
              Keep up with your favorite reviewers and recommendations.
            </li>
          </ul>
        </section>

        <section className="mb-12">
          <p className="text-grey mb-4 text-lg">
            <span className="font-arvo text-secondary text-xl font-bold italic">
              Community-Driven
            </span>{' '}
            – The best reviews rise to the top, shaped by the CineSlate
            community.
          </p>
          <ul className="text-grey ml-6 list-disc space-y-2">
            <li>
              <span className="text-whitesmoke font-bold">
                Create &amp; Share
              </span>{' '}
              Watchlists of your favorite films.
            </li>
            <li>
              <span className="text-whitesmoke font-bold">Stay Updated</span>{' '}
              with notifications on upcoming releases and discussions.
            </li>
          </ul>
          <p className="text-grey mt-4 text-lg">
            Start exploring, reviewing, and discussing your next favorite film
            today!{' '}
            <span className="text-whitesmoke font-bold">
              <NavLink
                to={user?.username?.length > 0 ? '/my-details' : 'login'}
                className={({ isActive }) =>
                  'text-primary hover:bg-primary hover:text-whitesmoke active:bg-opacity-80 rounded p-1 underline' +
                  ` ${isActive ? 'outline-whitesmoke outline outline-1' : null}`
                }
              >
                Sign up
              </NavLink>
              now and join the CineSlate community!
            </span>
          </p>
        </section>
      </div>
    </div>
  );
}

export default Home;

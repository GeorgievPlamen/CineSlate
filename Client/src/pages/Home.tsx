import { NavLink } from 'react-router-dom';

function Home() {
  return (
    <div className="flex flex-col items-center justify-center rounded-xl p-8">
      <div className="max-w-4xl">
        <h1 className="mb-6 text-center font-arvo text-3xl font-bold text-primary md:text-4xl">
          Find Your Next Favorite Movie Instantly! 🎬🍿
        </h1>

        <section className="mb-8">
          <p className="mb-4 text-lg text-grey">
            <span className="font-arvo text-xl font-bold italic text-secondary">
              Welcome to CineSlate
            </span>{' '}
            , the fastest and easiest way to discover great movies to watch!
            Whether you’re in the mood for an action-packed thriller, a
            heartwarming drama, or a mind-bending sci-fi epic, CineSlate helps
            you find the perfect film in seconds.
          </p>
          <ul className="ml-6 list-disc space-y-2 text-grey">
            <li>
              <span className="font-bold text-whitesmoke">
                Effortless Movie Discovery:
              </span>{' '}
              Instantly find any movie by title.
            </li>
            <li>
              <span className="font-bold text-whitesmoke">Trending Now:</span>{' '}
              See the most popular movies everyone’s watching.
            </li>
            <li>
              <span className="font-bold text-whitesmoke">
                Filter by Genre & Year:
              </span>{' '}
              Find exactly what you're in the mood for.
            </li>
            <li>
              <span className="font-bold text-whitesmoke">
                Fast & Responsive:
              </span>{' '}
              No waiting, no clutter—just movies!
            </li>
          </ul>
        </section>

        <section className="mb-8">
          <p className="mb-4 text-lg text-grey">
            <span className="font-arvo text-xl font-bold italic text-secondary">
              Everyone's a Critic
            </span>{' '}
            – Share Your Voice! CineSlate is powered by movie lovers like you!
            No paid critics, no professional reviewers—just real opinions from
            real people.
          </p>
          <ul className="ml-6 list-disc space-y-2 text-grey">
            <li>
              <span className="font-bold text-whitesmoke">Write Reviews:</span>{' '}
              Share your thoughts on any movie.
            </li>
            <li>
              <span className="font-bold text-whitesmoke">Rate & Comment:</span>{' '}
              Engage in discussions and see what others think.
            </li>
            <li>
              <span className="font-bold text-whitesmoke">
                Follow Users & Critics:
              </span>{' '}
              Keep up with your favorite reviewers and recommendations.
            </li>
          </ul>
        </section>

        <section className="mb-8">
          <p className="mb-4 text-lg text-grey">
            <span className="font-arvo text-xl font-bold italic text-secondary">
              Community-Driven
            </span>{' '}
            – The best reviews rise to the top, shaped by the CineSlate
            community.
          </p>
          <ul className="ml-6 list-disc space-y-2 text-grey">
            <li>
              <span className="font-bold text-whitesmoke">
                Create &amp; Share
              </span>{' '}
              Watchlists of your favorite films.
            </li>
            <li>
              <span className="font-bold text-whitesmoke">Stay Updated</span>{' '}
              with notifications on upcoming releases and discussions.
            </li>
          </ul>
          <p className="mt-4 text-lg text-grey">
            Start exploring, reviewing, and discussing your next favorite film
            today!{' '}
            <span className="font-bold text-whitesmoke">
              <NavLink
                to="login"
                className={({ isActive }) =>
                  'rounded p-1 text-whitesmoke underline hover:bg-primary active:bg-opacity-80' +
                  ` ${isActive ? 'outline outline-1 outline-whitesmoke' : null}`
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

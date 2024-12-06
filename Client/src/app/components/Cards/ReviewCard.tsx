import { Review } from '../../../pages/Movies/models/movieType';

interface Props {
  r: Review;
}

export default function ReviewCard({ r }: Props) {
  return (
    <div className="flex rounded-2xl border border-grey bg-background p-1">
      <img
        src="https://freesvg.org/img/abstract-user-flat-3.png"
        alt="profile-pic"
        className="h-20 w-20"
      />
      <div className="mx-4 my-2 w-80">
        <div className="mb-2 flex justify-between">
          <p className="text-xl">Username placeholder</p>
          <p>⭐{r.rating}</p>
        </div>
        <p className="font-roboto">
          {r.text} Lorem ipsum dolor sit, amet consectetur adipisicing elit.
          Autem, quas odio reiciendis sunt numquam assumenda architecto
          distinctio a vero. Ad laborum animi earum quasi non iste? Corporis
          asperiores maxime nihil!
        </p>
      </div>
    </div>
  );
}

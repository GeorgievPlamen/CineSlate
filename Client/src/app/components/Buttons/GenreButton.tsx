interface Props {
  name: string;
}

export default function GenreButton({ name }: Props) {
  return (
    <button className="m-2 h-10 rounded-full border bg-background px-4 py-1 hover:outline hover:outline-1 active:bg-opacity-80">
      {name}
    </button>
  );
}

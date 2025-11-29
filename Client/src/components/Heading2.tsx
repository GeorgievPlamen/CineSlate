interface Props {
  readonly title: string;
}

function Heading2({ title }: Props) {
  return <h2 className="font-arvo text-2xl font-bold">{title}</h2>;
}
export default Heading2;

import { IMG_PATH } from '../../config';

interface Props {
  path?: string;
}

export default function Backdrop({ path }: Props) {
  return (
    <div className="absolute -z-40">
      <img
        src={`${IMG_PATH}${path}`}
        alt="backdrop"
        style={{ backgroundSize: ' ' }}
        className="opacity-50"
      />
      <div className="bg-gradient-radial 6 absolute top-0 h-full w-full from-dark/60 via-dark/90 to-dark"></div>
    </div>
  );
}

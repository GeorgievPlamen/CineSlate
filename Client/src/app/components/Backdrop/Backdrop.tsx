import { useState } from 'react';
import { IMG_PATH } from '../../config';

interface Props {
  path?: string;
}

export default function Backdrop({ path }: Props) {
  const [loaded, setLoaded] = useState(false);

  console.log(loaded);

  return (
    <div className="absolute -z-40">
      <img
        src={`${IMG_PATH}${path}`}
        alt="backdrop"
        style={{ backgroundSize: ' ' }}
        className={
          'transition-opacity duration-500 ' +
          (loaded ? 'opacity-50' : 'opacity-0')
        }
        onLoad={() => setLoaded(true)}
      />
      <div className="6 absolute top-0 h-full w-full bg-gradient-radial from-dark/60 via-dark/90 to-dark"></div>
    </div>
  );
}

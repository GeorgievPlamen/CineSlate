import { useState } from 'react';
import { IMG_PATH } from '@/config';

interface Props {
  path?: string;
}

export default function Backdrop({ path }: Props) {
  const [loaded, setLoaded] = useState(false);

  return (
    <div className="absolute -z-40 right-0">
      <img
        src={`${IMG_PATH}${path}`}
        alt="backdrop"
        style={{ backgroundSize: ' ' }}
        className={
          'transition-opacity duration-500 object-contain ' +
          (loaded ? 'opacity-65' : 'opacity-0')
        }
        onLoad={() => setLoaded(true)}
      />
      <div className="absolute top-0 h-full w-full bg-muted-background/20" />
      <div className="absolute bottom-0 h-full w-2/3 bg-linear-to-r from-muted-background via-muted-background/75 to-transparent" />
      <div className="absolute bottom-0 h-2/3 w-full bg-linear-to-t from-muted-background via-muted-background/75 to-transparent" />
    </div>
  );
}

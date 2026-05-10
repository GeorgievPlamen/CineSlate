import Dropdown from '@/components/Dropdown';
import { cn } from '@/lib/utils';
import { ChevronUp, ChevronDown } from 'lucide-react';
import { MoviesBy, MoviesByTitleMap } from '../api/moviesClient';
import { ReactNode, useState } from 'react';

interface Props {
  items: ReactNode[];
  moviesBy: MoviesBy;
}
function SortByDropdown({ items, moviesBy }: Props) {
  const [isSortByMenuOpened, setIsSortByMenuOpened] = useState(false);

  return (
    <div className="w-full">
      <Dropdown
        items={items}
        classNameTrigger="w-full"
        classNameMenu="w-32"
        onOpen={(open) => setIsSortByMenuOpened(open)}
      >
        <div
          className={cn(
            'flex items-center h-8 rounded-lg px-2 bg-muted justify-between cursor-pointer hover:bg-primary'
          )}
        >
          <p className="w-24 text-sm">{MoviesByTitleMap[moviesBy]}</p>
          {isSortByMenuOpened ? <ChevronUp /> : <ChevronDown />}
        </div>
      </Dropdown>
    </div>
  );
}

export default SortByDropdown;

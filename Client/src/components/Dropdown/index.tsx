import { ReactNode } from 'react';
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '../ui/dropdown-menu';
import { cn } from '@/lib/utils';
import Button from '../Buttons/Button';

interface Props {
  children: ReactNode;
  menuLabel?: ReactNode;
  items?: ReactNode[];
  classNameTrigger?: string;
  classNameMenu?: string;
  onOpen?: (open: boolean) => void;
  onLoadMore?: () => void;
}

export default function Dropdown({
  children,
  menuLabel,
  items,
  classNameTrigger,
  classNameMenu,
  onOpen,
  onLoadMore,
}: Props) {
  return (
    <DropdownMenu onOpenChange={onOpen}>
      <DropdownMenuTrigger
        className={cn('focus:outline-none', classNameTrigger)}
      >
        {children}
      </DropdownMenuTrigger>
      <DropdownMenuContent className={cn(classNameMenu)}>
        {menuLabel && (
          <>
            <DropdownMenuLabel>{menuLabel}</DropdownMenuLabel>
            <DropdownMenuSeparator />
          </>
        )}
        {items?.map((i, index) => (
          <DropdownMenuItem key={index}>{i}</DropdownMenuItem>
        ))}
        {onLoadMore && (
          <Button onClick={onLoadMore} className="w-full mt-2">
            Load More
          </Button>
        )}
      </DropdownMenuContent>
    </DropdownMenu>
  );
}

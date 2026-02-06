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
  classNameMenuItem?: string;
  triggerDisabled?: boolean;
  isFetching?: boolean;
  onOpen?: (open: boolean) => void;
  onLoadMore?: () => void;
}

export default function Dropdown({
  children,
  menuLabel,
  items,
  classNameTrigger,
  classNameMenu,
  classNameMenuItem,
  triggerDisabled,
  isFetching,
  onOpen,
  onLoadMore,
}: Props) {
  return (
    <DropdownMenu onOpenChange={onOpen}>
      <DropdownMenuTrigger
        className={cn('focus:outline-none', classNameTrigger)}
        disabled={triggerDisabled}
      >
        {children}
      </DropdownMenuTrigger>
      <DropdownMenuContent className={cn(classNameMenu, 'scrollbar')}>
        {menuLabel && (
          <>
            <DropdownMenuLabel>{menuLabel}</DropdownMenuLabel>
            <DropdownMenuSeparator />
          </>
        )}
        {items?.map((i, index) => (
          <DropdownMenuItem key={index} className={cn(classNameMenuItem)}>
            {i}
          </DropdownMenuItem>
        ))}
        {onLoadMore && (
          <Button
            onClick={onLoadMore}
            className="w-full mt-2"
            isLoading={isFetching}
          >
            Load More
          </Button>
        )}
      </DropdownMenuContent>
    </DropdownMenu>
  );
}

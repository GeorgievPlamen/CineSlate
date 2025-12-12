import { ReactNode } from 'react';
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '../ui/dropdown-menu';

interface Props {
  children: ReactNode;
  menuLabel?: ReactNode;
  items: ReactNode[];
}

export default function Dropdown({ children, menuLabel, items }: Props) {
  return (
    <DropdownMenu>
      <DropdownMenuTrigger className="mx-2 rounded px-2 py-1 text-foreground hover:bg-primary">
        {children}
      </DropdownMenuTrigger>
      <DropdownMenuContent>
        {menuLabel && (
          <>
            <DropdownMenuLabel>{menuLabel}</DropdownMenuLabel>
            <DropdownMenuSeparator />
          </>
        )}
        {items &&
          items.map((i, index) => (
            <DropdownMenuItem
              key={index}
            >
              {i}
            </DropdownMenuItem>
          ))}
      </DropdownMenuContent>
    </DropdownMenu>
  );
}

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
      <DropdownMenuTrigger className="focus:outline-none">
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
            <DropdownMenuItem key={index}>{i}</DropdownMenuItem>
          ))}
      </DropdownMenuContent>
    </DropdownMenu>
  );
}

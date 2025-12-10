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
  label: ReactNode;
  menuLabel?: ReactNode;
  items: ReactNode[];
}

export default function Dropdown({ label, menuLabel, items }: Props) {
  return (
    <DropdownMenu>
      <DropdownMenuTrigger>{label}</DropdownMenuTrigger>
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

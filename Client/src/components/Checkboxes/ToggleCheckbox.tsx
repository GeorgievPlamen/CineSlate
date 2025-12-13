import { useState } from 'react';
import { FieldValues, UseFormRegister } from 'react-hook-form';

interface Props {
  name: string;
  bgClassName?: string;
  className?: string;
  isChecked?: boolean;

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  register: UseFormRegister<FieldValues | any>;
}

export default function ToggleCheckbox({
  name,
  className,
  bgClassName,
  isChecked,
  register,
}: Props) {
  const [checked, setChecked] = useState(isChecked);

  return (
    <div
      className={
        bgClassName +
        ` relative h-5 w-10 rounded-full ${checked ? 'bg-primary' : 'bg-muted'}`
      }
    >
      <input
        type="checkbox"
        className={
          className +
          ' appearance-none absolute left-0.5 top-0.5 h-4 w-4 rounded-full border border-foreground bg-foreground transition-all checked:left-[22px] checked:border checked:border-foreground hover:checked:border-foreground focus:ring-0 focus:ring-offset-0 focus:checked:border-foreground'
        }
        {...register(name)}
        onChange={() => setChecked(!checked)}
      />
    </div>
  );
}

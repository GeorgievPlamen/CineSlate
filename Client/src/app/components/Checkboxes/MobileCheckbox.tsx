import { useState } from 'react';
import { FieldValues, UseFormRegister } from 'react-hook-form';

interface Props {
  name: string;
  bgClassName?: string;
  className?: string;

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  register: UseFormRegister<FieldValues | any>;
}

export default function MobileCheckbox({
  name,
  className,
  bgClassName,
  register,
}: Props) {
  const [checked, setChecked] = useState(false);

  return (
    <div
      className={
        bgClassName +
        ` relative h-5 w-10 rounded-full ${checked ? 'bg-primary' : 'bg-grey'}`
      }
    >
      <input
        type="checkbox"
        className={
          className +
          ' absolute left-0.5 top-0.5 h-4 w-4 rounded-full border border-whitesmoke bg-whitesmoke transition-all checked:left-[22px] checked:border checked:border-whitesmoke checked:bg-primary hover:checked:border-whitesmoke hover:checked:bg-primary focus:ring-0 focus:ring-offset-0 focus:checked:border-whitesmoke focus:checked:bg-primary'
        }
        {...register(name)}
        onChange={() => setChecked(!checked)}
      />
    </div>
  );
}

interface Props {
  name: string;
  bgClassName?: string;
  className?: string;
  checked: boolean;
  setChecked: React.Dispatch<React.SetStateAction<boolean>>;
}

export default function MobileCheckbox({
  name,
  className,
  bgClassName,
  checked,
  setChecked,
}: Props) {
  return (
    <div
      className={
        bgClassName +
        ` relative h-5 w-10 rounded-full ${checked ? 'bg-primary' : 'bg-grey'}`
      }
    >
      <input
        type="checkbox"
        name={name}
        className={
          className +
          ' absolute left-0.5 top-0.5 h-4 w-4 rounded-full border border-whitesmoke bg-whitesmoke transition-all checked:left-[22px] checked:border checked:border-whitesmoke checked:bg-primary hover:checked:border-whitesmoke hover:checked:bg-primary focus:ring-0 focus:ring-offset-0 focus:checked:border-whitesmoke focus:checked:bg-primary'
        }
        onChange={() => {
          setChecked(!checked);
        }}
      />
    </div>
  );
}

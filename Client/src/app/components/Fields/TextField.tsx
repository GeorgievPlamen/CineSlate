import { UseFormRegisterReturn } from 'react-hook-form';

interface Props extends React.InputHTMLAttributes<HTMLInputElement> {
  register?: UseFormRegisterReturn;
  className?: string;
}

function TextField({ register, className, ...rest }: Props) {
  return (
    <input
      type="text"
      className={
        'h-8 rounded-md bg-dark px-2 focus:outline-none' + ' ' + className
      }
      {...register}
      {...rest}
    />
  );
}
export default TextField;

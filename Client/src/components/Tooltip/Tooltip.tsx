import { ReactNode } from 'react';

interface Props {
  children: ReactNode;
  content?: string;
}

function Tooltip({ children, content }: Props) {
  return (
    <div className="group relative">
      {children}
      <div className="absolute bottom-full left-1/2 z-10 mb-2 hidden -translate-x-1/2 transform rounded bg-gray-800 px-2 py-1 text-xs text-white group-hover:block">
        {content}
      </div>
    </div>
  );
}
export default Tooltip;

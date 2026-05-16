import {
  Link,
  RegisteredRouter,
  ValidateLinkOptions,
} from '@tanstack/react-router';

type HeaderLinkProps<
  TRouter extends RegisteredRouter = RegisteredRouter,
  TOptions = unknown,
> = {
  label: string;
  linkOptions: ValidateLinkOptions<TRouter, TOptions>;
};

function HeaderLink({ label, linkOptions }: HeaderLinkProps) {
  return (
    <Link
      {...linkOptions}
      activeProps={{
        className: 'border-b border-secondary',
      }}
      className={'h-full flex items-center cursor-default'}
    >
      <span className="text-sm lg:text-base rounded px-2 lg:px-4 py-1 hover:bg-primary active:bg-opacity-80 cursor-pointer font-semibold">
        {label}
      </span>
    </Link>
  );
}

export default HeaderLink;

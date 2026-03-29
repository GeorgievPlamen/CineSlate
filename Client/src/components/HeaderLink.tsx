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
      <span className="rounded px-2 py-1 hover:bg-primary active:bg-opacity-80 cursor-pointer">
        {label}
      </span>
    </Link>
  );
}

export default HeaderLink;

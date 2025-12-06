import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/my-details/')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/my-details/"!</div>
}

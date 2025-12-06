import MyDetails from '@/features/MyDetails/MyDetails'
import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/my-details/')({
  component: RouteComponent,
})

function RouteComponent() {
  return <MyDetails />
}

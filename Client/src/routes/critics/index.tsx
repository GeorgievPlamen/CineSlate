import Critics from '@/modules/Critics'
import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/critics/')({
  component: RouteComponent,
})

function RouteComponent() {
  return <Critics />
}

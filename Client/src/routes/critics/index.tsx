import Critics from '@/features/Critics/Critics'
import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/critics/')({
  component: RouteComponent,
})

function RouteComponent() {
  return <Critics />
}

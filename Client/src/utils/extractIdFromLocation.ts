export default function extractIdFromLocation(location: string) {
  return location.split('=')[1];
}

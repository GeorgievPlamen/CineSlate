import { useAppSelector } from '../../app/store/reduxHooks';

function MyDetails() {
  const user = useAppSelector((store) => store.users.user);
  console.log(user.id);
  return <div>MyDetails</div>;
}
export default MyDetails;

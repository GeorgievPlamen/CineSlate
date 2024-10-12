import Footer from './Footer';
import Header from './Header';
import Main from './Main';

function Layout() {
  return (
    <div className="flex min-h-screen flex-col">
      <Header />
      <Main />
      <Footer />
    </div>
  );
}
export default Layout;

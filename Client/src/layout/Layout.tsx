import Footer from './Footer';
import Header from './Header';
import Main from './Main';

function Layout() {
  return (
    <div className="h-screen bg-white text-black dark:bg-slate-900 dark:text-white">
      <Header />
      <Main />
      <Footer />
    </div>
  );
}
export default Layout;

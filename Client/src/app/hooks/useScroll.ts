import { useState, useEffect } from 'react';

export default function useScroll() {
  const [nearBottom, setNearBottom] = useState(false);

  useEffect(() => {
    function handleScroll() {
      const scrollHeight = document.body.scrollHeight;
      const scrollPosition = window.scrollY;
      const windowPosition = window.innerHeight;
      const offset = 100;

      if (scrollPosition + windowPosition + offset > scrollHeight) {
        setNearBottom(true);
      } else {
        setNearBottom(false);
      }
    }

    window.addEventListener('scroll', handleScroll);

    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, []);

  return { nearBottom };
}

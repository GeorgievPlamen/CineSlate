import { useState, useEffect } from 'react';

export default function useScroll() {
  const [nearBottom, setNearBottom] = useState(false);
  const [beyondScreen, setBeyondScreen] = useState(false);

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

      if (scrollPosition + 300 > windowPosition) {
        setBeyondScreen(true);
      } else {
        setBeyondScreen(false);
      }
    }

    window.addEventListener('scroll', handleScroll);

    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, []);

  return { nearBottom, beyondScreen };
}

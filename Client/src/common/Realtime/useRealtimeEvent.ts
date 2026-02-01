import { useEffect, useEffectEvent } from 'react';
import { RealtimeEvents } from './constants';
import realtimeClient from './realtimeClient';

export default function useRealtimeEvent(
  event: RealtimeEvents,
  cb: (args?: unknown) => void
) {
  const onEvent = useEffectEvent(cb);

  useEffect(() => {
    realtimeClient.on(event, onEvent);

    return () => {
      realtimeClient.off(event, onEvent);
    };
  }, [event]);
}

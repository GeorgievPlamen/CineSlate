import { CINESLATE_API_URL } from '@/config';
import { useUserStore } from '@/store/userStore';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from '@microsoft/signalr';
import logger from '../logger';

const getToken = () => `${useUserStore.getState()?.user?.token}`;

let connection: HubConnection | null;

const realtimeClient = {
  init: function () {
    if (connection) {
      logger.log('There is already an existing connection.');
      return;
    }

    const token = getToken();

    if (!token) {
      logger.log('Missing user token, will not initialize signal R.');
      return;
    }

    connection = new HubConnectionBuilder()
      .withUrl(
        (import.meta.env.VITE_CINESLATE_API_URL ?? CINESLATE_API_URL) +
          'notifications',
        {
          accessTokenFactory: getToken,
        }
      )
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();
  },
  start: async function () {
    try {
      if (connection?.connectionId) {
        logger.log(
          `Already active connection with id: ${connection.connectionId}`
        );
        return;
      }

      await connection?.start();
      logger.log('SignalR Connected.');
    } catch (err) {
      logger.log(err);
    }
  },

  stop: async function () {
    await connection?.stop();
    connection = null;
  },

  on: function (handler: string, callback: (args: unknown) => void) {
    if (!connection) {
      logger.log("No connection, can't register a handler!");
      return;
    }

    connection?.on(handler, callback);
  },

  off: function (handler: string, callback: (args: unknown) => void) {
    connection?.off(handler, callback);
  },
};

export default realtimeClient;

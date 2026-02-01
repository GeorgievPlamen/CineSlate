import { useUserStore } from '@/store/userStore';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from '@microsoft/signalr';

const getToken = () => `${useUserStore.getState()?.user?.token}`;

let connection: HubConnection | null;

const realtimeClient = {
  init: function () {
    if (connection) {
      console.log('There is already an existing connection.');
      return;
    }

    const token = getToken();

    if (!token) {
      console.log('Missing user token, will not initialize signal R.');
      return;
    }

    connection = new HubConnectionBuilder()
      .withUrl('http://localhost:8080/notifications', {
        accessTokenFactory: getToken,
      })
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();
  },
  start: async function () {
    try {
      if (connection?.connectionId) {
        console.log(
          `Already active connection with id: ${connection.connectionId}`
        );
        return;
      }

      await connection?.start();
      console.log('SignalR Connected.');
    } catch (err) {
      console.log(err);
    }
  },

  stop: async function () {
    await connection?.stop();
    connection = null;
  },

  on: function (handler: string, callback: (args: unknown) => void) {
    if (!connection) {
      throw new Error("No connection, can't register a handler!");
    }

    connection?.on(handler, callback);
  },

  off: function (handler: string, callback: (args: unknown) => void) {
    connection?.off(handler, callback);
  },
};

export default realtimeClient;

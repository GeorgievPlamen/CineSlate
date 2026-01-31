import { useUserStore } from '@/store/userStore';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from '@microsoft/signalr';

const getToken = () => `${useUserStore.getState()?.user?.token}`;

let connection: HubConnection | null;

const signalR = {
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
      .withUrl('http://localhost:5000/notifications', {
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
    connection?.on(handler, callback);
  },
};

export default signalR;

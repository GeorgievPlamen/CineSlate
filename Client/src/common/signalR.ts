import { useUserStore } from '@/store/userStore';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from '@microsoft/signalr';

const getToken = () => `${useUserStore.getState()?.user?.token}`;

let connection: HubConnection;

const signalR = {
  init: function () {
    const token = getToken();

    console.log(token);

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
      await connection.start();
      console.log('SignalR Connected.');
    } catch (err) {
      console.log(err);
    }
  },

  stop: async function () {
    await connection.stop();
  },

  on: async function (handler: string, callback: (args: unknown) => void) {
    connection.on(handler, callback);
  },
};

export default signalR;

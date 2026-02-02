const isEnabled = import.meta.env.VITE_ENABLE_DEBUG_LOGS;

const logger = {
  log: (args: unknown) => (isEnabled ? console.log(args) : {}),
  error: (args: unknown) => (isEnabled ? console.error(args) : {}),
};

export default logger;

const amqplib = require("amqplib");

async function createConnection() {
  try {
    const connectionString = "amqp://USERNAME:PASSWORD@localhost:5672/";
    const connection = await amqplib.connect(connectionString);
    return connection;
  } catch (error) {
    console.error(error);
    return null;
  }
}

module.exports = createConnection;

const amqplib = require("amqplib");

let connection;
let channel;

async function createConnection() {
  if (connection && channel) {
    return { connection, channel };
  }
  try {
    const connectionString = "amqp://USERNAME:PASSWORD@localhost:5672/";
    connection = await amqplib.connect(connectionString);
    channel = await connection.createChannel();
    return { connection, channel };
  } catch (error) {
    console.error(error);
    return { connection: null, channel: null };
  }
}

module.exports = createConnection;

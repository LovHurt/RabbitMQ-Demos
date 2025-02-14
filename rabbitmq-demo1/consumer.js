const createConnection = require("./rabbitMqConnection.js");

const consumer = async (queueName) => {
  const { connection, channel } = await createConnection();

  if (!connection || !channel) {
    await createConnection();
  }

  await channel.assertQueue(queueName, { durable: true });

  channel.prefetch(52); //limit of messages to be consumed
  await channel.consume(queueName, (msg) => {
    const data = JSON.parse(msg.content.toString());

    console.log("Data from RabbitMQ : ", data);

    channel.ack(msg);
  });
};

//our consumer listen this queue
consumer("queue-1");

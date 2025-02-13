const createConnection = require("./rabbitMqConnection.js");

const consumer = async (queueName) => {
  const connection = await createConnection();
  const channel = await connection.createChannel();

  await channel.assertQueue(queueName, { durable: true });

  await channel.consume(queueName, (msg) => {
    const data = JSON.parse(msg.content.toString());

    console.log("Data from RabbitMQ : ", data);

    channel.ack(msg);
  });
};

//our consumer listen this queue
consumer("queue-1");

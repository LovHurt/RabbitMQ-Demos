const express = require("express");
const app = express();
const createConnection = require("./rabbitMqConnection.js");

require("./consumer.js");

app.use(express.json());
app.use(express.urlencoded({ extended: true }));

const sendMessage = async (queueName, message) => {

  const {connection, channel} = await createConnection();

  if (!connection || !channel) {
    await createConnection();
  }

  //durable true means its on the disk not ram
  await channel.assertQueue(queueName, { durable: true });

  try {
    channel.sendToQueue(queueName, Buffer.from(message), { persistent: true });
    console.log("Message sent to queue:", queueName);
  } catch (error) {
    console.log("An error occurred while sending the message:", error);
    throw error;
  }
  console.log("message sent", message);

  return true;
};

app.post("/api/send-message", async (req, res) => {
  const { queue, message } = req.body;

  const data = { queue, message, date: new Date() };

  await sendMessage(queue, JSON.stringify(data));

  res.status(200).json({ data });
});

const port = 3005;

app.listen(port, () => console.log("server started on port :", port));

//asd

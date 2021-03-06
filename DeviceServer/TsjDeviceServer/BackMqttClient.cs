﻿using Common;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TsjDeviceServer
{
    public class BackMqttClient
    {
        static IMqttClient _client;
        bool _bConnect = true;

        public async Task Run(IMqttApplicationMessageReceivedHandler recverHandler)
        {
            try
            {
                var factory = new MqttFactory();
                _client = factory.CreateMqttClient();
#if DEBUG
                var ip = "47.98.179.238";
#else
                var ip = Config.Configuration["MQTT:ServerIP"];
#endif
                var port = int.Parse(Config.Configuration["MQTT:ServerPort"]);
                var user = Config.Configuration["MQTT:Username"];
                var pwd = Config.Configuration["MQTT:Password"];
                Console.WriteLine($"ip{ip} port{port} user{user} pwd{pwd}");
                var clientOptions = new MqttClientOptionsBuilder()
                    .WithClientId("back1"/*Guid.NewGuid().ToString().Substring(0, 5)*/)
                    .WithTcpServer(ip, port)
                    .WithCredentials(user, pwd)
                    //.WithTls()
                    //.WithCleanSession()
                    .Build();
                _client.ApplicationMessageReceivedHandler = recverHandler;
                _client.ConnectedHandler = new MqttClientConnectedHandlerDelegate(async e =>
                {
                    Console.WriteLine("### CONNECTED WITH SERVER ###");

                    //await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic("#").Build());
                    foreach(var topic in TsjTopics.BackSubscribeTopics)
                    {
                        await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic).Build());
                    }

                    Console.WriteLine("### SUBSCRIBED ###");
                    //CheckUpdateFirmware checkUpdateFirmware = new CheckUpdateFirmware();
                    //checkUpdateFirmware.Start();
                });
                _client.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(async e =>
                {
                    if (!_bConnect)
                        return;
                    Console.WriteLine("### DISCONNECTED FROM SERVER ###");
                    await Task.Delay(TimeSpan.FromSeconds(5));

                    try
                    {
                        await _client.ConnectAsync(clientOptions);
                    }
                    catch
                    {
                        Console.WriteLine("### RECONNECTING FAILED ###");
                    }
                });
                try
                {
                    await _client.ConnectAsync(clientOptions);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("### CONNECTING FAILED ###" + Environment.NewLine + exception);
                }
                Console.WriteLine("### CONNECTING SUCCESS ###");
            }catch(Exception exc)
            {
                Console.WriteLine(exc);
            }
            //while (!Console.ReadLine().Equals("exit")) ;
        }
        public void Stop()
        {
            _bConnect = false;
            _client.DisconnectAsync();
        }
        static public Task Send(MqttApplicationMessage msg)
        {
            if(_client!=null)
                return _client.PublishAsync(msg);
            return Task.FromException(new Exception("backMqttClient is null"));
        }
    }
}

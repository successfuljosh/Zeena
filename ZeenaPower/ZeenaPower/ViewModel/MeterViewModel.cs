using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeenaPower.Models;
using ZeenaPower.Services;

namespace ZeenaPower.ViewModel
{
   public class MeterViewModel:INotifyPropertyChanged
    {
        ClientWebSocket client;
        CancellationTokenSource cts = new CancellationTokenSource();

        Restful WebService;
        private string token;
        private string currentMeterId;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        measurement _meter;
        public measurement Meter
        {
            get
            {
                return _meter;
            }
            set
            {
                _meter = value;
                OnPropertyChanged();
            }
        }
        public MeterViewModel(string meterId, string tokenvalue)
        {
            currentMeterId = meterId;
            WebService = new Restful();
            token = tokenvalue;
            client = new ClientWebSocket();
        }
        public void GetSingleMeter()
        { 
            GetSingleAsync();
            GetUsingWebsocket();
        }
        private async void GetSingleAsync()
        {
            var meter = await WebService.GetSingle(token, currentMeterId);
            if (meter != null)
            {
                Meter = meter.measurement;
            }
      
        }
        public async Task SwitchMeter(bool switchData)
        {
            var stat = await WebService.SwitchMeter(token, currentMeterId, switchData);
            //Meter.connectivity = stat;
            //Meter = Meter;
        }
        private async Task GetUsingWebsocket()
        {
            //websocket connection

            Console.WriteLine("Starting Ws");
            await client.ConnectAsync(new Uri("wss://echo.websocket.org"), cts.Token);

            await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await ReadMessage();
                }
            }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

           SendMessageAsync(dummyWsdata());

           
            //using (var ws = new WebSocket("ws://dragonsnest.far/Laputa"))
            //{
            //    ws.OnMessage += (sender, e) =>
            //      Console.WriteLine("Laputa says: " + e.Data);

            //    ws.Connect();
            //    ws.Send("BALUS");
            //    Console.ReadKey(true);
            //}
        }
        public async void closeWs()
        {
            if (client.State == WebSocketState.Open)
            {
                //await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing upon exit", cts.Token);
                client.Dispose();
            }

        }
        string dummyWsdata()
        {
            var du = new measurement
            {
                connectivity = Convert.ToBoolean(new Random().Next(-1,1)),
                cost = new Random().NextDouble(),
                created_at = DateTime.Now,
                current = new Random().NextDouble(),
                current_cost_reading = new Random().NextDouble(),
                frequency = new Random().NextDouble(),
                energy = new Random().NextDouble(),
                power = new Random().NextDouble(),
                meter = new Random().NextDouble(),
                voltage = new Random().NextDouble(),

            };

            var s = JsonConvert.SerializeObject(du);
            return s;
        }
        async Task ReadMessage()
        {
            WebSocketReceiveResult result;
            var message = new ArraySegment<byte>(new byte[4096]);
            do
            {
                result = await client.ReceiveAsync(message, cts.Token);
                if (result.MessageType != WebSocketMessageType.Text)
                    break;
                var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                string receivedMessage = Encoding.UTF8.GetString(messageBytes);
                Console.WriteLine("Received: {0}", receivedMessage);
                Meter = JsonConvert.DeserializeObject<measurement>(receivedMessage);
               // SendMessageAsync(dummyWsdata());
            }
            while (!result.EndOfMessage);
        }

        async void SendMessageAsync(string message)
        {
            //if (!CanSendMessage(message))
            //    return;

            var byteMessage = Encoding.UTF8.GetBytes(message);
            var segmnet = new ArraySegment<byte>(byteMessage);

            await client.SendAsync(segmnet, WebSocketMessageType.Text, true, cts.Token);
        }
    }
}

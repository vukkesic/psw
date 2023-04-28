using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Timers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using IntegrationApi.Protos;
using IntegrationAPI.Protos;
using Microsoft.Extensions.Hosting;
using Channel = Grpc.Core.Channel;

namespace IntegrationAPI
{
    public class ClientGrpcService : IHostedService
    {
        private Channel channel;
        private SpringGrpcService.SpringGrpcServiceClient client;

        public ClientGrpcService() { }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            channel = new Channel("localhost", 8787, ChannelCredentials.Insecure);
            client = new SpringGrpcService.SpringGrpcServiceClient(channel);
            return Task.CompletedTask;
        }

        private async void DonateBlood(object source, ElapsedEventArgs e)
        {
            try
            {
                DateTime StartTime = new DateTime(2023, 05, 20, 8, 00, 0, DateTimeKind.Utc);
                DateTime EndTime = new DateTime(2023, 05, 20, 16, 00, 0, DateTimeKind.Utc);
                BloodDonationAppointment appointment = await client.makeBloodDonationAppointmentAsync(new BloodDonationRequest() { StartTime = Timestamp.FromDateTime(StartTime), EndTime = Timestamp.FromDateTime(EndTime), PatientName = "Stojan", Location = "Kac" });
                Console.WriteLine(appointment.PatientName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Neuspesno");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            channel?.ShutdownAsync();
            return Task.CompletedTask;
        }
    }
}

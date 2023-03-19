using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Sqs.Publisher.WebJob.Models;
using System.Text.Json;

namespace Sqs.Publisher.WebJob
{
    public sealed class EventAWSPublisher
    {
        private static string _url = "customers";
        private static AmazonSQSClient _awsClient = new AmazonSQSClient(RegionEndpoint.USEast1);
        private static CustomerCreatedMessage _customerMessage;
        public async static Task MainAsync() 
        {
            #region Create Message
            _customerMessage = new CustomerCreatedMessage 
            {
                Id = Guid.NewGuid(),    
                Name = "Kaoe Ferreira",
                Email = "batman@dc.com",
                GitHubUserName = "Gondlir"
            };
            #endregion
            Console.WriteLine("Iniciando !");
            var queueUrlReponse = await _awsClient.GetQueueUrlAsync(_url);

            await SendCustomerQueueMessageAsync(_customerMessage, queueUrlReponse);
        }

        #region Private Methods
        private async static Task SendCustomerQueueMessageAsync(CustomerCreatedMessage message, GetQueueUrlResponse queueUrlReponse) 
        {
            try
            {
                Console.WriteLine("Enviando mensagem !");
                var sendMessageRequest = new SendMessageRequest
                {
                    QueueUrl = queueUrlReponse.QueueUrl,
                    MessageBody = JsonSerializer.Serialize(_customerMessage),
                    MessageAttributes = new Dictionary<string, MessageAttributeValue> 
                    {
                        {
                            "MessageType", new MessageAttributeValue
                            {
                                DataType = "String",
                                StringValue = nameof(CustomerCreatedMessage)
                            }
                        }
                    }
                };
                var response = await _awsClient.SendMessageAsync(sendMessageRequest);
                Console.WriteLine("Mensagem enviada!");
            }
            catch (Exception ex)
            {
                // Log Errors
                Console.WriteLine("Houve algum erro !");
                throw;
            }
        }
        #endregion
    }
}

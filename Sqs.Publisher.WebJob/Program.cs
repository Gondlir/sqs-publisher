// See https://aka.ms/new-console-template for more information
using Sqs.Publisher.WebJob;

Console.WriteLine("Enviando mensagem !");
// Log
try
{
    await EventAWSPublisher.MainAsync();

}
catch (Exception ex)
{
    Console.WriteLine("Houve algum erro !");
    throw;
}


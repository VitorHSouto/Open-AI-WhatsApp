using System.Net.Http;
using System.Net;
using VHS_Tarefas.Domain.Messages.CloudAPI;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using System.Text;
using VHS_Tarefas.Enums;

namespace VHS_Tarefas.Gateway
{
    public class CloudAPIGateway : IGateway
    {
        static HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) };
        private const string _tokenAPI = "EAAIYO1vXZB7YBAJMFEeZAC5A2XquhaiAp5rZBee8EeZBMQWry3gqiwQNaimbnu32eB5UH0Cvu8qZCEJM4BHeetrWbz4Yle3rdXmKlM8CZBTnCHZCy9mS6o2h0orPH2cTn0QcDU6KZCZCKuXuEdZBqt2EYr70HWYYxsIvGSnX9bmp0KwZBecNxWbqlnCJlkLxFI9vUpZAfJRFy9TXZAwZDZD";
        private const string _phoneNumberTest = "5534991218085";

        public CloudAPIGateway() { }

        public async Task SendMessage(string messageText)
        {
            var messageLenght = 0;
            var messageCurrentLenght = messageLenght = messageText.Length;
            var sendedChars = 0;
            do
            {
                var startChar = sendedChars;
                sendedChars += (messageCurrentLenght > 4000 ? 4000 : messageCurrentLenght);
                var text = messageText.Substring(startChar, sendedChars - startChar);
                var sendMessage = CreateBaseMessageObject(MessageType.TEXT, _phoneNumberTest, text);
                await SendMessage(sendMessage);
                messageCurrentLenght -= sendedChars - startChar;
            } 
            while(sendedChars < messageLenght);           
        }

        public async Task SendImage(string imageUrl)
        {
            var sendMessage = CreateBaseMessageObject(MessageType.IMAGE, _phoneNumberTest, imageUrl);
            await SendMessage(sendMessage);
        }

        public async Task SendVideo(string videoUrl)
        {
            var sendMessage = CreateBaseMessageObject(MessageType.VIDEO, _phoneNumberTest, videoUrl);
            await SendMessage(sendMessage);
        }

        private CloudAPIOutMessage CreateBaseMessageObject(MessageType messageType, string to, string currentStr)
        {
            var message = new CloudAPIOutMessage();
            message.To = to;
            message.Type = GetCloudAPIMessageType(messageType);

            if(messageType == MessageType.TEXT)
                message.Text = new CloudAPIOutMessageText() { Body=currentStr };
            else if (messageType == MessageType.IMAGE)
                message.Image = new CloudAPIImage() { Link = currentStr };

            return message;
        }

        private string GetCloudAPIMessageType(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.IMAGE: return "image";
                case MessageType.VIDEO: return "video";
                case MessageType.DOCUMENT: return "document";
                case MessageType.INTERACTIVE: return "interactive";
                default: return "text";
            }
        }

        private async Task<string> SendMessage(CloudAPIOutMessage dialogMessage)
        {
            try
            {
                using (var requestMessage = new HttpRequestMessage())
                {
                    var json = JsonSerializer.Serialize(dialogMessage);

                    requestMessage.Method = HttpMethod.Post;
                    requestMessage.RequestUri = new Uri($"https://graph.facebook.com/v16.0/101196852940502/messages");
                    requestMessage.Headers.Add("Authorization", $"Bearer {_tokenAPI}");
                    requestMessage.Content = new StringContent(json, UTF8Encoding.UTF8, "application/json");

                    string responseString = null;
                    try
                    {
                        var responseMessage = await SendAsync(requestMessage, TimeSpan.FromSeconds(10), json);
                        responseString = await responseMessage.Content.ReadAsStringAsync();

                        var response = JsonSerializer.Deserialize<CloudAPIOutMessageResponse>(responseString);

                        var id = response.Messages?.Select(i => i.Id).FirstOrDefault();
                        if (string.IsNullOrEmpty(id))
                            throw new Exception("Não foi possível enviar mensagem");

                        return id;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error CloudAPI\nRequest {json}\nResponse: {responseString}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro na comunicação com Whatsapp, tente novamente em alguns segundos ou entre em contato com o suporte técnico da Helena.", ex);
            }
        }

        private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, TimeSpan timeout, string jsonRequest = null)
        {
            HttpStatusCode statusCode;
            string responseString;
            try
            {
                var cts = new CancellationTokenSource(timeout);
                var responseMessage = await httpClient.SendAsync(requestMessage, cts.Token);
               statusCode = responseMessage.StatusCode;
                responseString = await responseMessage.Content.ReadAsStringAsync();

                switch (statusCode)
                {
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.ServiceUnavailable:
                    case HttpStatusCode.InternalServerError:
                    case HttpStatusCode.GatewayTimeout:
                    case HttpStatusCode.RequestTimeout:
                        throw new Exception(responseString);
                }

                return responseMessage;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

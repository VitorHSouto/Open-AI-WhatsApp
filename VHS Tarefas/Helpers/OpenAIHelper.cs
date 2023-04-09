using System;
using System.Net.Http.Headers;
using System.Text.Json;
using VHS_Tarefas.Domain.Messages.OpenAI;
using VHS_Tarefas.Entities;

namespace VHS_Tarefas.Helpers
{
    public static class OpenAIHelper
    {
        static HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(360) };
        static string URLBase { get { return "https://api.openai.com/v1"; } }

        static string AUTH { get { return "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"; } }
        //static string AUTH { get { return "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"; } }

        const string RewriteTemplateMessage = "Reescreva a mensagem para que fique mais amigável. Utilize emojis, quebras de linha e negrito, utilize como marcação de negrito apenas um asterisco, não utilize duas quebras de linha em sequência.\n";
        const string GetThemeMessage = "Sugira uma palavra, em inglês, para pesquisa de fotos baseado na mensagem abaixo: ";

        public static async Task<string> GenerateImageQuery(string query)
        {
            var url = $"{URLBase}/images/generations";
            var imageRequest = CreateImageObjectRequest(query);

            string json = JsonSerializer.Serialize(imageRequest);

            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Headers =
                {
                    { "Authorization", $"Bearer {AUTH}"},
                },
                Content = new StringContent(json)
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };

            using var response = await httpClient.SendAsync(request);
            try
            {
                //response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var deserialize = JsonSerializer.Deserialize<OpenAIImageOut>(body);
                var responseUrl = deserialize?.Data?.First()?.Url;
                return responseUrl;
            }
            catch (Exception ex)
            {
                if ((int)response.StatusCode == 429)
                    throw new Exception("O limite de pedidos foi atingido, tente novamente mais tarde.");

                throw new Exception($"Error OpenAI\nRequest {url}\nResponse: {response}", ex);
            }
        }

        public static async Task<string> AnswerQuery(List<MessageEntity> messages)
        {
            var url = $"{URLBase}/chat/completions";
            var chat = CreateChatObjectRequest();
            chat.Messages = CreateOpenAiMessages(messages);

            string json = JsonSerializer.Serialize(chat);

            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Headers =
                {
                    { "Authorization", $"Bearer {AUTH}"},
                },
                Content = new StringContent(json)
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };

            using var response = await httpClient.SendAsync(request);
            try
            {
                //response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var deserialize = JsonSerializer.Deserialize<OpenAIChatResponse>(body);
                var text = deserialize?.Choices?.First()?.Message?.Content;
                return text;
            }
            catch (Exception ex)
            {
                if ((int)response.StatusCode == 429)
                    throw new Exception("O limite de pedidos foi atingido, tente novamente mais tarde.");

                throw new Exception($"Error OpenAI\nRequest {url}\nResponse: {response}", ex);
            }
        }

        private static List<OpenAIMessage> CreateOpenAiMessages(List<MessageEntity> messages)
        {
            var openAIMessages = new List<OpenAIMessage>();
            foreach(var message in messages)
            {
                var newMessage = new OpenAIMessage();
                newMessage.Content = message.Text;
                newMessage.Role = message.IsUser ? "user" : "assistant";
                openAIMessages.Add(newMessage);
            }

            return openAIMessages;
        }

        private static OpenAISendChat CreateChatObjectRequest()
        {
            var chat = new OpenAISendChat();
            chat.Model = "gpt-3.5-turbo";
            chat.Messages = new List<OpenAIMessage>();
            chat.Temperature = 1;
            chat.TopP = 1;
            chat.N = 1;
            chat.Stream = false;
            chat.MaxTokens = 2000;
            chat.PresencePenalty = 0;
            chat.FrequencyPenalty = 0;

            return chat;
        }

        private static OpenAIImage CreateImageObjectRequest(string prompt)
        {
            var image = new OpenAIImage();
            image.Prompt = prompt;
            image.Size = "1024x1024";
            image.N = 1;

            return image;
        }
    }
}

using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace SignalRChatServer
{
    public class Chat : Hub // pipeline que escuta através dos métodos abaixo as mensagens enviadas pelos clientes 
    {
        public static List<Message> Messages;
        public Chat()
        {
            if (Messages == null) 
                Messages = new List<Message>();
        }

        public void NewMessage(string userName, string text) //mensagem a ser enviada
        {
            Clients.All.SendAsync("newMessage", userName, text); //faz a mensagem ser enviada para todos os clientes conectados
            Messages.Add(new Message()
            {
                Text = text,
                UserName = userName,
            });
        }

        public void NewUser(string userName, string connectionId) //mostra quando um usuário novo entra no chat
        {
            Clients.Client(connectionId).SendAsync("previousMessages", Messages); //mostra as mensagens que foram enviadas anteriormente no chat
            Clients.All.SendAsync("newUser", userName);
        }
    }

    public class Message
    {
        public string UserName { get; set; }
        public string Text { get; set; }
    }
}

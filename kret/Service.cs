using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace kret
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service : IService
    {
        List<User> users = new List<User>();
        int id = 1;

        public int Connect(string name)
        {
            User user = new User
            {
                ID = id,
                Name = name,
                operationContext = OperationContext.Current
            };

            id++;

            SendMessage($" {name} подключился.", 0);

            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);

            if(user != null)
            {
                users.Remove(user);

                SendMessage($" {user.Name} отключился.", 0);
            }
        }

        public void SendMessage(string message, int id)
        {
            foreach(var user in users)
            {
                string answer = DateTime.Now.ToShortTimeString();

                var usr = users.FirstOrDefault(i => i.ID == id);
                if(usr != null)
                {
                    answer += $": {usr.Name} > ";
                }

                answer += message;

                user.operationContext.GetCallbackChannel<IServiceCallback>().MessageCallback(answer);
            }
        }
    }
}

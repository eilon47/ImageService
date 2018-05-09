using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Infrastructure
{
    public class CommandMessage
    {
        CommandEnum commandID;
        string message;
        public CommandEnum CommandID
        {
            get { return commandID; }
            set { commandID = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public CommandMessage (CommandEnum id, string message)
        {
            commandID = id;
            this.message = message;
        }
        public override string ToString()
        {

            return CommandID.ToString() + ";" + Message;
        }
        public static CommandMessage FromString(string str)
        {
            string[] param = str.Split(';');
            CommandEnum c = (CommandEnum)int.Parse(param[0]);
            if(param.Length == 1)
            {
                return new CommandMessage(c, null);
            }
            return new CommandMessage(c, param[1]);
        }
    }
}

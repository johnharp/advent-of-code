using System;
namespace day02
{
    public class NavStep
    {
        public enum ActionValue { Forward, Up, Down}

        public ActionValue Action { get; set; }
        public int Quantity { get; set; }

        public NavStep(string txt)
        {
            string[] parts = txt.Split(' ');
            if (parts.Length != 2)
            {
                throw new FormatException("invalid number of fields in NavStep text");
            }

            switch(parts[0])
            {
                case "forward":
                    Action = ActionValue.Forward;
                    break;
                case "up":
                    Action = ActionValue.Up;
                    break;
                case "down":
                    Action = ActionValue.Down;
                    break;
                default:
                    throw new FormatException("invalid action in NavStep text");
            }

            int quantity;
            if (int.TryParse(parts[1], out quantity))
            {
                Quantity = quantity;
            }
            else
            {
                throw new FormatException("invalid quantity in NavStep txt");
            }
        }
    }
}

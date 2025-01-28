using System;
namespace day02
{
    public class Nav
    {
        public int HorizontalPosition { get; set; }
        public int Depth { get; set; }
        public int Aim { get; set; }

        public void ApplyNavStep(NavStep step)
        {
            switch(step.Action)
            {
                case NavStep.ActionValue.Down:
                    Aim += step.Quantity;
                    break;
                case NavStep.ActionValue.Up:
                    Aim -= step.Quantity;
                    break;
                case NavStep.ActionValue.Forward:
                    HorizontalPosition += step.Quantity;
                    Depth += (Aim * step.Quantity);
                    break;
            }

        }
    }
}

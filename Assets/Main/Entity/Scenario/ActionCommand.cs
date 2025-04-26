public abstract class ActionCommand { }

namespace Entity.Scenario
{
    public class ShowText : ActionCommand
    {
        public string text;
    }

    public class ActionCommandWait : ActionCommand
    {
        public float duration;
    }
}

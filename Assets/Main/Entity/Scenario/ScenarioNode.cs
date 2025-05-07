namespace Entity.Scenario
{
    public class ScenarioNode
    {
        public ActionType Type { get; }
        public string Name { get; }
        public string Text { get; }

        public ScenarioNode(ActionType type, string name, string text)
        {
            Type = type;
            Name = name;
            Text = text;
        }
    }
}

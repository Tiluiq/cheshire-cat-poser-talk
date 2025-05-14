namespace CheshireCatPoserTalk.Entity.Scenario
{
    public abstract class ScenarioNode { }

    public class TextNode : ScenarioNode
    {
        public string TargetId { get; }
        public string Name { get; }
        public string Text { get; }
        public bool WaitInput { get; set; }

        public TextNode(string targetId, string name, string text, bool waitInput = true)
        {
            TargetId = targetId;
            Name = name;
            Text = text;
            WaitInput = waitInput;
        }
    }

    public class HideTextNode : ScenarioNode
    {
        public string TargetId { get; }

        public HideTextNode(string targetId)
        {
            TargetId = targetId;
        }
    }

    public class AnimationNode : ScenarioNode
    {
        public string TargetId { get; }
        public string AnimationName { get; }
        public bool WaitForCompletion { get; }

        public AnimationNode(string targetId, string animationName, bool waitForCompletion = false)
        {
            TargetId = targetId;
            AnimationName = animationName;
            WaitForCompletion = waitForCompletion;
        }
    }

    public class WaitNode : ScenarioNode
    {
        public float Duration { get; }

        public WaitNode(float duration)
        {
            Duration = duration;
        }
    }
}

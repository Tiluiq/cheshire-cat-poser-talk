namespace CheshireCatPoserTalk.Entity.Scenario
{
    public abstract class ScenarioNode { }

    public class ShowTextWindowNode : ScenarioNode
    {
        public string WindowName { get; }
        public bool ShowName { get; }

        public ShowTextWindowNode(string windowName, bool showName = true)
        {
            WindowName = windowName;
            ShowName = showName;
        }
    }

    public class HideTextWindowNode : ScenarioNode
    {
        public string WindowName { get; }

        public HideTextWindowNode(string windowName)
        {
            WindowName = windowName;
        }
    }

    public class TextNode : ScenarioNode
    {
        public string Name { get; }
        public string Text { get; }
        public bool WaitInput { get; set; }

        public TextNode(string name, string text, bool waitInput = true)
        {
            Name = name;
            Text = text;
            WaitInput = waitInput;
        }
    }

    public class AnimationNode : ScenarioNode
    {
        public string AnimationName { get; }
        public bool WaitForCompletion { get; }

        public AnimationNode(string animationName, bool waitForCompletion = false)
        {
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

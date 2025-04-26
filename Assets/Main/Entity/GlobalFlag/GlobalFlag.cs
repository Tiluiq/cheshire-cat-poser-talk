namespace Entity.GlobalFlag
{
    public class GlobalFlag
    {
        public bool IsCleared { get; private set; } = false;

        public void SetIsCleared(bool isCleared)
        {
            IsCleared = isCleared;
        }
    }
}

namespace Entity.Flag.Global
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

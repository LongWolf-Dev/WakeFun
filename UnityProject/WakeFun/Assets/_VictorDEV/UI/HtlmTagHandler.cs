namespace VictorDev.UI
{
    public abstract class HtlmTagHandler
    {
        public static string ToSetSize(string target, float size)
        {
            return $"<size={size}>{target}</size>";
        }
    }
}

using GameEngine.Save;

namespace GameEngine
{
    public static class ResourcesExtensions
    {
        public static void Setup(this Resource resource, ResourceData data)
        {
            resource.Amount = data.Amount;
        }
    }
}
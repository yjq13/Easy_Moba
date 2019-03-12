
namespace Common
{
    public class ResourceID
    {
        private readonly string m_resourcePath = "";
        private ResourceID(string resourcePath)
        {
            m_resourcePath = resourcePath;
        }

        public static implicit operator ResourceID(string value)
        {
            ResourceID id = new ResourceID(value);
            return id;
        }

        public static implicit operator string(ResourceID value)
        {
            return value.m_resourcePath;
        }

        public static readonly ResourceID INVALID = "#null";
    }
}

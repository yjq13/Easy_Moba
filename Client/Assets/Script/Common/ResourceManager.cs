using UnityEngine;

namespace Common
{
    class ResourceManager : SingletonModule<ResourceManager>
    {
        protected override void OnCleanup()
        {
            //throw new System.NotImplementedException();
        }

        protected override void OnInit()
        {
            //throw new System.NotImplementedException();
        }

        public Object GetResource(ResourceID id)
        {
            if(id != ResourceID.INVALID)
            {
                return Resources.Load(id);
            }
            else
            {
                return null;
            }

        }

        public ResourceID GetResourceIDbyPath(string path)
        {
            if (path != null && path != string.Empty)
            {
                return path;
            }
            else
            {
                return null;
            }

        }
    }
}

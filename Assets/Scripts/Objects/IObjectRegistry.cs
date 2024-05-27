using System.Collections.Generic;

namespace Objects
{
    public interface IObjectRegistry
    {
        public bool TryRegisterObject(InspectableObject obj);
        public bool TryUnregisterObject(string id);
        
        public bool TryGetObject(string id, out InspectableObject obj);
        public InspectableObject GetObject(string id);
        public List<InspectableObject> GetObject(List<string> ids);
        public List<InspectableObject> GetAll();

    }
}
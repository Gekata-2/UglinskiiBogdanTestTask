using System.Collections.Generic;

namespace Objects
{
    public class DictionaryRegistry : IObjectRegistry
    {
        private readonly Dictionary<string, InspectableObject> _objects = new();

        public bool TryRegisterObject(InspectableObject obj)
        {
            if (_objects.ContainsKey(obj.name))
                return false;

            _objects.Add(obj.name, obj);

            return true;
        }


        public bool TryUnregisterObject(string id)
        {
            if (!_objects.ContainsKey(id))
                return false;

            _objects.Remove(id);
            return true;
        }


        public InspectableObject GetObject(string id)
        {
            return _objects[id];
        }

        public bool TryGetObject(string id, out InspectableObject obj)
        {
            if (!_objects.ContainsKey(id))
            {
                obj = null;
                return false;
            }

            obj = _objects[id];
            return true;
        }

        public List<InspectableObject> GetObject(List<string> ids)
        {
            List<InspectableObject> objects = new List<InspectableObject>();
            
            foreach (var id in ids)
                if (TryGetObject(id, out InspectableObject obj))
                    objects.Add(obj);

            return objects;
        }

        public List<InspectableObject> GetAll()
        {
            List<InspectableObject> objects = new List<InspectableObject>();
            
            foreach (var pair in _objects) 
                objects.Add(pair.Value);

            return objects;
        }
    }
}
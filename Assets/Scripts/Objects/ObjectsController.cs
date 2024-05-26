using UnityEngine;

namespace Objects
{
    public class ObjectsController : MonoBehaviour
    {
        public static ObjectsController Instance { get; private set; }
        private ObjectRegistry _registry;

        public void Init()
        {
            if (Instance == null)
            {
                Instance = this;
                _registry = new ObjectRegistry();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Add(InspectableObject obj)
        {
            _registry.TryRegisterObject(obj);
        }

        public void Remove(string id)
        {
            _registry.TryUnregisterObject(id);
        }
    }
}
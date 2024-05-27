using UnityEngine;

namespace Objects
{
    public class ObjectData
    {
        public float Alpha;
        public Color Color;
        public bool IsVisible;
    }

    public class InspectableObject : MonoBehaviour
    {
        [SerializeField] private GameObject view;
        [SerializeField] private Collider interactZone;

        public float Alpha
        {
            get => view.GetComponent<ObjectView>().Alpha;
            set => view.GetComponent<ObjectView>().Alpha = value;
        }

        public Color Color
        {
            get => view.GetComponent<ObjectView>().Color;
            set => view.GetComponent<ObjectView>().Color = value;
        }

        private static int _objectCount;

        private void Start()
        {
            name = $"Object {_objectCount++}";
            ObjectsController.Instance.Add(this);
        }

        private void OnDestroy()
        {
            ObjectsController.Instance.Remove(name);
        }

        public ObjectData GetData() => new()
        {
            Alpha = Alpha,
            Color = Color,
            IsVisible = view.activeSelf
        };

        public void Show()
        {
            view.SetActive(true);
            interactZone.enabled = true;
        }

        public void Hide()
        {
            view.SetActive(false);
            interactZone.enabled = false;
        }
    }
}
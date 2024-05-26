using System;
using UnityEngine;

namespace Objects
{
    public class ObjectData
    {
        public float Alpha;
        public Color Color;
    }

    public class InspectableObject : MonoBehaviour
    {
        [SerializeField] private GameObject view;
        [SerializeField] private Collider interactZone;

        public float Alpha
        {
            get => view.GetComponent<MeshRenderer>().material.color.a;
            set
            {
                Renderer rendererView = view.GetComponent<MeshRenderer>();
                var material = rendererView.material;
                material.color = new Color(material.color.r, material.color.g,
                    material.color.b, value);
            }
        }

        public Color Color
        {
            get => view.GetComponent<MeshRenderer>().material.color;
            set
            {
                Renderer rendererView = view.GetComponent<MeshRenderer>();
                var material = rendererView.material;
                material.color = value;
            }
        }


        // Start is called before the first frame update
        private void Start()
        {
            ObjectsController.Instance.Add(this);
        }

        private void OnDestroy()
        {
            ObjectsController.Instance.Remove(name);
        }

        public ObjectData GetData() => new() { Alpha = Alpha, Color = Color };

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
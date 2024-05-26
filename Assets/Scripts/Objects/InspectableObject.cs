using UnityEngine;

namespace Objects
{
    public class InspectableObject : MonoBehaviour
    {
        [SerializeField] private GameObject view;

        public float Alpha
        {
            get => view.GetComponent<MeshRenderer>().material.color.a;
            set
            {
                Renderer rendererView = GetComponentInChildren<MeshRenderer>();
                var material = rendererView.material;
                material.color = new Color(material.color.r, material.color.g,
                    material.color.b, value);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
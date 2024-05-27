using System;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class ObjectView : MonoBehaviour
    {
        [SerializeField] private List<MeshRenderer> views;
        [SerializeField] private Color defaultColor;
        private float _alpha = 1f;
        private Color _color = Color.white;

        private void Awake()
        {
            Color = defaultColor;
        }

        public float Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                SetAlpha();
            }
        }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                SetColor();
            }
        }

        private void SetAlpha()
        {
            foreach (var view in views)
            {
                Renderer rendererView = view.GetComponent<MeshRenderer>();
                var material = rendererView.material;
                material.color = new Color(material.color.r, material.color.g,
                    material.color.b, _alpha);
            }
        }

        private void SetColor()
        {
            Color value = _color;
            foreach (var view in views)
            {
                Renderer rendererView = view.GetComponent<MeshRenderer>();
                value.a = Alpha;
                var material = rendererView.material;
                material.color = value;
            }
        }
    }
}
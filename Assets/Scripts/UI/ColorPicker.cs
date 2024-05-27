using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ColorPicker : MonoBehaviour
    {
        [SerializeField] private ColorUI r, g, b;

        [SerializeField] private Image result;
        public event Action<Color> onColorChanged;

        private Color _color = Color.white;

        // Start is called before the first frame update
        private void Start()
        {
            r.OnChannelValueChanged += OnRedChannelValueChanged;
            g.OnChannelValueChanged += OnGreenChannelValueChanged;
            b.OnChannelValueChanged += OnBlueChannelValueChanged;
        }

        private void OnDestroy()
        {
            r.OnChannelValueChanged -= OnRedChannelValueChanged;
            g.OnChannelValueChanged -= OnGreenChannelValueChanged;
            b.OnChannelValueChanged -= OnBlueChannelValueChanged;
        }

        private void OnRedChannelValueChanged(float r)
        {
            _color.r = r;
            UpdateColor();
        }

        private void OnGreenChannelValueChanged(float g)
        {
            _color.g = g;
            UpdateColor();
        }

        private void OnBlueChannelValueChanged(float b)
        {
            _color.b = b;
            UpdateColor();
        }

        private void UpdateColor()
        {
            result.color = _color;
            onColorChanged?.Invoke(_color);
        }
    }
}
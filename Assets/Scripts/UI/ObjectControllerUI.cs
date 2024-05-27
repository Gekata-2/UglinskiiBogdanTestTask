using System.Collections.Generic;
using Objects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ObjectControllerUI : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private Toggle allToggle;
        [SerializeField] private Slider alphaSlider;
        [SerializeField] private Sprite showModeSprite, hideModeSprite;
        [SerializeField] private Color showModeColor, hideModeColor;
        [SerializeField] private Button showButton, destroyButton, spawnObjectButton;
        [SerializeField] private ColorPicker colorPicker;
        [SerializeField] private GameObject objectUIPrefab;

        private List<ObjectUI> _views = new();
        private List<string> _checkedObj = new();

        enum EShowMode
        {
            Show,
            Hide
        }

        private EShowMode _showMode;

        public void Init()
        {
            ObjectsController.Instance.onObjectAdded += OnObjectAdded;
            ObjectsController.Instance.onObjectRemoved += OnObjectRemoved;

            showButton.onClick.AddListener(OnShowClicked);
            destroyButton.onClick.AddListener(OnDestroyObjectClicked);
            spawnObjectButton.onClick.AddListener(OnSpawnObjectClicked);
            allToggle.onValueChanged.AddListener(OnAllToggleClicked);
            alphaSlider.onValueChanged.AddListener(OnAlphaSliderValueChanged);
            colorPicker.onColorChanged += OnColorChanged;
            UpdateView();
            SetHideMode();
        }


        private void OnDestroy()
        {
            ObjectsController.Instance.onObjectAdded -= OnObjectAdded;
            ObjectsController.Instance.onObjectRemoved -= OnObjectRemoved;

            showButton.onClick.RemoveAllListeners();
            allToggle.onValueChanged.RemoveAllListeners();
            alphaSlider.onValueChanged.RemoveAllListeners();

            foreach (var v in _views)
                v.onToggle -= OnToggle;
        }

        private void OnSpawnObjectClicked()
        {
            ObjectsController.Instance.SpawnObject();
        }

        private void OnColorChanged(Color color)
        {
            ObjectsController.Instance.SetColor(color, _checkedObj);
            UpdateView();
        }

        private void OnDestroyObjectClicked()
        {
            ObjectsController.Instance.DestroyObjects(_checkedObj);

            foreach (var id in _checkedObj)
                RemoveObjectView(id);

            _checkedObj.Clear();
            UpdateView();
        }

        private void OnObjectRemoved(string id)
        {
            RemoveObjectView(id);
            if (_checkedObj.Contains(id))
                _checkedObj.Remove(id);
        }

        private void RemoveObjectView(string id)
        {
            ObjectUI viewUI = null;

            foreach (var view in _views)
            {
                if (view.Id == id)
                {
                    viewUI = view;
                    break;
                }
            }

            if (viewUI == null)
                return;

            _views.Remove(viewUI);
            Destroy(viewUI.gameObject);
        }

        private void OnObjectAdded(string objId)
        {
            GameObject go = Instantiate(objectUIPrefab, content);
            if (go.TryGetComponent(out ObjectUI view))
            {
                view.Init(objId);
                _views.Add(view);
                view.onToggle += OnToggle;
            }

            UpdateView();
        }

        private void OnShowClicked()
        {
            switch (_showMode)
            {
                case EShowMode.Show:
                    ObjectsController.Instance.ShowObjects(_checkedObj);
                    SetHideMode();

                    break;
                case EShowMode.Hide:
                    ObjectsController.Instance.HideObjects(_checkedObj);
                    SetShowMode();
                    break;
                default:
                    break;
            }

            UpdateView();
        }

        private void SetHideMode()
        {
            _showMode = EShowMode.Hide;
            showButton.image.sprite = hideModeSprite;
            showButton.image.color = hideModeColor;
        }

        private void SetShowMode()
        {
            _showMode = EShowMode.Show;
            showButton.image.sprite = showModeSprite;
            showButton.image.color = showModeColor;
        }

        private void OnAlphaSliderValueChanged(float val)
        {
            ObjectsController.Instance.SetAlpha(val / alphaSlider.maxValue, _checkedObj);
            UpdateView();
        }

        private void OnAllToggleClicked(bool isToggle)
        {
            foreach (var view in _views)
            {
                view.SetToggle(isToggle);
            }
        }


        private void OnToggle(bool isToggled, string id)
        {
            switch (isToggled)
            {
                case true:
                    if (!_checkedObj.Contains(id)) _checkedObj.Add(id);
                    break;
                case false:
                    if (_checkedObj.Contains(id)) _checkedObj.Remove(id);
                    break;
            }

            allToggle.SetIsOnWithoutNotify(_checkedObj.Count >= _views.Count);
        }

        private void UpdateView()
        {
            foreach (var view in _views)
            {
                view.UpdateView(ObjectsController.Instance.GetInfo(view.Id));
               
            }

            if (_checkedObj.Count < _views.Count || _views.Count == 0)
                allToggle.SetIsOnWithoutNotify(false);
            else
                allToggle.SetIsOnWithoutNotify(true);
        }
    }
}
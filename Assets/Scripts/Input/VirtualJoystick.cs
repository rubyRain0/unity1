using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Input
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform handle;
        [SerializeField] private float handleRange = 60f;
        [SerializeField] private CanvasGroup canvasGroup;

        private Vector2 _value;
        private RectTransform _area;

        public Vector2 Value => _value;

        private void Awake()
        {
            _area = (RectTransform)transform;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _area, eventData.position, eventData.pressEventCamera, out Vector2 local))
            {
                _value = Vector2.ClampMagnitude(local / handleRange, 1f);
                if (handle != null) handle.anchoredPosition = _value * handleRange;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _value = Vector2.zero;
            if (handle != null) handle.anchoredPosition = Vector2.zero;
        }
    }
}

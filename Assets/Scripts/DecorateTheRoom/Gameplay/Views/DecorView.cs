using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BlackKakadu.DecorateTheRoom.Gameplay.Views
{
    public class DecorView : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<DecorView> Clicked;

        public event Action<DecorView> BeginDrag;

        public int SortingOrder => _spriteRenderer.sortingOrder;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private PolygonCollider2D _collider;

        private Transform _transform;

        private Camera _camera;

        private Vector3 _offset;

        private bool _isDragging;

        private void Awake()
        {
            _transform = transform;
            _camera = Camera.main;
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;

            var shapePoints = new List<Vector2>();

            sprite.GetPhysicsShape(0, shapePoints);

            _collider.points = shapePoints.ToArray();
        }

        public void SetSortingOrder(int order)
        {
            _spriteRenderer.sortingOrder = order;
            var currentPosition = _transform.position;
            currentPosition.z = -0.01f * order;
            _transform.position = currentPosition;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isDragging)
            {
                return;
            }

            Clicked?.Invoke(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;

            BeginDrag?.Invoke(this);

            _offset = transform.position - _camera.ScreenToWorldPoint(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDragging)
            {
                return;
            }

            var newPosition = _camera.ScreenToWorldPoint(eventData.position) + _offset;

            newPosition.z = transform.position.z;

            _transform.position = newPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_isDragging)
            {
                return;
            }

            _isDragging = false;

            _offset = Vector3.zero;
        }
    }
}
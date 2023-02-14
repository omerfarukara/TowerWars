using System;
using System.Collections;
using DG.Tweening;
using GameFolders.Scripts.Concretes;
using UnityEngine;

namespace GameFolders.Scripts.Controllers
{
    public class MouseFollower : MonoBehaviour
    {
        #region Old Mechanic

        // [SerializeField] private float openTime;
        // [SerializeField] private float closeTime;
        // [SerializeField] private Ease openEase;
        // [SerializeField] private Ease closeEase;
        // [SerializeField] private LayerMask groundLayer;
        //
        // private Camera _camera;
        // private Tween _openTween;
        // private Tween _closeTween;
        //
        // private Vector3 _screenPosition;
        // private Vector3 _worldPosition;
        //
        // private float _defaultScale;
        // private bool _trueTap;
        //
        // private void Awake()
        // {
        //     _camera = Camera.main;
        // }
        //
        // // Start is called before the first frame update
        // void Start()
        // {
        //     _defaultScale = transform.localScale.x;
        //     transform.localScale = Vector3.zero;
        // }
        //
        // private void Update()
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         _screenPosition = Input.mousePosition;
        //
        //         Ray ray = _camera.ScreenPointToRay(_screenPosition);
        //
        //         if (Physics.Raycast(ray, out RaycastHit hit, 100, groundLayer))
        //         {
        //             if (hit.collider.CompareTag("Ground"))
        //             {
        //                 if (_closeTween != null && _closeTween.IsPlaying())
        //                 {
        //                     _closeTween.Kill();
        //                 }
        //
        //                 _openTween = transform.DOScale(Vector3.one * _defaultScale, openTime).SetEase(openEase);
        //                 _trueTap = true;
        //             }
        //         }
        //     }
        //     else if (Input.GetMouseButton(0))
        //     {
        //         if (!_trueTap) return;
        //
        //         _screenPosition = Input.mousePosition;
        //
        //         Ray ray = _camera.ScreenPointToRay(_screenPosition);
        //
        //         if (Physics.Raycast(ray, out RaycastHit hit, 100, groundLayer))
        //         {
        //             if (hit.collider.CompareTag("Ground"))
        //             {
        //                 _worldPosition = hit.point;
        //             }
        //         }
        //
        //         transform.position = _worldPosition;
        //     }
        //     else if (Input.GetMouseButtonUp(0))
        //     {
        //         if (!_trueTap) return;
        //
        //         if (_openTween != null && _openTween.IsPlaying())
        //         {
        //             _openTween.Kill();
        //         }
        //
        //         _closeTween = transform.DOScale(Vector3.zero, closeTime).SetEase(closeEase);
        //         _trueTap = false;
        //     }
        // }

        #endregion
        
        [SerializeField] private float maxScale;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float minSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private LayerMask groundLayer;

        private Camera _camera;
        private EventData _eventData;
        
        private Vector3 _screenPosition;
        private Vector3 _worldPosition;

        private float _defaultScale;
        private float _currentScale;
        private float _currentSpeed;
        private bool _trueTap;

        private void Awake()
        {
            _camera = Camera.main;
            _eventData = Resources.Load("EventData") as EventData;
        }

        void Start()
        {
            _currentSpeed = maxSpeed;
            _currentScale = 0;
            transform.localScale = Vector3.zero;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _screenPosition = Input.mousePosition;

                Ray ray = _camera.ScreenPointToRay(_screenPosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 100, groundLayer))
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        _trueTap = true;
                    }
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (!_trueTap) return;

                _screenPosition = Input.mousePosition;

                Ray ray = _camera.ScreenPointToRay(_screenPosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 100, groundLayer))
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        _worldPosition = hit.point;
                    }
                }

                transform.position = _worldPosition;
                
                if (_currentScale <= maxScale)
                {
                    _currentScale += _currentSpeed * Time.deltaTime;
                }

                if (_currentSpeed > minSpeed)
                {
                    _currentSpeed -= acceleration * Time.deltaTime;
                }
                
                transform.localScale = Vector3.one * _currentScale;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (!_trueTap) return;
                
                transform.localScale = Vector3.zero;
                _currentScale = 0;
                _currentSpeed = maxSpeed;
                
                _trueTap = false;
            }
        }
    }
}
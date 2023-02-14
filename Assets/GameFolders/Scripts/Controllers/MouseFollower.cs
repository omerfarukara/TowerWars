using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace GameFolders.Scripts.Controllers
{
    public class MouseFollower : MonoBehaviour
    {
        [SerializeField] private float openTime;
        [SerializeField] private float closeTime;
        [SerializeField] private Ease openEase;
        [SerializeField] private Ease closeEase;
        [SerializeField] private LayerMask groundLayer;

        private Camera _camera;
        private Tween _openTween;
        private Tween _closeTween;

        private Vector3 _screenPosition;
        private Vector3 _worldPosition;
        
        private float _defaultScale;
        

        private void Awake()
        {
            _camera = Camera.main;
        }

        // Start is called before the first frame update
        void Start()
        {
            _defaultScale = transform.localScale.x;
            transform.localScale = Vector3.zero;
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _screenPosition = Input.mousePosition;
                
                Ray ray = _camera.ScreenPointToRay(_screenPosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 100,groundLayer))
                {
                    if (_closeTween != null && _closeTween.IsPlaying())
                    {
                        _closeTween.Kill();
                    }
                    _openTween = transform.DOScale(Vector3.one * _defaultScale, openTime).SetEase(openEase);
                }
            }
            else if (Input.GetMouseButton(0))
            {
                _screenPosition = Input.mousePosition;
                
                Ray ray = _camera.ScreenPointToRay(_screenPosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 100,groundLayer))
                {
                    _worldPosition = hit.point;
                }
                
                transform.position = _worldPosition;

            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (_openTween != null && _openTween.IsPlaying())
                {
                    _openTween.Kill();
                }
                _closeTween = transform.DOScale(Vector3.zero, closeTime).SetEase(closeEase);
            }
        }
    }
}

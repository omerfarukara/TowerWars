using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[System.Serializable]
public class Motion
{
    [SerializeField] private Transform transform;
    [SerializeField] private MotionType motionType;
    [SerializeField] private Vector3 defaultValue = Vector3.zero;
    [SerializeField] private Vector3 motionValue = Vector3.one;
    [SerializeField] private float openTime;
    [SerializeField] private float closeTime;
    [SerializeField] private Ease openEase;
    [SerializeField] private Ease closeEase;
    
    public void DoMotionIn(params UnityAction[] onComplateActions)
    {
        switch (motionType)
        {
            case MotionType.Move:
                transform.DOMove(motionValue, openTime).SetEase(openEase).OnComplete(() =>
                {
                    foreach (UnityAction action in onComplateActions)
                    {
                        action?.Invoke();
                    }
                });;
                break;
            case MotionType.LocalMove:
                transform.DOLocalMove(motionValue, openTime).SetEase(openEase).OnComplete(() =>
                {
                    foreach (UnityAction action in onComplateActions)
                    {
                        action?.Invoke();
                    }
                });;
                break;
            case MotionType.Scale:
                transform.DOScale(motionValue, openTime).SetEase(openEase).OnComplete(() =>
                {
                    foreach (UnityAction action in onComplateActions)
                    {
                        action?.Invoke();
                    }
                });
                break;
        }
    }
    
    public void DoMotionBack(params UnityAction[] onComplateActions)
    {
        switch (motionType)
        {
            case MotionType.Move:
                transform.DOMove(defaultValue, closeTime).SetEase(closeEase).OnComplete(() =>
                {
                    foreach (UnityAction action in onComplateActions)
                    {
                        action?.Invoke();
                    }
                });;
                break;
            case MotionType.LocalMove:
                transform.DOLocalMove(defaultValue, closeTime).SetEase(closeEase).OnComplete(() =>
                {
                    foreach (UnityAction action in onComplateActions)
                    {
                        action?.Invoke();
                    }
                });;
                break;
            case MotionType.Scale:
                transform.DOScale(defaultValue, closeTime).SetEase(closeEase).OnComplete(() =>
                {
                    foreach (UnityAction action in onComplateActions)
                    {
                        action?.Invoke();
                    }
                });
                break;
        }
    }

}
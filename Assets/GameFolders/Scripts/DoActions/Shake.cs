using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[System.Serializable]
public class Shake
{
    [SerializeField] private Transform transform;
    [SerializeField] private ShakeType shakeType;
    [SerializeField] private Ease ease;
    [SerializeField] private float duration;
    [SerializeField] private Vector3 strength;
    [SerializeField] private int vibrato = 10;
    [SerializeField] [Range(0, 180)] private float randomness = 90f;
    [SerializeField] private bool snapping = false;
    [SerializeField] private bool fadeOut = true;


    public void DoShake(params UnityAction[] onComplateActions)
    {
        switch (shakeType)
        {
            case ShakeType.Position:
                transform.DOShakePosition(duration, strength, vibrato, randomness, snapping, fadeOut).SetEase(ease)
                    .OnComplete(
                        () =>
                        {
                            foreach (UnityAction action in onComplateActions)
                            {
                                action?.Invoke();
                            }
                        });
                ;
                break;
            case ShakeType.Rotation:
                transform.DOShakeRotation(duration, strength, vibrato,randomness, fadeOut).SetEase(ease)
                    .OnComplete(
                        () =>
                        {
                            foreach (UnityAction action in onComplateActions)
                            {
                                action?.Invoke();
                            }
                        });
                ;
                break;
            case ShakeType.Scale:
                transform.DOShakeScale(duration, strength, vibrato,randomness, fadeOut).SetEase(ease)
                    .OnComplete(
                        () =>
                        {
                            foreach (UnityAction action in onComplateActions)
                            {
                                action?.Invoke();
                            }
                        });
                ;
                break;
        }
    }
}
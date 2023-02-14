using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameFolders.Scripts.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button productionButton;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button incomeButton;
    
    [Header("Price Texts")]
    [SerializeField] private TextMeshProUGUI productionPriceText;
    [SerializeField] private TextMeshProUGUI attackPriceText;
    [SerializeField] private TextMeshProUGUI incomePriceText;
    
    [Header("Click Animation Values")]
    [SerializeField] private float clickInScaleFactor;
    [SerializeField] private float clickInAnimationTime;
    [SerializeField] private float clickOutAnimationTime;
    [SerializeField] private Ease clickInEase;
    [SerializeField] private Ease clickOutEase;
    
    [Header("Gold Panel")]
    [SerializeField] private TextMeshProUGUI goldText;

    private void OnEnable()
    {
        productionButton.onClick.AddListener(ProductionUpgrade);
        attackButton.onClick.AddListener(AttackUpgrade);
        incomeButton.onClick.AddListener(IncomeUpgrade);
    }

    private void Update()
    {
        goldText.text = $"{GameController.Instance.Gold}";
    }

    private void ProductionUpgrade()
    {
        if (GameController.Instance.IncreaseProduction())
        {
            productionButton.transform.DOScale(Vector3.one * clickInScaleFactor, clickInAnimationTime).SetEase(clickInEase)
                .OnComplete(() => productionButton.transform.DOScale(Vector3.one, clickOutAnimationTime).SetEase(clickOutEase));
        }
    }

    private void AttackUpgrade()
    {
        if (GameController.Instance.IncreaseAttack())
        {
            attackButton.transform.DOScale(Vector3.one * clickInScaleFactor, clickInAnimationTime).SetEase(clickInEase)
                .OnComplete(() => attackButton.transform.DOScale(Vector3.one, clickOutAnimationTime).SetEase(clickOutEase));
        }
    }

    private void IncomeUpgrade()
    {
        if (GameController.Instance.IncreaseIncome())
        {
            incomeButton.transform.DOScale(Vector3.one * clickInScaleFactor, clickInAnimationTime).SetEase(clickInEase)
                .OnComplete(() => incomeButton.transform.DOScale(Vector3.one, clickOutAnimationTime).SetEase(clickOutEase));
        }
    }
}

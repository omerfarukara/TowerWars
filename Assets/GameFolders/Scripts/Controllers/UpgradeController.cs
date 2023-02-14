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
    [SerializeField] private Button panelButton;
    
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

    [Header("Current Stats Texts")] 
    [SerializeField] private TextMeshProUGUI currentAttackStatsText;
    [SerializeField] private TextMeshProUGUI currentIncomeStatsText;
    [SerializeField] private TextMeshProUGUI currentProductionStatsText;

    private Animator _animator;
    
    private bool _isOpenPanel;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        panelButton.onClick.AddListener(OpenClosePanel);
        productionButton.onClick.AddListener(ProductionUpgrade);
        attackButton.onClick.AddListener(AttackUpgrade);
        incomeButton.onClick.AddListener(IncomeUpgrade);
    }

    private void Start()
    {
        attackPriceText.text = $"${GameController.Instance.AttackPrice}";
        incomePriceText.text = $"${GameController.Instance.IncomePrice}";
        productionPriceText.text = $"${GameController.Instance.ProductionPrice}";
        currentAttackStatsText.text = $"{GameController.Instance.Attack}";
        currentIncomeStatsText.text = $"{GameController.Instance.Income}x";
        currentProductionStatsText.text = $"{GameController.Instance.ProductionTime}sec";
    }

    private void Update()
    {
        goldText.text = $"{GameController.Instance.Gold}";
    }

    private void OpenClosePanel()
    {
        _animator.SetTrigger(_isOpenPanel ? "Close" : "Open");
        _isOpenPanel = !_isOpenPanel;
    }

    private void ProductionUpgrade()
    {
        if (GameController.Instance.IncreaseProduction())
        {
            productionButton.transform.DOScale(Vector3.one * clickInScaleFactor, clickInAnimationTime).SetEase(clickInEase)
                .OnComplete(() =>
                {
                    currentProductionStatsText.text = $"{GameController.Instance.ProductionTime:F1}sec";
                    productionPriceText.text = $"${GameController.Instance.ProductionPrice}";
                    productionButton.transform.DOScale(Vector3.one, clickOutAnimationTime).SetEase(clickOutEase);
                });
        }
    }

    private void AttackUpgrade()
    {
        if (GameController.Instance.IncreaseAttack())
        {
            attackButton.transform.DOScale(Vector3.one * clickInScaleFactor, clickInAnimationTime).SetEase(clickInEase)
                .OnComplete(() =>
                {
                    currentAttackStatsText.text = $"{GameController.Instance.Attack}";
                    attackPriceText.text = $"${GameController.Instance.AttackPrice}";
                    attackButton.transform.DOScale(Vector3.one, clickOutAnimationTime).SetEase(clickOutEase);
                });
        }
    }

    private void IncomeUpgrade()
    {
        if (GameController.Instance.IncreaseIncome())
        {
            incomeButton.transform.DOScale(Vector3.one * clickInScaleFactor, clickInAnimationTime).SetEase(clickInEase)
                .OnComplete(() =>
                {
                    currentIncomeStatsText.text = $"{GameController.Instance.Income}x";
                    incomePriceText.text = $"${GameController.Instance.IncomePrice}";
                    incomeButton.transform.DOScale(Vector3.one, clickOutAnimationTime).SetEase(clickOutEase);
                });
        }
    }
}

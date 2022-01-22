using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    [SerializeField] private Button _payButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _doubleMoneyButton;
    [SerializeField] private Text _testUI;
    [SerializeField] private Text _doubleMoney;

    private int _oilCount = 0;

    private Action _rewardType;



    void Awake()
    {
        _payButton = GameObject.Find("Pay Button").GetComponent<Button>();
        _buyButton = GameObject.Find("Buy Button").GetComponent<Button>();
        _doubleMoneyButton = GameObject.Find("2X Money Button").GetComponent<Button>();
    }

    void Start()
    {
        _payButton?.onClick.AddListener(delegate{HandleRewardTypes(PayForSomething);});
        _buyButton?.onClick.AddListener(delegate{HandleRewardTypes(BuySomething);});
        _doubleMoneyButton?.onClick.AddListener(delegate{HandleRewardTypes(DoubleMoney);});
        
    }


    void OnEnable()
    {
        Ads.OnRewardGiven += RewardUIHandler;
    }

    void OnDisable() 
    {
        Ads.OnRewardGiven -= RewardUIHandler;
    }

    void HandleRewardTypes(Action type)
    {
        Ads.ShowRewardedAd(type);
    }

    void PayForSomething()
    {
        _testUI.text = "Paid!";
    }

    void BuySomething()
    {
        _oilCount += 2;
        _testUI.text = $"{_oilCount}L Oil Given!";
    }

    void DoubleMoney()
    {
        int money = Convert.ToInt32(_doubleMoney.text);
        money *= 2;
        _doubleMoney.text = money.ToString();
    }

    void RewardUIHandler(Action type)
    {
        type?.Invoke();
    }
}

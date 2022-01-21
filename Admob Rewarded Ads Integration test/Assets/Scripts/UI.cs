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

    private enum ActionType
    {
        pay,
        buy,
        doubleMoney
    }

    private ActionType _actionType;

    void Awake()
    {
        _payButton = GameObject.Find("Pay Button").GetComponent<Button>();
        _buyButton = GameObject.Find("Buy Button").GetComponent<Button>();
        _doubleMoneyButton = GameObject.Find("2X Money Button").GetComponent<Button>();
    }

    void Start()
    {
        _payButton?.onClick.AddListener(delegate{HandleRewardTypes(ActionType.pay);});
        _buyButton?.onClick.AddListener(delegate{HandleRewardTypes(ActionType.buy);});
        _doubleMoneyButton?.onClick.AddListener(delegate{HandleRewardTypes(ActionType.doubleMoney);});
        
    }


    void OnEnable()
    {
        Ads.OnRewardGiven += RewardUIHandler;
    }

    void OnDisable() 
    {
        Ads.OnRewardGiven -= RewardUIHandler;
    }

    void HandleRewardTypes(ActionType type)
    {
        _actionType = type;
        Ads.ShowRewardedAd();
        
    }

    void RewardUIHandler()
    {
        switch(_actionType)
        {
            case ActionType.pay:
            _testUI.text = "Paid!";
            break;

            case ActionType.buy:
            _oilCount += 2;
            _testUI.text = $"{_oilCount}L Oil Given!";
            break;

            case ActionType.doubleMoney:
            int money = Convert.ToInt32(_doubleMoney.text);
            money *= 2;
            _doubleMoney.text = money.ToString();
            
            break;
        }
    }

    
    
}

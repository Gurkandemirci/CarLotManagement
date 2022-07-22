using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    public int nextLevelMoney;

    [SerializeField] TMP_Text moneyText;
    public int money;

    void Start()
    {
        money = 0;
        nextLevelMoney = 600;
        moneyText.text = money.ToString();
        instance = this;
    }

    public void AddMoney(int value)
    {
        money += value;
        moneyText.text = money.ToString();
    }
    public void SubtractMoney(int value)
    {
        money -= value;
        moneyText.text = money.ToString();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [Header("--- Настройки Денег ---")]
    public int currentMoney = 1000;
    
    public Text coinTextReference; 

    [Header("--- Цены Мечей ---")]
    public List<int> swordPrices = new List<int>();

    private void Awake()
    {
        UpdateCoinText();
    }

    private void OnValidate()
    {
        UpdateCoinText();
    }

    public void BuyWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= swordPrices.Count)
        {
            return;
        }

        if (ActiveWeapon.Instance == null)
        {
            return;
        }

        int price = swordPrices[weaponIndex];

        if (currentMoney >= price)
        {
            currentMoney -= price;
            UpdateCoinText();
            ActiveWeapon.Instance.EquipWeaponByIndex(weaponIndex);
        }
    }

    private void UpdateCoinText()
    {
        if (coinTextReference != null)
        {
            coinTextReference.text = currentMoney.ToString(); 
        }
    }
}
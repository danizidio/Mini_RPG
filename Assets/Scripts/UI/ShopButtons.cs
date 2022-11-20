using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveLoadPlayerPrefs;

public class ShopButtons : MonoBehaviour
{
    [SerializeField] GameObject _txtAction;

    [SerializeField] EquipmentType _type;

    int _itemNumber;

    #region - Button Events
    public void BuyEquip(int a)
    {
        if(GameBehaviour.instance.PlayerMoney >= a)
        {
            SaveLoad s = new SaveLoad();

            GameObject p = GameObject.FindGameObjectWithTag("Player");

            GameBehaviour.instance.PlayerMoney -= a;

            s.SavingCoins(GameBehaviour.instance.PlayerMoney);

            GameBehaviour.instance.OnTakingCoins();

            if(_type == EquipmentType.ARMOR)
            {
                s.PlayerSaveInt(SaveStrings.ARMOR.ToString(), _itemNumber);
            }

            if (_type == EquipmentType.WEAPON)
            {
                s.PlayerSaveInt(SaveStrings.WEAPON.ToString(), _itemNumber);
            }

            p.GetComponent<PlayerBehaviour>().EquipsToUse();

            ShowInfoUI(true, "THANK YOU!! HOPE YOU LIKE IT!");
        }
        else
        {
            ShowInfoUI(true, "PLEASE COME BACK WHEN YOU CAN AFFORD IT!");
        }
    }

    public void ItemNumber(int a)
    {
        _itemNumber = a;
    }

    public void SellingMiscItems()
    {
        SaveLoad s = new SaveLoad();

        int a = NavigationData.instance.MiscItems * 10;

        GameBehaviour.instance.PlayerMoney += a;

        s.SavingCoins(GameBehaviour.instance.PlayerMoney);

        GameBehaviour.instance.OnTakingCoins();

        ShowInfoUI(true, "TAKE THIS " + a + " COINS, FOR YOUR " + NavigationData.instance.MiscItems + " SOLD ITEMS");
    }

    #endregion
    public void ShowInfoUI(bool show, string txt)
    {
        _txtAction.SetActive(show);

        _txtAction.GetComponentInChildren<TMPro.TMP_Text>().text = txt;
    }
}

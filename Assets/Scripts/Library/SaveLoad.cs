using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SaveLoadPlayerPrefs
{
    public enum SaveStrings
    {
        //GAMEPLAY PREFS
        MONETARY_VALUE,
        HIGHSCORE,
        MISC_ITEMS,
        PLAYER_LEVEL,
        CHARACTER_PREFS,
        
        //CHARACTER CUSTOMIZATION
        HAS_MADE_CHARACTER,
        FEMALE,
        HAIR,
        FACE,
        HAIRCOLOR,
        SKINCOLOR,
        SHIRTCOLOR,
        LEGCOLOR,
        ARMOR,
        WEAPON,

        //MENU PREFS
        BLOOM,
        FILMGRAIN,
        CHROMATIC_ABERRATION,
        VOLUME,
        RESOLUTION,
        FULLSCREEN,
        SFX,
        FIRSTUSE
    }
    public class SaveLoad
    {
        public void SavingCoins(int coinsToSave)
        {
            if (PlayerPrefs.HasKey(SaveStrings.MONETARY_VALUE.ToString())) PlayerSaveInt(SaveStrings.MONETARY_VALUE.ToString(), coinsToSave);
        }

        public int LoadingCoins()
        {
            return PlayerPrefs.GetInt(SaveStrings.MONETARY_VALUE.ToString());
        }

        public void SpendingCoins(int spendCoins)
        {
            if (PlayerPrefs.HasKey(SaveStrings.MONETARY_VALUE.ToString()))
            {
                int a = 0;
                a = PlayerPrefs.GetInt(SaveStrings.MONETARY_VALUE.ToString());

                int total = a - spendCoins;

               PlayerSaveInt(SaveStrings.MONETARY_VALUE.ToString(), total);
            }
        }

        public void SaveScore(int scoreToSave)
        {
            PlayerSaveInt(SaveStrings.HIGHSCORE.ToString(), scoreToSave);
        }

        public int LoadingScore()
        {
            return PlayerPrefs.GetInt(SaveStrings.HIGHSCORE.ToString());
        }

        public bool LoadingBool(string str)
        {
            return PlayerPrefs.GetString(str) == "True" ;
        }
        public void PlayerSaveInt(string str, int value)
        {
            PlayerPrefs.SetInt(str, value);
            PlayerPrefs.Save();
        }
        public void PlayerSaveFloat(string str, float value)
        {
                PlayerPrefs.SetFloat(str, value);
                PlayerPrefs.Save();
        }

        public void PlayerSaveBool(string str, bool value)
        {
            PlayerPrefs.SetString(str, value.ToString());
            PlayerPrefs.Save();
        }

        public void PlayerSaveColor(string str, Color32 clr)
        {
            string colorHex = ColorUtility.ToHtmlStringRGB(clr);

            PlayerPrefs.SetString(str, colorHex.ToString());
            PlayerPrefs.Save();
        }
    }
}

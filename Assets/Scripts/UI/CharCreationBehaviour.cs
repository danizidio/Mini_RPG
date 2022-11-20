using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveLoadPlayerPrefs;

public class CharCreationBehaviour : MonoBehaviour
{
    public static CharCreationBehaviour instance;

    [SerializeField] GameObject _femaleBody, _maleBody;

    [SerializeField] SpriteRenderer[] _femaleHair, _maleHair;
    [SerializeField] SpriteRenderer[] _femaleFace, _maleFace;
    [SerializeField] SpriteRenderer[] _femaleEar, _maleEar;
    [SerializeField] SpriteRenderer[] _femaleSkin, _maleSkin;

    [SerializeField] SpriteRenderer _femaleShirt, _femaleLegs, _maleShirt, _maleLegs;

    [SerializeField] bool _isFemale;

    int _valueHair, _valueFace, _valueBody, _valueShirt, _valueLegs, _valueHairColor;

    SaveLoad s;

    private void Awake()
    {
        instance = this;
    }


    #region - UI Button Events

    public void FirstPersonalization()
    {
        s = new SaveLoad();

        if (!PlayerPrefs.HasKey(SaveStrings.HAS_MADE_CHARACTER.ToString()))
        {
            s.PlayerSaveInt(SaveStrings.HAS_MADE_CHARACTER.ToString(), 1);
        }
    }
    public void GenderChange(bool a)
    {
        s = new SaveLoad();

        if (a)
        {
            _femaleBody.SetActive(true);
            _maleBody.SetActive(false);
        }
        else
        {
            _femaleBody.SetActive(false);
            _maleBody.SetActive(true);
        }

        _isFemale = a;

        s.PlayerSaveBool(SaveStrings.FEMALE.ToString(), _isFemale);
    }
    public void HairChange(int a)
    {
        s = new SaveLoad();

        _valueHair += a;

        if (_valueHair > 2)
        {
            _valueHair = 0;
        }
        if (_valueHair < 0)
        {
            _valueHair = 2;
        }

        if (!_isFemale)
        {
            SelectItem(_maleHair, _valueHair);
        }
        else
        {
            SelectItem(_femaleHair, _valueHair);
        }

        s.PlayerSaveInt(SaveStrings.HAIR.ToString(), _valueHair);
    }
    public void FaceChange(int a)
    {
        s = new SaveLoad();

        _valueFace += a;

        if (_valueFace > 2)
        {
            _valueFace = 0;
        }
        if (_valueFace < 0)
        {
            _valueFace = 2;
        }

        if (!_isFemale)
        {
            SelectItem(_maleFace, _valueFace);
            SelectItem(_maleEar, _valueFace);
        }
        else
        {
            SelectItem(_femaleFace, _valueFace);
            SelectItem(_femaleEar, _valueFace);
        }

        s.PlayerSaveInt(SaveStrings.FACE.ToString(), _valueFace);
    }
    public void HairColor(int a)
    {
        s = new SaveLoad();

        _valueHairColor += a;

        if (_valueHairColor > 2)
        {
            _valueHairColor = 0;
        }
        if (_valueHairColor < 0)
        {
            _valueHairColor = 2;
        }

        if (!_isFemale)
        {
            foreach (var item in _maleHair)
            {
                switch (_valueHairColor)
                {
                    case 0:
                        {
                            item.color = new Color32(255, 100, 150, 255);

                            break;
                        }
                    case 1:
                        {
                            item.color = new Color32(0, 0, 0, 255);

                            break;
                        }
                    case 2:
                        {
                            item.color = new Color32(255, 255, 255, 255);

                            break;
                        }
                }
            }
        }
        else
        {
            foreach (var item in _femaleHair)
            {
                switch (_valueHairColor)
                {
                    case 0:
                        {
                            item.color = new Color32(255, 100, 150, 255);

                            break;
                        }
                    case 1:
                        {
                            item.color = new Color32(0, 0, 0, 255);

                            break;
                        }
                    case 2:
                        {
                            item.color = new Color32(255, 255, 255, 255);

                            break;
                        }
                }
            }
        }
        s.PlayerSaveInt(SaveStrings.HAIRCOLOR.ToString(), _valueHairColor);
    }
    public void SkinColor(int a)
    {
        s = new SaveLoad();

        _valueBody += a;

        if (_valueBody > 2)
        {
            _valueBody = 0;
        }
        if (_valueBody < 0)
        {
            _valueBody = 2;
        }

        if(!_isFemale)
        {
            foreach (var item in _maleSkin)
            {
                switch (_valueBody)
                {
                    case 0:
                        {
                            item.color = new Color32(255, 245, 230, 255);

                            break;
                        }
                    case 1:
                        {
                            item.color = new Color32(185, 141, 141, 255);

                            break;
                        }
                    case 2:
                        {
                            item.color = new Color32(255, 218, 231, 255);

                            break;
                        }
                }
            }
        }
        else
        {
            foreach (var item in _femaleSkin)
            {
                switch (_valueBody)
                {
                    case 0:
                        {
                            item.color = new Color32(255, 245, 230, 255);

                            break;
                        }
                    case 1:
                        {
                            item.color = new Color32(185, 141, 141, 255);

                            break;
                        }
                    case 2:
                        {
                            item.color = new Color32(255, 218, 231, 255);

                            break;
                        }
                }
            }
        }

        s.PlayerSaveInt(SaveStrings.SKINCOLOR.ToString(), _valueBody);
    }
    public void ShirtColor(int a)
    {
        s = new SaveLoad();

        _valueShirt += a;

        if (_valueShirt > 2)
        {
            _valueShirt = 0;
        }
        if (_valueShirt < 0)
        {
            _valueShirt = 2;
        }

        if(!_isFemale)
        {
            switch (_valueShirt)
            {
                case 0:
                    {
                        _maleShirt.color = new Color32(255, 255, 255, 255);

                        break;
                    }
                case 1:
                    {
                        _maleShirt.color = new Color32(207, 38, 41, 255);

                        break;
                    }
                case 2:
                    {
                        _maleShirt.color = new Color32(100, 0, 255, 255);

                        break;
                    }
            }
        }
        else
        {
            switch (_valueShirt)
            {
                case 0:
                    {
                        _femaleShirt.color = new Color32(255, 255, 255, 255);

                        break;
                    }
                case 1:
                    {
                        _femaleShirt.color = new Color32(207, 38, 41, 255);

                        break;
                    }
                case 2:
                    {
                        _femaleShirt.color = new Color32(100, 0, 255, 255);

                        break;
                    }
            }
        }
        s.PlayerSaveInt(SaveStrings.SHIRTCOLOR.ToString(), _valueShirt);
    }
    public void LegsColor(int a)
    {
        s = new SaveLoad();

        _valueLegs += a;

        if (_valueLegs > 2)
        {
            _valueLegs = 0;
        }
        if (_valueLegs < 0)
        {
            _valueLegs = 2;
        }

        if(!_isFemale)
        {
            switch (_valueLegs)
            {
                case 0:
                    {
                        _maleLegs.color = new Color32(255, 255, 255, 255);

                        break;
                    }
                case 1:
                    {
                        _maleLegs.color = new Color32(207, 38, 41, 255);

                        break;
                    }
                case 2:
                    {
                        _maleLegs.color = new Color32(100, 0, 255, 255);

                        break;
                    }
            }
        }
        else
        {
            switch (_valueLegs)
            {
                case 0:
                    {
                        _femaleLegs.color = new Color32(255, 255, 255, 255);

                        break;
                    }
                case 1:
                    {
                        _femaleLegs.color = new Color32(207, 38, 41, 255);

                        break;
                    }
                case 2:
                    {
                        _femaleLegs.color = new Color32(100, 0, 255, 255);

                        break;
                    }
            }
        }
        s.PlayerSaveInt(SaveStrings.LEGCOLOR.ToString(), _valueLegs);
    }
    #endregion

    void SelectItem(SpriteRenderer[] sr, int value)
    {
        for (int n = 0; n < sr.Length; n++)
        {
            if (n != value)
            {
                sr[n].gameObject.SetActive(false);
            }
            else
            {
                sr[n].gameObject.SetActive(true);
            }
        } 
    }

    public void LoadCustomCharacter()
    {
        s = new SaveLoad();

        if (s.LoadingBool(SaveStrings.FEMALE.ToString()))
        {
            _femaleBody.SetActive(true);
            _maleBody.SetActive(false);

            SelectItem(_femaleHair, PlayerPrefs.GetInt(SaveStrings.HAIR.ToString()));

            SelectItem(_femaleFace, PlayerPrefs.GetInt(SaveStrings.FACE.ToString()));
            SelectItem(_femaleEar, PlayerPrefs.GetInt(SaveStrings.FACE.ToString()));

            foreach (var item in _femaleHair)
            {
                switch (PlayerPrefs.GetInt(SaveStrings.HAIRCOLOR.ToString()))
                {
                    case 0:
                        {
                            item.color = new Color32(255, 100, 150, 255);

                            break;
                        }
                    case 1:
                        {
                            item.color = new Color32(0, 0, 0, 255);

                            break;
                        }
                    case 2:
                        {
                            item.color = new Color32(255, 255, 255, 255);

                            break;
                        }
                }
            }

            foreach (var item in _femaleSkin)
            {
                switch (PlayerPrefs.GetInt(SaveStrings.SKINCOLOR.ToString()))
                {
                    case 0:
                        {
                            item.color = new Color32(255, 245, 230, 255);

                            break;
                        }
                    case 1:
                        {
                            item.color = new Color32(185, 141, 141, 255);

                            break;
                        }
                    case 2:
                        {
                            item.color = new Color32(255, 218, 231, 255);

                            break;
                        }
                }
            }

            switch (PlayerPrefs.GetInt(SaveStrings.SHIRTCOLOR.ToString()))
            {
                case 0:
                    {
                        _femaleShirt.color = new Color32(255, 255, 255, 255);

                        break;
                    }
                case 1:
                    {
                        _femaleShirt.color = new Color32(207, 38, 41, 255);

                        break;
                    }
                case 2:
                    {
                        _femaleShirt.color = new Color32(100, 0, 255, 255);

                        break;
                    }
            }

            switch (PlayerPrefs.GetInt(SaveStrings.LEGCOLOR.ToString()))
            {
                case 0:
                    {
                        _femaleLegs.color = new Color32(255, 255, 255, 255);

                        break;
                    }
                case 1:
                    {
                        _femaleLegs.color = new Color32(207, 38, 41, 255);

                        break;
                    }
                case 2:
                    {
                        _femaleLegs.color = new Color32(100, 0, 255, 255);

                        break;
                    }
            }

        }
        else
        {
            _femaleBody.SetActive(false);
            _maleBody.SetActive(true);

            SelectItem(_maleHair, PlayerPrefs.GetInt(SaveStrings.HAIR.ToString()));

            SelectItem(_maleFace, PlayerPrefs.GetInt(SaveStrings.FACE.ToString()));
            SelectItem(_maleEar, PlayerPrefs.GetInt(SaveStrings.FACE.ToString()));

            foreach (var item in _maleHair)
            {
                switch (PlayerPrefs.GetInt(SaveStrings.HAIRCOLOR.ToString()))
                {
                    case 0:
                        {
                            item.color = new Color32(255, 100, 150, 255);

                            break;
                        }
                    case 1:
                        {
                            item.color = new Color32(0, 0, 0, 255);

                            break;
                        }
                    case 2:
                        {
                            item.color = new Color32(255, 255, 255, 255);

                            break;
                        }
                }
            }

            foreach (var item in _maleSkin)
            {
                switch (PlayerPrefs.GetInt(SaveStrings.SKINCOLOR.ToString()))
                {
                    case 0:
                        {
                            item.color = new Color32(255, 245, 230, 255);

                            break;
                        }
                    case 1:
                        {
                            item.color = new Color32(185, 141, 141, 255);

                            break;
                        }
                    case 2:
                        {
                            item.color = new Color32(255, 218, 231, 255);

                            break;
                        }
                }
            }

            switch (PlayerPrefs.GetInt(SaveStrings.SHIRTCOLOR.ToString()))
            {
                case 0:
                    {
                        _maleShirt.color = new Color32(255, 255, 255, 255);

                        break;
                    }
                case 1:
                    {
                        _maleShirt.color = new Color32(207, 38, 41, 255);

                        break;
                    }
                case 2:
                    {
                        _maleShirt.color = new Color32(100, 0, 255, 255);

                        break;
                    }
            }

            switch (PlayerPrefs.GetInt(SaveStrings.LEGCOLOR.ToString()))
            {
                case 0:
                    {
                        _maleLegs.color = new Color32(255, 255, 255, 255);

                        break;
                    }
                case 1:
                    {
                        _maleLegs.color = new Color32(207, 38, 41, 255);

                        break;
                    }
                case 2:
                    {
                        _maleLegs.color = new Color32(100, 0, 255, 255);

                        break;
                    }
            }
        }
    }
}

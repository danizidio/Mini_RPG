using UnityEngine;
using TMPro;

public class GameVersion : MonoBehaviour
{
    TMP_Text _gameVersionText;

    void Start()
    {
        _gameVersionText = this.GetComponent<TMP_Text>();

        SetAppVersion();
    }

    private void SetAppVersion()
    {
        _gameVersionText.text = AppVersion();
    }

    private string AppVersion()
    {
        return "Version: " + Application.version.ToString();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] NpcAttributes _npcAttributes;

    [SerializeField] GameObject _txtAction;
    [SerializeField] GameObject _menuShop;

    PlayerBehaviour p;
    void Talking()
    {
        p.CanMove(false);

        ShowInfoUI(false, "");

        ShowShopUI(true, _npcAttributes.NpcDialogs[Random.Range(0, _npcAttributes.NpcDialogs.Length)]);
    }

    public void ExitChat()
    {
        p.CanMove(true);

        ShowShopUI(false, "");
    }

    public void ShowShopUI(bool show, string txt)
    {
        _menuShop.SetActive(show);

        _menuShop.GetComponentInChildren<TMPro.TMP_Text>().text = txt;
    }
    public void ShowInfoUI(bool show, string txt)
    {
        _txtAction.SetActive(show);

        _txtAction.GetComponentInChildren<TMPro.TMP_Text>().text = txt;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        p = collision.gameObject.GetComponent<PlayerBehaviour>();

        if (p != null)
        {
            collision.GetComponent<PlayerBehaviour>().OnActing += Talking;
            collision.GetComponent<PlayerBehaviour>().OnActing -= collision.GetComponent<PlayerBehaviour>().Attacking;

            ShowInfoUI(true, "TALK");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        p = collision.gameObject.GetComponent<PlayerBehaviour>();

        if (p != null)
        {
            collision.GetComponent<PlayerBehaviour>().OnActing -= Talking;
            ShowInfoUI(false, "");
        }
    }
}

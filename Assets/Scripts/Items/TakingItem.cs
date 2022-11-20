using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingItem : MonoBehaviour
{
    [SerializeField] GameObject _txtAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehaviour p = collision.gameObject.GetComponent<PlayerBehaviour>();

        if (p != null)
        {
            collision.GetComponent<PlayerBehaviour>().OnActing += PickingItem;
            collision.GetComponent<PlayerBehaviour>().OnActing -= collision.GetComponent<PlayerBehaviour>().Attacking;

            ShowInfoUI(true, "TAKE");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        PlayerBehaviour p = collision.gameObject.GetComponent<PlayerBehaviour>();

        if (p != null)
        {
            collision.GetComponent<PlayerBehaviour>().OnActing -= PickingItem;
            ShowInfoUI(false, "TAKE");
        }
    }

    public void ShowInfoUI(bool show, string txt)
    {
        _txtAction.SetActive(show);

        _txtAction.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        _txtAction.GetComponentInChildren<TMPro.TMP_Text>().text = txt;
    }

    void PickingItem()
    {
        NavigationData.instance.TakingMiscItems();

        Destroy(this.gameObject);
    }
}

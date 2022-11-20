using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOrDeactivateGameObject : MonoBehaviour
{
    //FOR ANIMATION EVENT PURPOSES
    public void DestroyingGameObject()
    {
        Destroy(this.gameObject);
    }

    public void DeactivateGameObject()
    {
        this.gameObject.SetActive(false);
    }
}

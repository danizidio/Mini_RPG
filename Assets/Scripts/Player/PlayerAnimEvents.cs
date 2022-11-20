using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    [SerializeField] PlayerBehaviour p;

    public void StopMove()
    {
        if(p.canMove)
        {
            p.CanMove(false);
        }
        else
        {
            p.CanMove(true);
        }
    }

}

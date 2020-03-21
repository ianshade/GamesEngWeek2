using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderBehaviour : MonoBehaviour
{
    public GameObject myGround;

    public Collider2D GetGroundCollider ()
    {
        return myGround.GetComponent<Collider2D>();
    }
}

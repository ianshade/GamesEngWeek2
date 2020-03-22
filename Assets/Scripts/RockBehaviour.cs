using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    private Animator childAnimator;
    private Rigidbody2D myRigidbody;

    void Start()
    {
        childAnimator = GetComponentInChildren<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        transform.GetChild(0).localEulerAngles = -transform.localEulerAngles;
        childAnimator.SetFloat("rotation", -myRigidbody.velocity.x);
    }
}

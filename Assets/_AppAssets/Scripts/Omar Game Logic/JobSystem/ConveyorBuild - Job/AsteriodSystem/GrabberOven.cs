using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberOven : MonoBehaviour
{
    public float burnPeriodInSeconds;
    private float timeCounter;
    public string MovingObjectTag;
    public ConveyorBeltProductionObject currentObjectInOven;
    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
        if (currentObjectInOven != null)
        {
            //Burn time count
            //Burn animation 
            animator.SetTrigger("Burn");
            timeCounter += Time.deltaTime;
            if (timeCounter>=burnPeriodInSeconds)
            {
                currentObjectInOven.conveyorBeltStation.isWaitingAfterEnd = false;
                timeCounter = 0;
                currentObjectInOven = null;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == MovingObjectTag)
        {
            currentObjectInOven = other.gameObject.GetComponent<ConveyorBeltProductionObject>();
        }
    }
    
}

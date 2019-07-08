using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    Rigidbody myRigidbody;
    ConveyorBeltProductionObject myConveyorBeltProductionObject;
    AsteroidInstatiator asteroidInstatiator;
    bool reachedVacum;
    float suckingSpeed=0.1f;
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myConveyorBeltProductionObject = GetComponent<ConveyorBeltProductionObject>();
        asteroidInstatiator=transform.parent.gameObject.GetComponentInChildren<AsteroidInstatiator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!reachedVacum)
        {
            if (Vector3.Distance(asteroidInstatiator.vacumEntrance.position, transform.position) < 0.2f)
            {
                reachedVacum = true;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, asteroidInstatiator.vacumEntrance.position, suckingSpeed+=0.0001f);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position,asteroidInstatiator.vacumCore.position) < 1f)
            {
                Destroy(this.gameObject);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, asteroidInstatiator.vacumCore.position, suckingSpeed);
            }
        }

    }
}

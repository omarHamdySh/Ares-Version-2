using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidInnerController : MonoBehaviour
{
    public GameObject asteroidPartPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Asteroid")
        {
            StartCoroutine("spawnAnAsteroid");
        }
    }
    IEnumerator spawnAnAsteroid() {
        yield return new WaitForSeconds(2);
        var obj = Instantiate(asteroidPartPrefab, transform.parent.parent.position, Quaternion.identity, transform.parent.parent.parent.parent);
        obj.GetComponent<Rigidbody>().isKinematic = false;
        obj.GetComponent<AsteroidController>().enabled = false;
        obj.GetComponent<ConveyorBeltProductionObject>().enabled = true;

    }
}

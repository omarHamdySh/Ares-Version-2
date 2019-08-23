
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceToConsume))]
public class ResourceConsumer
{
    public GameObject consumerGameObject;
    public Resource resource;
    public float consumptionRate;

    public ResourceConsumer(GameObject consumerGameObject)
    {
        this.consumerGameObject = consumerGameObject;
    }
    public void addResource(Resource resource, float consumptionRate)
    {
        //resourcesConsumptionRates.Add(resource, consumptionRate);
        this.resource = resource;
        this.consumptionRate = consumptionRate;
    }


}

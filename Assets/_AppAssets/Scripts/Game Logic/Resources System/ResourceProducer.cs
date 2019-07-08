using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceToProduce))]
public class ResourceProducer
{
    public GameObject ProducerGameObject;
    public Resource resource;
    public float productionRate;
    public ResourceProducer(GameObject ProducerGameObject)
    {
        this.ProducerGameObject = ProducerGameObject;
    }
    public void addResource(Resource resource, float productionRate)
    {
        //resourcesProductionRates.Add(resource, productionRate);
        this.resource = resource;
        this.productionRate = productionRate;
    }
}

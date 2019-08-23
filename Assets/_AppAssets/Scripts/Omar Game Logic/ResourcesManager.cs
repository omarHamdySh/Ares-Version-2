using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Food,
    Electricity,
    Iron
}
public class ResourcesManager : MonoBehaviour
{
    [SerializeField]
    public List<Resource> gameResources = new List<Resource>();
    public List<ResourceConsumer> consumers = new List<ResourceConsumer>();
    public List<ResourceProducer> producers = new List<ResourceProducer>();
    [HideInInspector]
    public bool isCaluclating;
    [HideInInspector]
    public bool isReadyToLateStart;
    // Start is called before the first frame update
    void Start()
    {

    }


    void Init()
    {

    }
    /// <summary>
    /// Checking if the current run of the game is within the testing time:
    /// This will make the isCalculating true at the testing time hence that at the build version
    /// the gameplay states are affecting the resources manager some may be toggle it off.
    /// </summary>
    public void updateEachSecond()
    {
        //if (GameBrain.Instance.testing)
        //{//Don't remove this before reading the summary above the method's body.
        //    isCaluclating = true;
        //}
        if (isCaluclating)
        {

            calculateResourcesConsumption();
            //calculateResourcesProduction();//Very bad performance
            if (GameBrain.Instance.testing)
            {
                //Debug.Log(gameResources[0].valueInPercentage);
            }
        }

    }
    /// <summary>
    /// Loop on all consumers and decrease the resources percentage according to what 
    /// resources each consumer is consuming and at what rate per second does this happens.
    /// </summary>
    /// 
    float counter;
    private void calculateResourcesConsumption()
    {

        //foreach (var resource in gameResources)
        //{
        //    foreach (var consumer in consumers)
        //    {
        //        if (resource.valueInPercentage > 0)
        //        {
        //            if (consumer.resourcesConsumptionRates.ContainsKey(resource))
        //            {
        //                resource.totalConsumptionRate += consumer.resourcesConsumptionRates[resource];
        //            }
        //        }

        //    }
        //}
        foreach (var resource in gameResources)
        {
           
            //if (resource.valueInPercentage > 0)
            //{
            //    resource.valueInPercentage -=
            //     (resource.valueInPercentage - resource.totalConsumptionRate >= 0) ?
            //       resource.totalConsumptionRate : 0;
            //    resource.totalConsumptionRate = 0;
            //}
            List<ResourceConsumer> resourceConsumers = consumers.FindAll(c => c.resource == resource);

            if (resourceConsumers.Count==0)
            {
                continue;
            }
            List<ResourceProducer> resourceProducers = producers.FindAll(c => c.resource == resource);
            float totalProductionsPerCycle = 2 * (30 * resourceProducers.Count); //2 is the number of hours that is needed to complete production cycle 
            float consumptionRatesFactor = (totalProductionsPerCycle / resourceConsumers.Count) / 100;
            float averageConsumtionRates = consumptionRatesFactor *( resourceConsumers.Count/
                (5f-GameBrain.Instance.paceManager.resourcesConsumptionSpeed));
            consumeFromThis(resource, averageConsumtionRates);
            resource.valueInPercentage = (resource.valueInPercentage >= 0) ? resource.valueInPercentage : 0;
        }
    }

    /// <summary>
    /// Loop on all consumers and decrease the resources percentage according to what 
    /// resources each consumer is consuming and at what rate per second does this happens.
    /// </summary>
    //private void calculateResourcesProduction()
    //{
    //    foreach (var resource in gameResources)
    //    {
    //        foreach (var producer in producers)
    //        {
    //            if (producer.resourcesProductionRates.ContainsKey(resource))
    //            {
    //                Room room = LevelManager.Instance.roomManager.getRoomWithGameObject(producer.ProducerGameObject);
    //                if (LevelManager.Instance.roomManager.rooms.Contains(room))
    //                {
    //                    room.roomProductivity = room.roomProductivity * producer.resourcesProductionRates[resource];
    //                }
    //                resource.valueInPercentage =
    //                       (resource.valueInPercentage + producer.resourcesProductionRates[resource] <= 100) ?
    //                        resource.valueInPercentage + producer.resourcesProductionRates[resource] : 100;
    //            }
    //        }
    //    }
    //}

    public void consumeFromThis(Resource resource, float consumptionValue)
    {
        counter++;

        resource.valueInPercentage -= consumptionValue;
            resource.valueInPercentage =(resource.valueInPercentage < 0) ?  0 : resource.valueInPercentage;
            resource.totalConsumptionRate = 0;
    }

    public Resource getResource(ResourceType resourceType)
    {
        foreach (var resource in gameResources)
        {
            if (resource.resourceType == resourceType)
            {
                return resource;
            }
        }
        return null;
    }

    public void OnSecondChange()
    {//Called each real second
        updateEachSecond();
    }
    public void OnMinuteChange()
    {//Called each real Minute

    }
    public void OnGameHourChange()
    {//Called each Game Hour
        counter=0;

    }
    public void OnGameDayChange()
    {// Called each Game Day

    }
}

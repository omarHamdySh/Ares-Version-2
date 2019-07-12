using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltProductionLine : MonoBehaviour
{
    public GameObject nextStation;
    public GameObject startPoint;
    public GameObject endPoint;
    public bool isWaitingAftereStart;
    public bool isWaitingInTheMiddle;
    public bool isWaitingAfterEnd;
    private bool isWaiting;
    public string MovingObjectTag;
    public float MovementSpeed;
    public ConveyorBeltProductionObject objectTopass;
    public Queue<ConveyorBeltProductionObject> productionObjectsQueue = new Queue<ConveyorBeltProductionObject>();

    // Update is called once per frame
    void Update()
    {
        if (productionObjectsQueue.Count != 0)
        {
            productionObjectsQueue.Peek().transform.position = startPoint.transform.position;
            productionObjectsQueue.Dequeue().changeObjectStateTo(conveyorBeltObjectState.start);
        }
        if (isWaiting)
        {
            passToTheNextStation();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == MovingObjectTag)
        {
            collision.collider.gameObject.GetComponent<ConveyorBeltProductionObject>().conveyorBeltStation = this;
            if (isWaitingAftereStart)
            {
                collision.collider.gameObject.GetComponent<ConveyorBeltProductionObject>().changeToStation( conveyorBeltObjectState.pause);
            }
            else
            {
                collision.collider.gameObject.GetComponent<ConveyorBeltProductionObject>().changeToStation(conveyorBeltObjectState.moving);
            }
        }
    }


    public void passToTheNextStation()
    {
        isWaiting = true;
        if (!isWaitingAfterEnd)
        {
            if (nextStation != null)
            {
                objectTopass.conveyorBeltStation = nextStation.GetComponentInChildren<ConveyorBeltProductionLine>();
                nextStation.GetComponentInChildren<ConveyorBeltProductionLine>().productionObjectsQueue.Enqueue(objectTopass);
                GetComponent<BoxCollider>().enabled = false;
                StartCoroutine("reEnableCollider");
            }
            else {
                Destroy(objectTopass.gameObject);
            }
            isWaiting = false;
            objectTopass = null;
        }
    }
    IEnumerator reEnableCollider() {
        yield return new WaitForSeconds(1);
        GetComponent<BoxCollider>().enabled = true;
    }
}

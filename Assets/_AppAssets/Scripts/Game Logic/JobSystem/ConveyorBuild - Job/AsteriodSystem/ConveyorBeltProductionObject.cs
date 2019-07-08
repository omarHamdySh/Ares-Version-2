using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum conveyorBeltObjectState
{
    start,
    moving,
    pause,
    end
}
public class ConveyorBeltProductionObject : MonoBehaviour
{
    public ConveyorBeltProductionLine conveyorBeltStation;
    public conveyorBeltObjectState conveyorBeltState;
    public bool isNewStation;
    // Start is called before the first frame update
    //public ConveyorBeltProductionObject(ConveyorBeltProductionLine conveyorBeltStation, conveyorBeltObjectState conveyorBeltState)
    //{
    //    this.conveyorBeltStation = conveyorBeltStation;
    //    this.conveyorBeltState = conveyorBeltState;
    //}

    private void FixedUpdate()
    {
        updateObjectState();
    }
    public void changeObjectStateTo(conveyorBeltObjectState state)
    {
        conveyorBeltState = state;
        updateObjectState();
    }

    public void changeToStation(conveyorBeltObjectState conveyorBeltState)
    {
        this.conveyorBeltState = conveyorBeltState;
    }
    public void updateObjectState()
    {
        switch (conveyorBeltState)
        {
            case conveyorBeltObjectState.start:
                gameObject.SetActive(true);
                StartCoroutine("resetFlag");
                break;
            case conveyorBeltObjectState.moving:
                if (conveyorBeltStation.isWaitingInTheMiddle)
                {
                    conveyorBeltState = conveyorBeltObjectState.pause;
                    break;
                }
                transform.position = Vector3.MoveTowards(transform.position, conveyorBeltStation.endPoint.transform.position, conveyorBeltStation.MovementSpeed);
                if (Mathf.Abs(transform.position.x - conveyorBeltStation.endPoint.transform.position.x) < 0.2f& !isNewStation)
                {
                    conveyorBeltStation.objectTopass = this;
                    conveyorBeltState = conveyorBeltObjectState.end;
                    conveyorBeltStation.passToTheNextStation();
                    GetComponent<Collider>().enabled = true;
                }
                break;
            case conveyorBeltObjectState.end:
                gameObject.SetActive(false);
                isNewStation = true;
                break;
            case conveyorBeltObjectState.pause:
                break;
        }
    }
    IEnumerator resetFlag()
    {
        yield return new WaitForSeconds(4);
        isNewStation = false;
    }
}
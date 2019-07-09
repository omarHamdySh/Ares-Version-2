using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomEntity : MonoBehaviour
{
    public Slot mySlot; //Line 7 for git merging.
    public GameObject roomGameObject;
    public GameObject leftEntrance;
    public GameObject rightEntrance;
    public JobPathFinder[] jobPathFinders;
    public productionJobType productionJobType;
    public CapsuleProcessesData DebuggingUI;
    public int NumOfCharInRoom;
    public bool IsFirstTime = true;

    //----------------------------------------------------
    public GameObject gemButton;
    public List<GameObject> lights;

    public void Start()
    {
        roomGameObject = this.gameObject.transform.parent.gameObject;
        jobPathFinders = GetComponentsInChildren<JobPathFinder>();
        LevelManager.Instance.roomManager.getRoomWithGameObject(roomGameObject).
            productionJobType = this.productionJobType;
    }
    /// <summary>
    ///  - This method can be called from the characterEntity in order to determine the path
    /// from the entrance to the job.
    /// Will send to it the entrance that the character has used to enter the room form, and his job.
    ///  - This method can be called from the characterEntity in order to determine the path
    /// from the entrance to the job.
    /// Will send to it the entrance that the character should exit the room form and his job.
    /// </summary>
    /// <param name="job"></param>
    /// <param name="entrance"></param>
    /// <returns></returns>
    public static JobPathFinder getJobPathObject(Job job, Character character, JobPathFinder[] jobPathFinders)
    {

        foreach (var jobPathFinder in jobPathFinders)
        {
            if (jobPathFinder.job == job &&
                character.containerEntrance == jobPathFinder.entrancePos)
            {
                return jobPathFinder;
            }
        }
        return null;
    }

    public static JobPathFinder getInnerPathToRestPoints(Job job, Character character, JobPathFinder[] jobPathFinders)
    {

        foreach (var jobPathFinder in jobPathFinders)
        {
            if (character.containerEntrance == jobPathFinder.entrancePos)
            {
                return jobPathFinder;
            }
        }
        return null;
    }

    public void AddCharCountToRoom()
    {
        NumOfCharInRoom++;
        if (!IsFirstTime)
        {
            PlayerPrefs.SetInt(transform.parent.name + " CharNum", NumOfCharInRoom);
        }
    }

    public void SubCharCountToRoom()
    {
        NumOfCharInRoom--;
        PlayerPrefs.SetInt(transform.parent.name + " CharNum", NumOfCharInRoom);
    }

    /// <summary>
    /// You suppose to use a method called "addResourceLoad" in the room class in order to add the new resources load to the current value.
    /// </summary>
    public void showGemUI()
    {
        if (gemButton != null)
        {
            gemButton.gameObject.SetActive(true);
        }

    }

    public void collectGem()
    {

        LevelManager.Instance.roomManager.getRoomWithGameObject(roomGameObject).addResourceLoad();
        gemButton.gameObject.SetActive(false);
    }

}

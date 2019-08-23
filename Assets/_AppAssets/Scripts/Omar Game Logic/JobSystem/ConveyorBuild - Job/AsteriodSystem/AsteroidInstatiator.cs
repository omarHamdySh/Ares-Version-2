using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidInstatiator : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public Job GrabberRoomJob;
    public float spawnPeriodInSeconds;
    public float secondsCounter;
    public Transform vacumEntrance;
    public Transform vacumCore;
    // Start is called before the first frame update
    void Start()
    {
        JobEntity thisRoomJobEntity = GetComponentInParent<JobEntity>();
        foreach (var job in thisRoomJobEntity.roomJobs)
        {
            if (job.jobAnimation == CharacterAnimationsState.Job1)
            {
                GrabberRoomJob = job;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GrabberRoomJob.jobState == JobState.Occupied)
        {
            secondsCounter+=Time.deltaTime;
            if (secondsCounter>=spawnPeriodInSeconds)
            {
                spawnAsteroid();
                secondsCounter = 0;
            }
        }
        else {

        }
    }

    private void spawnAsteroid()
    {
        Instantiate(asteroidPrefab, transform.GetChild(Random.Range(0, transform.childCount - 1)).position, Quaternion.identity, transform.parent);
    }
}

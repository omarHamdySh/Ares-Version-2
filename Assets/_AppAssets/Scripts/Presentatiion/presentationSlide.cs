using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class presentationSlide : MonoBehaviour
{
    public UnityEvent OnThisActivation;
    public bool isOnlyActive;
    public List<GameObject> defaultDeactivated;
    public bool isGameplayPause;
    public bool isLookingAt;
    public bool isFollowing;
    public GameObject target;
    public CinemachineVirtualCamera vCam;
    // Start is called before the first frame update

    public void Update()
    {
        if (this.gameObject.activeInHierarchy && !isOnlyActive)
        {
            OnThisActivation.Invoke();
            isOnlyActive = true;
            if (isGameplayPause)
            {
                GameBrain.Instance.gameplayFSMManager.pauseGame();
            }
        }
        if (isLookingAt)
        {
            vCam.m_LookAt = target.transform;
        }
        if (isFollowing)
        {
            vCam.m_Follow = target.transform;
        }

    }
    public void OnDeactivation()
    {
        if (this.gameObject.activeInHierarchy)
        {
            foreach (var obj in defaultDeactivated)
            {
                obj.SetActive(false);
            }
            if (isGameplayPause)
            {
                GameBrain.Instance.gameplayFSMManager.resumeGame();
            }

            if (isLookingAt)
            {
                vCam.m_LookAt = null;
            }

            if (isFollowing)
            {
                vCam.m_Follow = null;
            }
        }

    }
}
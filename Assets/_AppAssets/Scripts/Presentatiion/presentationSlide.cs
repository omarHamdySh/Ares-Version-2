using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class presentationSlide : MonoBehaviour
{
    public UnityEvent OnThisActivation;
    public bool isOnlyActive;
    public List<GameObject> defaultDeactivated;
    public bool isGameplayPause;
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
    }
    public void OnDeactivation() {
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
        }
    }
}

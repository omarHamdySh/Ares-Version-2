using UnityEngine;
using UnityEngine.Events;

public class PresentationButton : MonoBehaviour
{

    public UnityEvent onSpacebar;
    public UnityEvent onReturn;
    public TimlineController timeLineController;
 
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            resumePresentation();
            if (onSpacebar != null)
            {
                resumePresentation();
                timeLineController.currentTimeline++;
                onSpacebar.Invoke();

            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            resumePresentation();
            if (onReturn != null)
            {
                timeLineController.currentTimeline--;
                onReturn.Invoke();
            }
        }
    }

    private static void resumePresentation()
    {
        if (!LevelManager.Instance.isPresentationModeOn())
        {
            LevelManager.Instance.turnPresentationModeOn();
        }
    }
}
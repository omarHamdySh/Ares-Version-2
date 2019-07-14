using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomGenerateTrainAnim : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] animControllers;
    [SerializeField] private Animator[] animBtnObj;
    [SerializeField] private RoomEntity trainingRoom;

    [SerializeField] private RuntimeAnimatorController charTrainAnime;
    [SerializeField] private Button[] animBtns;
    [SerializeField] private GameObject Fire;
    [SerializeField] private Vector3[] firePos;

    #region Tutorial
    [SerializeField] private FixTextMeshPro tutorialTxt;
    [SerializeField] private List<string> arTutorial;
    [SerializeField] private List<string> enTutorial;
    [SerializeField] private float textDelay;
    [SerializeField] private UIElement animChoser;
    [SerializeField] private UIElement animChoserBtns;

    private int lineIndex;

    private bool isArabic;
    private bool isTutorialTextTimerRun;
    #endregion

    GameObject charcter;

    public void GenerateRandome()
    {
        List<int> objs = new List<int>();
        for (int i = 0; i < animControllers.Length; i++)
        {
            objs.Add(i);
        }

        charcter = LevelManager.Instance.roomManager.getRoomWithGameObject(trainingRoom.transform.parent.gameObject)
            .roomJobs[1].jobHolder.characterGameObject;
        if (charcter)
        {
            charcter.GetComponent<Animator>().runtimeAnimatorController = charTrainAnime;
            charcter.GetComponent<TrainingAnimationsManager>().randomGenerateTrain = this;

            int index;

            for (int i = 0; i < animControllers.Length; i++)
            {
                // Randomize the animation controller
                index = Random.Range(0, objs.Count);
                animBtnObj[i].runtimeAnimatorController = animControllers[objs[index]];
                int slot = objs[index] + 1;

                // Add listener to button
                animBtns[i].onClick.RemoveAllListeners();
                animBtns[i].onClick.AddListener(() => charcter.GetComponent<TrainingAnimationsManager>().runThisAnimation(slot));
                animBtns[i].onClick.AddListener(() => animChoser.SwitchVisibility());

                // Remove the index from the list
                objs.Remove(objs[index]);
            }
        }
    }

    #region Tutorial 1

    public void StartTutorial()
    {
        isArabic = true; /*(PlayerPrefs.GetString("Lang").Equals("ar")) ? true : false;*/
        isTutorialTextTimerRun = true;
        lineIndex = 0;
        Fire.SetActive(true);
        StartCoroutine(TutorialTimer());
    }

    IEnumerator TutorialTimer()
    {
        while (isTutorialTextTimerRun)
        {
            if (lineIndex != 0)
            {
                yield return new WaitForSeconds(textDelay);
            }

            tutorialTxt.text = (isArabic) ? arTutorial[lineIndex] : enTutorial[lineIndex];
            lineIndex++;
            if (lineIndex >= arTutorial.Count)
            {
                isTutorialTextTimerRun = false;
                animChoser.SwitchVisibility();
            }
        }
    }

    public void ShowResult(CharacterTrainingAnimationsState animationState)
    {
        charcter.transform.GetChild(4).gameObject.SetActive(true);
        charcter.transform.GetChild(4).localPosition = new Vector2(firePos[((int)animationState) - 1].x, firePos[((int)animationState) - 1].y);
        charcter.transform.GetChild(4).rotation = Quaternion.Euler(
            firePos[((int)animationState) - 1].z,
            charcter.transform.GetChild(4).rotation.eulerAngles.y,
            charcter.transform.GetChild(4).rotation.eulerAngles.z
            );
        if (animationState == CharacterTrainingAnimationsState.Correct)
        {
            tutorialTxt.text = "صحيح لقد اتقنت التدريب";
            StartCoroutine(WaitToSeeAnim(5.0f, true));
        }
        else
        {
            tutorialTxt.text = "خطا سيعاد التدريب لك مجددا";
            StartCoroutine(WaitToSeeAnim(5.0f, false));
        }

    }

    IEnumerator WaitToSeeAnim(float delay, bool isCorrect)
    {
        yield return new WaitForSeconds(delay);

        if (isCorrect)
        {
            tutorialTxt.text = "";
            Fire.SetActive(false);
            RestAnimToIdle();
            animChoserBtns.SwitchVisibility();
        }
        else
        {
            RestAnimToIdle();
            tutorialTxt.text = (isArabic) ? arTutorial[arTutorial.Count - 1] : enTutorial[arTutorial.Count - 1];
            GenerateRandome();
            animChoser.SwitchVisibility();
        }
        charcter.transform.GetChild(4).gameObject.SetActive(false);
    }

    public void RestAnimToIdle()
    {
        if (charcter)
        {
            charcter.GetComponent<TrainingAnimationsManager>().runThisAnimation(0);
        }
    }
    #endregion
}

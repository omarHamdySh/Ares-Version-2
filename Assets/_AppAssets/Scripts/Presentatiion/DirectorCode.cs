using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class DirectorCode : MonoBehaviour {

    public Image pauseMenuPanel;
    public Text slideNumberText;

    [Header("Assets")]
    public List<PlayableDirector> playableDirectors;
    public TimelineAsset[] timeLinesInOrder;
    private PlayableDirector director;
    private int slideNumber;
    bool prevPresed = false;

    private void Start()
    {
        director = GetComponent<PlayableDirector>();
        director.playableAsset = null;
        if (slideNumberText)
        {
            slideNumberText.text = "0";
        }

        slideNumber = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("next");
            NextSlide();
        }
        else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevSlide();
            Debug.Log("Prev");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuPanel)
            {
                pauseMenuPanel.gameObject.SetActive(!pauseMenuPanel.IsActive());
            }
        }
    }

    void NextSlide()
    {
        if(!prevPresed)
        {
            if (slideNumber < timeLinesInOrder.Length-1)
            {
                slideNumber++;
            }
            else
            {
                slideNumber = timeLinesInOrder.Length - 1;
                Debug.LogWarning("This is the last slide you can not go further");
            }

            director.playableAsset = timeLinesInOrder[slideNumber];
            if (slideNumberText)
            {
                slideNumberText.text = slideNumber.ToString();
            }

        }

        director.Play();
    }

    void PrevSlide()
    {

        if (slideNumber < 0)
        {
            slideNumber = 0;
        }
        else
        {
            slideNumber--;
        }

        director.playableAsset = timeLinesInOrder[slideNumber];
        if (slideNumberText)
        {
            slideNumberText.text = slideNumber.ToString();
        }
        prevPresed = true;
    }

}

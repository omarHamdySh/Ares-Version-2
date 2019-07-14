using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer cutscene;

    private bool check;

    private void Start()
    {
        cutscene.loopPointReached += EndVideo;
    }

    private void EndVideo(VideoPlayer player)
    {
        if (!cutscene.isPlaying && !check)
        {
            check = true;
            ZUIManager.Instance.OpenMenu("LoadingMenuPanel");
            GetComponent<UIManager>().LoadLevel(2);
        }
    }
}

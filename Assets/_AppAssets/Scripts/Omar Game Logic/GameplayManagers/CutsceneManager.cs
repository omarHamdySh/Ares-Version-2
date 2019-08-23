

public class CutsceneManager : IGameplayEventManager
{
    CutScene currentCutscene;
    float cutSceneNumber;
    public bool cutsceneFinished;

    public CutScene CutScene
    {
        get => default;
        set
        {
        }
    }

    public void fireEvent(IGameplayEvent eventObj)
    {
        GameBrain.Instance.gameplayFSMManager.changeToCutSceneState();
        currentCutscene = (CutScene)eventObj;
        currentCutscene.startEvent();
        //Gameplay state change
    }


}

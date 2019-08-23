using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationCustomStateMachine : StateMachineBehaviour
{ 
    public void startGame()
    { 
        SceneManager.LoadScene(1);
    }
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateMove(animator, stateInfo, layerIndex);
        startGame();
    }
}

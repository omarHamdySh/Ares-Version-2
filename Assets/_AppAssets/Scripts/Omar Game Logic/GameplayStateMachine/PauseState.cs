﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : MonoBehaviour, IGameplayState
{
    GameplayState stateName = GameplayState.PauseState;
    /// <summary>
    /// Declaration of dynamic variables for surving the logic goes here.
    /// will be populated here by the GameplayFSMManager itself that will declare this place at the first place.
    /// Eg.
    ///     GameplayFSMManager
    /// </summary>
    public GameplayFSMManager gameplayFSMManager;

    public void OnStateEnter()
    {
        //movementController.followPath();
        GameBrain.Instance.logMessage(this.ToString());
        GameBrain.Instance.resourcesManager.isCaluclating = false;
        GameBrain.Instance.timeManager.isUpdating = false;
        LevelManager.Instance.GetComponent<TouchFSMController>().enabled = false;
    }
    /// <summary>
    /// Logic of exiting the state goes here.
    ///  Eg.
    ///     pop the currentstate
    ///     push the next state
    /// </summary>
    public void OnStateExit()
    {
        /// <summary>
        /// Logic of exiting the state goes here.
        /// Eg.    
        ///     pop the currentstate
        ///     push the next state
        /// other logic related to exiting the this state also goes here
        /// </summary>
        //movementController.steeringScript = null;
        //movementController.removeSteeringScript();
        LevelManager.Instance.GetComponent<TouchFSMController>().enabled = true;
        GameBrain.Instance.resourcesManager.isCaluclating = true;
        GameBrain.Instance.timeManager.isUpdating = true;
    }

    public void OnStateUpdate()
    {
        //Follow patroling path route.
    }
    string ToString()
    {
        return stateName.ToString();
    }

    public GameplayState GetStateName()
    {
        return stateName;
    }
}

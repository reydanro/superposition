using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public GoalController[] goals;

    public void OnGoalStateDidUpdate(GoalController goal)
    {
        if (!Array.Exists(goals, goal => goal.isTriggered == false))
        {
            Debug.Log("You beat the level");
            GlobalControl.Instance.GotoNextLevel();
        }
    }

}

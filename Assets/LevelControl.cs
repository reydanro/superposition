using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public int goalCount;
    private bool[] goalStates;
    // Start is called before the first frame update
    void Start()
    {
        goalStates = new bool[goalCount];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGoalState(int goalIndex, bool state)
    {
        goalStates[goalIndex] = state;

        if (!Array.Exists(goalStates, state => state == false))
        {
            Debug.Log("You beat the level");
            GlobalControl.Instance.GotoNextLevel();
        }
    }

}

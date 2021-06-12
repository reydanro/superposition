using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    private float playerHealth = 100;
    public GoalController[] goals;

    public void OnGoalStateDidUpdate(GoalController goal)
    {
        if (!Array.Exists(goals, goal => goal.isTriggered == false))
        {
            Debug.Log("You beat the level");
            GlobalControl.Instance.GotoNextLevel();
        }
    }

    public void DealPlayerDamage(DamageObstacle obstacle, float damage)
    {
        playerHealth -= damage;
        Debug.Log("Health="+playerHealth);
        if (playerHealth <= 0)
        {
            Debug.LogWarning("Player Died!!!!");
        }
    }
}

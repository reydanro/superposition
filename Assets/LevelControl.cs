using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    private float playerHealth = 100;
    public GoalController[] goals;
    public Transform[] players;

    private bool isVictoryAnimation = false;
    private Transform[] cameraBoundingMarkers;

    public void Start()
    {
        cameraBoundingMarkers = Array.FindAll<Transform>(Camera.main.GetComponentsInChildren<Transform>(), component => component.name.StartsWith("Marker"));
    }
    public void OnGoalStateDidUpdate(GoalController goal)
    {
        if (!Array.Exists(goals, goal => goal.isTriggered == false))
        {
            Debug.Log("You beat the level");
            GetComponent<AudioSource>().Play();
            isVictoryAnimation = true;
        }
    }

    public void DealPlayerDamage(DamageObstacle obstacle, float damage)
    {
        if (isVictoryAnimation)
        {
            return;
        }

        playerHealth -= damage;
        Debug.Log("Health="+playerHealth);
        if (playerHealth <= 0)
        {
            Debug.LogWarning("Player Died!!!!");
            GlobalControl.Instance.HandleDeath();
        }
    }

    public void Update()
    {
        if (isVictoryAnimation)
        {
            Time.timeScale = Math.Max(Time.timeScale * 0.987f, 0.15f);
            float step = 20 * Time.deltaTime;
            foreach (Transform marker in cameraBoundingMarkers)
            {
                marker.position = Vector3.MoveTowards(marker.position, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10)), step);

                if (marker.position.x == 0)
                {
                    Time.timeScale = 1.0f;

                    GlobalControl.Instance.GotoNextLevel();
                    isVictoryAnimation = false;
                    return;
                }
            }

            foreach (Transform player in players)
            {
                if (player.position.x != 0 || player.position.y != 0)
                {
                    player.position = Vector3.MoveTowards(player.position, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10)), step);
                }
            }
        }
    }
}

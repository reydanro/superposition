using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public bool isTriggered;

    private LevelControl lc;

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        lc = GameObject.Find("LevelMaster").GetComponent<LevelControl>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggered = true;
        lc.OnGoalStateDidUpdate(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false;
        lc.OnGoalStateDidUpdate(this);
    }
}

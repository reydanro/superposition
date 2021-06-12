using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{

    public int index;
    private LevelControl lc;
    // Start is called before the first frame update
    void Start()
    {
        lc = GameObject.Find("LevelMaster").GetComponent<LevelControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lc.UpdateGoalState(index, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        lc.UpdateGoalState(index, false);
    }
}

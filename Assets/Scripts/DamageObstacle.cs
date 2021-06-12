using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObstacle : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private LevelControl level;

    // Start is called before the first frame update
    void Start()
    {
        level = GameObject.Find("LevelMaster").GetComponent<LevelControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player == null)
        {
            return;
        }
        level.DealPlayerDamage(this, damage);
    }
}

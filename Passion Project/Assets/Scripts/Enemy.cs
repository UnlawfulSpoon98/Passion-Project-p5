using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour

{
    private Transform Player;
    public float speed;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
      if (Vector3.Distance(Player.position, transform.position) <= range)
        {
           transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        }

    }
}

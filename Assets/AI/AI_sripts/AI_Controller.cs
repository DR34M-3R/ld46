using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour
{
    [Header (" Точка от которой будет идти патруль  ")]
    public Transform Point;
    public float PositionPatrol = 5;
    public float Move_Speed = 3f;
    [Tooltip("радиус агра на игрока")]
    public float AngryZona;
    bool MovingRight;
    Transform Player;
    [Space]
    

    bool isWalking = false;
    bool isAngry = false;
    bool onGoBack = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(Point.position.x, Point.position.y)) < PositionPatrol && isAngry == false)
        {
            isWalking = true; 
        }

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(Player.position.x, Player.position.y)) < AngryZona)
        {
            isAngry = true;
            isWalking = false;
            onGoBack = false;
        }

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(Player.position.x, Player.position.y)) > AngryZona)
        {
            onGoBack = true;
            isAngry = false;
        }
        if(isWalking ==true)
        {
            walk();
        }
        else if(isAngry==true )
        {
            Angry();
        }
        else if(onGoBack == true )
        {
            WalkingBack();
        }

    }


    void walk()
    {
        if (transform.position.x > Point.position.x + PositionPatrol)
        {
            MovingRight = false;
        }
        else if (transform.position.x < Point.position.x - PositionPatrol)
        {
            Debug.Log("start");
        MovingRight = true;
        }
        if (MovingRight)
            transform.position = new Vector2(transform.position.x + (Move_Speed * Time.deltaTime), transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - (Move_Speed * Time.deltaTime), transform.position.y);

    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.position, Move_Speed * Time.deltaTime);


	}

    void WalkingBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, Point.position, Move_Speed * Time.deltaTime);


    }
}

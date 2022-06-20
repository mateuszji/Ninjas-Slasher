using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player playerTarget;
    private float movementSpeed;
    private Vector2 targetPos;
    public enum Enemies { Red, Green, Yellow };
    /*
     *
     *  Red = flying, random position target
     *  Green = running fast, random player target
     *  Yellow = walking slow, random player target
     * 
    */
    public Enemies enemyType;
    private void Start()
    {
        int player = Random.Range(0, PlayersManager.Instance.players.Length);
        playerTarget = PlayersManager.Instance.players[player];
        targetPos = transform.position;

        if (enemyType == Enemies.Red)
            movementSpeed = 2.0f;
        if (enemyType == Enemies.Green)
            movementSpeed = 2.0f;
        if (enemyType == Enemies.Yellow)
            movementSpeed = 1.0f;
    }

    private void Update()
    {
        if(enemyType == Enemies.Red)
        {
            this.transform.position = Vector2.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);
            
            if((Vector2)transform.position == targetPos)
                targetPos = ChooseNewTargetPos();
        }
        else
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, playerTarget.transform.position, movementSpeed * Time.deltaTime);
        }

        if (this.transform.position.x > playerTarget.transform.position.x)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        else
            transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private Vector2 ChooseNewTargetPos()
    {
        Vector2 newTarget = new Vector2(Random.Range(-13, 13), Random.Range(-7, 4));

        return newTarget;
    }
}

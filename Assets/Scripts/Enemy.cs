using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player playerTarget;
    [SerializeField]
    private float movementSpeed;
    private void Start()
    {
        int player = Random.Range(0, PlayersManager.Instance.players.Length);
        playerTarget = PlayersManager.Instance.players[player];
    }

    private void Update()
    {
        this.transform.position  = Vector2.MoveTowards(this.transform.position, playerTarget.transform.position, movementSpeed * Time.deltaTime);

        if (this.transform.position.x > playerTarget.transform.position.x)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        else
            transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}

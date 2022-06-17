using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    #region Singleton

    private static Chain _instance;
    public static Chain Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

    private LineRenderer lr;
    private EdgeCollider2D col;
    private bool isSpawned = false;

    [Range(1,10), SerializeField] private float maxDistance;
    private void Start()
    {
        if (!lr) lr = GetComponent<LineRenderer>();
        if (!col) col = GetComponent<EdgeCollider2D>();

        SetStatus(false);
    }
    private void FixedUpdate()
    {
        if(isSpawned)
        {
            if(PlayersManager.Instance.GetDistanceBetweenPlayers() <= maxDistance)
            {
                ChangePosition();

                SetColliders();
            }
            else
            {
                SetStatus(false);
            }
        }
    }

    public void ChainCheck()
    {
        bool chain = true;

        for (int i = 0; i < PlayersManager.Instance.players.Length; i++)
        {
            if(!PlayersManager.Instance.players[i].spawnedChain)
            {
                chain = false;
                isSpawned = false;
                SetStatus(false);
                break;
            }

        }

        if(chain)
        {
            if (PlayersManager.Instance.GetDistanceBetweenPlayers() > maxDistance)
            {
                Debug.Log("too far!");
            }
            else
            {
                isSpawned = true;
                SetStatus(true);
            }
        }
    }

    private void SetStatus(bool status)
    {
        if(status == false)
        {
            lr.SetPosition(0, new Vector3(0, 0, -10));
            lr.SetPosition(1, new Vector3(0, 0, -10));
        }
        lr.enabled = status;
        col.enabled = status;
    }

    private void ChangePosition()
    {
        for (int i = 0; i < PlayersManager.Instance.players.Length; i++)
            lr.SetPosition(i, PlayersManager.Instance.players[i].chainTarget.position);

        this.transform.position = (lr.GetPosition(0) + lr.GetPosition(1)) / 2;
    }

    private void SetColliders()
    {
        List<Vector2> edges = new List<Vector2>();
        for (int p = 0; p < lr.positionCount; p++)
        {
            Vector3 lrP = lr.GetPosition(p);
            lrP = transform.InverseTransformPoint(lrP);
            edges.Add(new Vector2(lrP.x, lrP.y));
        }
        col.SetPoints(edges);
    }
}

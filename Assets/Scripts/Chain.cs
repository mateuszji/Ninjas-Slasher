using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField]
    private Material chainDefault, chainTooLongDistance;
    private GameObject tooFarText;

    [HideInInspector] public float maxDistance;

    private bool spawnedChain;
    private void Start()
    {
        if (!lr) lr = GetComponent<LineRenderer>();
        if (!col) col = GetComponent<EdgeCollider2D>();
        if (!tooFarText) tooFarText = GameObject.Find("ChainTooLong");
        spawnedChain = false;
        
        col.enabled = false;
        lr.enabled = false;

        tooFarText.SetActive(false);
    }
    private void FixedUpdate()
    {
        if(spawnedChain)
        {
            ChangePosition();
            SetColliders();

            if (PlayersManager.Instance.GetDistanceBetweenPlayers() <= maxDistance)
            {
                tooFarText.SetActive(false);
                lr.material = chainDefault;
                col.enabled = true;
            }
            else
            {
                tooFarText.SetActive(true);
                lr.material = chainTooLongDistance;
                col.enabled = false;
            }
        }
    }

    public void ChainCheck()
    {
        if (!lr) return;

        bool chain = true;

        for (int i = 0; i < PlayersManager.Instance.players.Length; i++)
        {
            if(!PlayersManager.Instance.players[i].spawnedChain)
            {
                chain = false;
                break;
            }
        }

        if (chain)
            spawnedChain = true;
        else
        {
            tooFarText.SetActive(false);
            lr.SetPosition(0, new Vector3(0, 0, -10));
            lr.SetPosition(1, new Vector3(0, 0, -10));
            spawnedChain = false;
        }

        lr.enabled = spawnedChain;
        col.enabled = spawnedChain;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 8) // Enemy
        {
            ParticleSystem destroyEffect = Instantiate(EnemiesManager.Instance.destroyEffect, other.transform.position, Quaternion.identity);
            ParticleSystem.MainModule ma = destroyEffect.main;
            Enemy.Enemies enemyType = other.gameObject.GetComponent<Enemy>().enemyType;
            if (enemyType == Enemy.Enemies.Red)
                ma.startColor = Color.red;
            if (enemyType == Enemy.Enemies.Green)
                ma.startColor = Color.green;
            if (enemyType == Enemy.Enemies.Yellow)
                ma.startColor = Color.yellow;

            Destroy(destroyEffect, 3f);
            Destroy(other.gameObject);

            float points = 100 + ((maxDistance - PlayersManager.Instance.GetDistanceBetweenPlayers()) * 10);
            GameManager.Instance.AddScore((int)points);
        }
    }
}

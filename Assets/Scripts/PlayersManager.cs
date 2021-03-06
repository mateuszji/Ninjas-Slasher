using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    #region Singleton

    private static PlayersManager _instance;
    public static PlayersManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

    [HideInInspector]
    public Player[] players;
    [SerializeField]
    private GameObject[] playerPrefabs;
    [SerializeField]
    private GameObject chainPrefab;
    [SerializeField, Range(1, 10)] private float maxChainDistance;

    public float playerSpeed = 7.5f;
    void Start()
    {
        players = new Player[2];
        for(int i = 0; i < players.Length; i++)
        {
            int skinID;
            if (i == 0)
                skinID = ConfigManager.Instance.skinNinjaWASD;
            else
                skinID = ConfigManager.Instance.skinNinjaArrows;

            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(100, Screen.width-100), Random.Range(100, Screen.height-100), 10));
            GameObject newPlayer = Instantiate(playerPrefabs[skinID], screenPosition, Quaternion.identity);
            players[i] = newPlayer.GetComponent<Player>();
        }

        players[0].playerType = Player.Players.WSAD;
        players[0].gameObject.name = "Player_WSAD";

        players[1].playerType = Player.Players.Arrows;
        players[1].gameObject.name = "Player_Arrows";

        GameObject newChain = Instantiate(chainPrefab, Vector3.zero, Quaternion.identity);
        newChain.name = "Chain";
        newChain.GetComponent<Chain>().maxDistance = maxChainDistance;
    }

    public Vector3 GetCenterBetweenPlayers()
    {
        return Vector3.Lerp(players[0].GetComponent<CapsuleCollider2D>().bounds.center, players[1].GetComponent<CapsuleCollider2D>().bounds.center, 0.5f);
    }

    public float GetDistanceBetweenPlayers()
    {
        return Vector3.Distance(players[0].GetComponent<CapsuleCollider2D>().bounds.center, players[1].GetComponent<CapsuleCollider2D>().bounds.center);
    }
}

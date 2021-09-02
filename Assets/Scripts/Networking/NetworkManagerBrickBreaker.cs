using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerBrickBreaker : NetworkManager
{
    public Transform topPaddleSpawn;
    public Transform bottomPaddleSpawn;
    public GameObject ball;

    public List<GameObject> players = new List<GameObject>();

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? topPaddleSpawn : bottomPaddleSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
        players.Add(player);

        // spawn ball if two players
        if (numPlayers == 2)
        {
            PlayerMovement playerScript = players[0].GetComponent<PlayerMovement>();

            ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"), playerScript.ballLaunchLocation.position, Quaternion.identity);
            NetworkServer.Spawn(ball);
            ball.GetComponent<Ball>().inLauncher = true;

            ball.GetComponent<Ball>().mainPlayer = playerScript;

            players[0].GetComponent<PlayerMovement>().ballToLaunch = ball;
            GameManager.instance.ball = ball;

            ResetLevel();
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // destroy ball
        if (ball != null)
            NetworkServer.Destroy(ball);

        players.Remove(conn.identity.gameObject);

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }

    public void ResetLevel()
    {
        if (NetworkClient.isHostClient)
        {
            BrickManager.instance.ResetBricks();
            ScoreManager.instance.ResetScore();
        }
    }
}

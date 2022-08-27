using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public float spawnx;
    public float spawny;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 spawnPos = new Vector2(spawnx, spawny);
        if (MovementController.playerInstance == null)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
        }
    }


}

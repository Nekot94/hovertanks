using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int numRoundsToWin = 5;
    public float startDelay = 1.5f;
    public float endDelay = 1.5f;
    public CameraControl cameraControl;
    public Text messageText;
    public GameObject tankPrefab;
    public TankManager[] tanks;

    private int toundNumber;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private TankManager roundWinner;
    private TankManager gameWinner;

    private void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        SpawnAllTanks();
        //SetCameraTargets();

        //StartCoroutine(GameLoop());

    }

    private void SpawnAllTanks()
    {
        for (int i = 0;i < tanks.Length;i++)
        {
            tanks[i].instance = Instantiate(tankPrefab,tanks[i].spawnPoint.position,tanks[i].spawnPoint.rotation);
            tanks[i].playerNumber = i + 1;
            tanks[i].Setup();
        }
    }

}

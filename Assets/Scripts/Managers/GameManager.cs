using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int numberOfLivePlayers = 2;
    public int numRoundsToWin = 5;
    public float startDelay = 1.5f;
    public float endDelay = 1.5f;
    public CameraControl cameraControl;
    public Text messageText;
    public GameObject[] tankPrefabs;
    public TankManager[] tanks;
    public List<Transform> wayPointsForAI;

    private int roundNumber;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private TankManager roundWinner;
    private TankManager gameWinner;

    private void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        SpawnAllTanks();
        SetCameraTargets();

        StartCoroutine(GameLoop());
    }

    private void SpawnAllTanks()
    {
        for (int i = 0; i < numberOfLivePlayers; i++)
        {
            tanks[i].instance = Instantiate(tankPrefabs[i], tanks[i].spawnPoint.position,
                tanks[i].spawnPoint.rotation);
            tanks[i].playerNumber = i + 1;
            tanks[i].SetupPlayerTank();
        }

        for (int i = numberOfLivePlayers; i < tanks.Length; i++)
        {
            tanks[i].instance = Instantiate(tankPrefabs[i], tanks[i].spawnPoint.position,
                tanks[i].spawnPoint.rotation);
            tanks[i].playerNumber = i + 1;
            tanks[i].SetupAI(wayPointsForAI);
        }
    }

    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[tanks.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = tanks[i].instance.transform;
        }

        cameraControl.targets = targets;
    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (gameWinner != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }

    }

    private IEnumerator RoundStarting()
    {
        ResetAllTanks();
        DisableTankControl();

        cameraControl.SetStartPositionAndSize();

        roundNumber++;
        messageText.text = "РАУНД " + roundNumber;

        yield return startWait;
    }

    private IEnumerator RoundPlaying()
    {
        EnableTankControl();

        messageText.text = string.Empty;

        while (!OneTankLeft())
        {
            yield return null;
        }

    }

    private IEnumerator RoundEnding()
    {
        DisableTankControl();
        roundWinner = null;
        roundWinner = GetRoundWinner();

        if (roundWinner != null)
            roundWinner.wins++;

        gameWinner = GetGameWinner();

        string message = EndMessage();
        messageText.text = message;

        yield return endWait;

    }

    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < tanks.Length; i++)
        {
            if (tanks[i].instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }

    private TankManager GetRoundWinner()
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            if (tanks[i].instance.activeSelf)
                return tanks[i];
        }

        return null;
    }

    private TankManager GetGameWinner()
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            if (tanks[i].wins == numRoundsToWin)
                return tanks[i];
        }

        return null;
    }

    private string EndMessage()
    {
        string message = " НИЧЬЯ;-(";

        if (roundWinner != null)
            message = roundWinner.coloredPlayerText + " ПОБЕДИЛ!!!";

        message += "\n\n\n\n";

        for (int i = 0; i < tanks.Length; i++)
        {
            message += tanks[i].coloredPlayerText + " ПОБЕД: " +
                tanks[i].wins + "\n";
        }

        if (gameWinner != null)
            message = gameWinner.coloredPlayerText + " ЧЕМПИОН!!!";

        return message;

    }

    private void ResetAllTanks()
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            tanks[i].Reset();
        }
    }

    private void EnableTankControl()
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            tanks[i].EnableControl();
        }
    }

    private void DisableTankControl()
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            tanks[i].DisableControl();
        }
    }
}

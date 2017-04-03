using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class TankManager
{
    public Color playerColor;
    public Transform spawnPoint;
    [HideInInspector]
    public int playerNumber;
    [HideInInspector]
    public string coloredPlayerText;
    [HideInInspector]
    public GameObject instance;
    [HideInInspector]
    public int wins;
    [HideInInspector]
    public List<Transform> wayPointList;

    private TankMovement movement;
    private TankShooting shooting;
    private GameObject canvasGameObject;
    private StateController stateController;
    
    public void SetupAI(List<Transform> wayPointList)
    {
        stateController = instance.GetComponent<StateController>();
        stateController.SetupAI(true, wayPointList);

        shooting = instance.GetComponent<TankShooting>();

        canvasGameObject = instance.GetComponentInChildren<Canvas>().gameObject;

        shooting.playerNumber = playerNumber;

        coloredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) +
            ">ИГРОК" + playerNumber + "</color>";

        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = playerColor;
        }
    }

    public void SetupPlayerTank()
    {
        movement = instance.GetComponent<TankMovement>();
        shooting = instance.GetComponent<TankShooting>();
        canvasGameObject = instance.GetComponentInChildren<Canvas>().gameObject;

        movement.playerNumber = playerNumber;
        shooting.playerNumber = playerNumber;

        coloredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) +
            ">ИГРОК" + playerNumber + "</color>";

        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = playerColor;
        }
    }

    public void DisableControl()
    {
        if (movement != null)
            movement.enabled = false;

        if (stateController != null)
            stateController.enabled = false;

        shooting.enabled = false;

        canvasGameObject.SetActive(false);

    }

    public void EnableControl()
    {
        if (movement != null)
            movement.enabled = true;

        if (stateController != null)
            stateController.enabled = true;

        shooting.enabled = true;

        canvasGameObject.SetActive(true);

    }

    public void Reset()
    {
        instance.transform.position = spawnPoint.position;
        instance.transform.rotation = spawnPoint.rotation;

        instance.SetActive(false);
        instance.SetActive(true);
    }
}

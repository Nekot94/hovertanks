﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public float minTime = 5f;
    public float maxTime = 40f;

    private GameObject[] itemSpawnPoints;

    [SerializeField]

    private GameObject item;

    private GameObject currentItem;

    private int randomPointNumber = 0;

    void Awake()
    {
        itemSpawnPoints = GameObject.FindGameObjectsWithTag("Floor");
        StartCoroutine(SpawnPerTime());
    }

    private IEnumerator SpawnPerTime()
    {
        while (true)
        {
            randomPointNumber = Random.Range(0, itemSpawnPoints.Length);
            SpawnItem();
            float randTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(randTime);
        }
    }

    private void SpawnItem()
    {
        if (itemSpawnPoints != null)
        {
            Vector3 position = itemSpawnPoints[randomPointNumber].
                transform.position;
            position += new Vector3(0, 1f, 0);
            currentItem = Instantiate(item,
                position, Quaternion.identity) as GameObject;
            ItemScript itemScript = currentItem.GetComponent<ItemScript>();
            if (itemScript != null)
                itemScript.ChooseRandomState();
        }
    }

}

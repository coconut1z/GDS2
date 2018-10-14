﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour {
    public int maxResourceAmt;
    public int currentResourceAmt;
    public int resourceCollectionAmt;

    public float respawnTimer;
    public float timeToRespawn;

    public bool isRespawning;

	void Start () {
        //timeToRespawn = Timer._instance.secondsInFullDay * 5;	//keep in case of error
        currentResourceAmt = maxResourceAmt;
	}
	
	void Update () {
		if (isRespawning) {
            respawnTimer += Time.deltaTime * Timer._instance.speed;
            if (respawnTimer >= timeToRespawn) {
                isRespawning = false;

                foreach (Transform child in transform) {
                    if (!child.gameObject.name.Contains("TutIndicator")) {
                        child.gameObject.SetActive(true);
                    }
                }

                gameObject.layer = LayerMask.NameToLayer("Resource");
                currentResourceAmt = maxResourceAmt;
                respawnTimer = 0;
            }
        }
	}

    public void AddResource(UnitController unit) {
        switch(tag) {
            case "Wood":
                unit.woodCollected += resourceCollectionAmt;
                SoundManager.instance.ResourceCollected();
                break;
            case "Stone":
                unit.stoneCollected += resourceCollectionAmt;
                SoundManager.instance.ResourceCollected();
                break;
            case "Gold":
                unit.goldCollected += resourceCollectionAmt;
                SoundManager.instance.ResourceCollected();
                break;
            default:
                break;
        }

        currentResourceAmt -= resourceCollectionAmt;

        if (currentResourceAmt <= 0) {
            isRespawning = true;

            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }

            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            if (SelectionController._instance.selectedObj == gameObject) {
                SelectionController._instance.DeselectObj();
            }
        }
    }
}

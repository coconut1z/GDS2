﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PicketFence : Technology, IPointerEnterHandler, IPointerExitHandler {

    public Image unresearchedImage;
    public Image connectingBar;
    public GameObject technologyObject;
    public Transform technologyPosition;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        technologyName = "Picket Fence";
        technologyDescription = "A wooden fence is constructed adding to your defenses";
        researchRequirement = "";
        woodCost = 10;
        stoneCost = 0;
        goldCost = 0;
        researchTime = 5f; 
        researchTimer = researchTime;
        researched = false;
        researching = false;
        applyTechnology = false;
        technologyImage = unresearchedImage;
        proceedingTechnologyBar.Add(connectingBar);
        technologyPosition = GameObject.Find(BaseController._instance.gameObject.name + "/Walls").transform;
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (researched && !researching && !applyTechnology) {
            EndResearch();
            applyTechnology = true;
        }
	}

    public override void TechnologyEffect() {
        //The effects of the technology which are active once research ends
        mainBase.defense += 1;
        Debug.Log("Added " + technologyName + " to the town");
        GameObject tech = Instantiate(technologyObject);
        tech.transform.SetParent(technologyPosition);
    }

    public override void StartResearch() {
        if (!researched && !researching) {
            if (resources.wood >= woodCost) {
                researchTimer = 0;
                researching = true;
                resources.SubtractWood(woodCost);
                resources.UpdateResourceText();
                Debug.Log("Researching: " + technologyName);
            }            
        }
    }

    public override void EndResearch() {
        TechnologyEffect();
        Debug.Log("Researched: " + technologyName);
    }

    public override void OnPointerEnter(PointerEventData pointer) {
        Debug.Log("Mouse has entered " + technologyName);
        ttbName.text = technologyName;
        ttbResearchRequirement.text = researchRequirement;
        ttbDescription.text = technologyDescription;
        ttbWoodCost.text = woodCost.ToString();
        ttbStoneCost.text = stoneCost.ToString();
        ttbGoldCost.text = goldCost.ToString();
        ttbResearchTime.text = researchTime.ToString() + " s";
    }

    public override void OnPointerExit(PointerEventData pointer) {
        Debug.Log("Mouse has exited " + technologyName);
        ttbName.text = "";
        ttbResearchRequirement.text = "";
        ttbDescription.text = "";
        ttbWoodCost.text = "";
        ttbStoneCost.text = "";
        ttbGoldCost.text = "";
        ttbResearchTime.text = "";
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ElixirOfLife : Technology, IPointerEnterHandler, IPointerExitHandler
{

    public Image unresearchedImage;
    public Image connectingBar;
    public Image connectingBar2;
    public GameObject technologyObject;
    public Transform technologyPosition;
    public Technology requiredTechnology;   //add more if you need more than one pre-requiste

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        technologyName = "Elixer of Life";
        technologyDescription = "A soft cloth which lets you perform actions during the day but you still take damage heavily";
        researchRequirement = "Philospher Stone";
        woodCost = 100;
        stoneCost = 100;
        goldCost = 500;
        researchTime = 300f;
        researchTimer = researchTime;
        researched = false;
        researching = false;
        applyTechnology = false;
        technologyImage = unresearchedImage;
        proceedingTechnologyBar.Add(connectingBar);
        proceedingTechnologyBar.Add(connectingBar2);
        mainBase = BaseController._instance;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (researched && !researching && !applyTechnology)
        {
            EndResearch();
            applyTechnology = true;
        }
    }

    public override void TechnologyEffect()
    {
        //The effects of the technology which are active once research ends
        //mainBase.defense += 3;
        Debug.Log("Added " + technologyName + " to the town");
        //Instantiate(technologyObject, technologyPosition);
    }

    public override void StartResearch()
    {
        if (!researched && !researching && requiredTechnology.researched)
        {
            if (resources.wood >= woodCost && resources.stone >= stoneCost && resources.gold >= goldCost)
            {
                researchTimer = 0;
                researching = true;
                resources.SubtractWood(woodCost);
                resources.SubtractStone(stoneCost);
                resources.SubtractGold(goldCost);
                resources.UpdateResourceText();
                Debug.Log("Researching: " + technologyName);
            }
        }
    }

    public override void EndResearch()
    {
        TechnologyEffect();
        Debug.Log("Researched: " + technologyName);
    }

    public override void OnPointerEnter(PointerEventData pointer)
    {
        ttbName.text = technologyName;
        ttbResearchRequirement.text = "Requirement: " + researchRequirement;
        ttbDescription.text = technologyDescription;
        ttbWoodCost.text = woodCost.ToString();
        ttbStoneCost.text = stoneCost.ToString();
        ttbGoldCost.text = goldCost.ToString();
        ttbResearchTime.text = researchTime.ToString() + " s";
    }

    public override void OnPointerExit(PointerEventData pointer)
    {
        ttbName.text = "";
        ttbResearchRequirement.text = "";
        ttbDescription.text = "";
        ttbWoodCost.text = "";
        ttbStoneCost.text = "";
        ttbGoldCost.text = "";
        ttbResearchTime.text = "";
    }
}


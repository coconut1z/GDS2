﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PhilospherStone : Technology, IPointerEnterHandler, IPointerExitHandler
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
        technologyName = "Philospher Stone";
        technologyDescription = "Increase the thirst bar by 50";
        researchRequirement = "Alchemy";
        researchCost = 100;
        researchTime = 20f;
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
        ResourceStorage._instance.maxHunger += 50;
        Debug.Log("Added " + technologyName + " to the town");
        //Instantiate(technologyObject, technologyPosition);
    }

    public override void StartResearch()
    {
        if (!researched && !researching && requiredTechnology.researched)
        {
            if (ResourceStorage._instance.wood >= researchCost)
            {
                researchTimer = 0;
                researching = true;
                ResourceStorage._instance.SubtractWood(researchCost);
                ResourceStorage._instance.UpdateResourceText();
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
        Debug.Log("Mouse has entered " + technologyName);
        ttbName.text = technologyName;
        ttbResearchRequirement.text = "Requirement: " + researchRequirement;
        ttbDescription.text = technologyDescription;
        ttbCost.text = researchCost.ToString() + " wood";
        ttbResearchTime.text = researchTime.ToString() + " seconds (need to edit)";
    }

    public override void OnPointerExit(PointerEventData pointer)
    {
        Debug.Log("Mouse has exited " + technologyName);
        ttbName.text = "";
        ttbResearchRequirement.text = "";
        ttbDescription.text = "";
        ttbCost.text = "";
        ttbResearchTime.text = "";
    }
}



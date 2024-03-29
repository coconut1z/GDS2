﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Alchemy : Technology, IPointerEnterHandler, IPointerExitHandler
{

    public Image unresearchedImage;
    public Image connectingBar;
    public Image connectingBar2;
    public GameObject technologyObject;
    public Transform technologyPosition;

    private bool hasResearched = false;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        technologyName = "Alchemy";
        technologyDescription = "Create a laboratory";
        researchRequirement = "";
        woodCost = 20;
        stoneCost = 30;
        goldCost = 30;
        researchTime = 30f;
        researchTimer = researchTime;
        researched = false;
        researching = false;
        applyTechnology = false;
        technologyImage = unresearchedImage;
        proceedingTechnologyBar.Add(connectingBar);
        proceedingTechnologyBar.Add(connectingBar2);
        mainBase = BaseController._instance;
        technologyPosition = GameObject.Find(BaseController._instance.gameObject.name).transform;
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
        GameObject tech = Instantiate(technologyObject);
        tech.transform.SetParent(technologyPosition);
    }

    public bool CheckIfResearched()
    {
        if (hasResearched == true)
        {
            return true;
        }

        return false;
    }

    public override void StartResearch()
    {
        if (!researched && !researching)
        {
            if (resources.wood >= woodCost && resources.stone >= stoneCost && resources.gold >= goldCost)
            {
                researchTimer = 0;
                researching = true;
                resources.SubtractWood(woodCost);
                resources.SubtractStone(stoneCost);
                resources.SubtractGold(goldCost);
                resources.UpdateResourceText();
            }
            else {
                NotEnoughResources();
            }
        }
    }

    public override void EndResearch()
    {
        TechnologyEffect();
        hasResearched = true;
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
        ttbWoodIcon.localScale = shownScale;
        ttbStoneIcon.localScale = shownScale;
        ttbGoldIcon.localScale = shownScale;
        ttbResearchTimeIcon.localScale = shownScale;
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
        ttbWoodIcon.localScale = hiddenScale;
        ttbStoneIcon.localScale = hiddenScale;
        ttbGoldIcon.localScale = hiddenScale;
        ttbResearchTimeIcon.localScale = hiddenScale;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class LeatherArmour : Technology, IPointerEnterHandler, IPointerExitHandler
{

    public Image unresearchedImage;
    public Image connectingBar;
    public GameObject technologyObject;
    public Transform technologyPosition;
    public Technology requiredTechnology;

    // Use this for initialization
    protected override void Start() {
        base.Start();
        technologyName = "Leather Armour";
        technologyDescription = "Defense + 1" + "\n" + "You wear the skin of dead enemies to protect yourself";
        researchRequirement = "Armoury";
        woodCost = 20;
        stoneCost = 0;
        goldCost = 10;
        researchTime = 5f;
        researchTimer = researchTime;
        researched = false;
        researching = false;
        applyTechnology = false;
        technologyImage = unresearchedImage;
        proceedingTechnologyBar.Add(connectingBar);
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        if (researched && !researching && !applyTechnology) {
            EndResearch();
            applyTechnology = true;
        }
    }

    public override void TechnologyEffect() {
        //The effects of the technology which are active once research ends
        mainBase.defense += 1;
        //Instantiate(technologyObject, technologyPosition);
    }

    public override void StartResearch() {
        if (!researched && !researching && requiredTechnology.researched) {
            if (resources.wood >= woodCost && resources.stone >= stoneCost && resources.gold >= goldCost) {
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
        else {
            MissingPrerequisite();
        }
    }

    public override void EndResearch() {
        TechnologyEffect();
    }

    public override void OnPointerEnter(PointerEventData pointer) {
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

    public override void OnPointerExit(PointerEventData pointer) {
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

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreatController : MonoBehaviour {
    public Slider threatBar;
    public float maxThreat;
    public float threat;
    public int threatLevel;
    public float threatToDeplete;

	// Use this for initialization
	void Start () {
        maxThreat = 100f;
        threat = 0f;
        threatBar = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void AddThreat(float amt) {
        threat += amt;
        threat = Mathf.Clamp(threat, 0f, maxThreat);
        SetThreatLevel();
        threatBar.value = HungerPercentage();
    }

    public void SubtractThreat() {
        if (threatLevel != 0) {
            threat -= threatToDeplete / threatLevel;
        }

        threat = Mathf.Clamp(threat, 0f, maxThreat);
        SetThreatLevel();
        threatBar.value = HungerPercentage();
    }

    public float HungerPercentage() {
        return threat / maxThreat;
    }

    public void SetThreatLevel() {
        if (threat == 0) {
            threatLevel = 0;
        } else if (threat < 25f) {
            threatLevel = 1;
        } else if (threat < 50f) {
            threatLevel = 2;
        } else if (threat < 75f) {
            threatLevel = 3;
        } else if (threat < 100f) {
            threatLevel = 4;
        } else if (threat == 100f) {
            threatLevel = 5;
        }
    }
}
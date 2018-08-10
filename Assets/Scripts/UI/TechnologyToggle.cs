﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TechnologyToggle : MonoBehaviour {

    public bool clicked;
    public RectTransform rect;
    public Vector3 scale;

	// Use this for initialization
	void Start () {
        clicked = false;
        scale = new Vector3(1, 1, 1);
        rect = GameObject.Find("Canvas/TechnologyWindow").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Toggle() {
        clicked = !clicked;
        if (!clicked) {
            rect.localScale = new Vector3(0, 0, 0);
        }
        else {
            rect.localScale = scale;
        }
    }
}
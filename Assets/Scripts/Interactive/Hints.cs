using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hints : MonoBehaviour
{
    public PlayerHandler player;
    public Canvas hintsUI;
    public Text hintsText;
    public Preinteraction canvasScript;

    bool hasLearnedKarting = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        hintsUI = GetComponent<Canvas>();
        hintsText = GetComponentInChildren<Text>();
        canvasScript = GetComponentInChildren<Preinteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        LearnKarting();
    }

    void LearnKarting()
    {
        if (player.level == 16 && !hasLearnedKarting)
        {
            hintsText.text = "Hint:\nCon click izquierdo, puedes atacar al dragón...";
            canvasScript.SetActive();
        }
        if (!hasLearnedKarting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hasLearnedKarting = true;
                canvasScript.SetUnactive();
            }
        }
    }
}

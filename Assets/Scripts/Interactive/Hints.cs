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
    bool hasDanced = false;

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
        AdviceAnxiety();
        LearnKarting();
        Dance();
    }

    void AdviceAnxiety()
    {
        if (player.anxiety >= 55)
        {
            hintsText.fontSize = 10;
            hintsText.text = "Tus niveles de ansiedad están subiendo peligrosamente.\nConsume una pastilla o cerveza para estabilizarte";
            canvasScript.SetActive();
        } else
        {
            canvasScript.SetUnactive();
        }
    }

    void LearnKarting()
    {
        if (player.level == 16 && !hasLearnedKarting)
        {
            hintsText.fontSize = 12;
            hintsText.text = "Hint:\nCon click izquierdo, puedes atacar al dragón...";
            canvasScript.SetActive();
        }
        if (!hasLearnedKarting)
        {
            if (Input.GetMouseButtonDown(0) && player.level >= 16)
            {
                hasLearnedKarting = true;
                canvasScript.SetUnactive();
            }
        }
    }

    void Dance()
    {
        if (player.hasKilledEvilMao && !hasDanced)
        {
            hintsText.fontSize = 12;
            hintsText.text = "¡Lo has logrado!\nPresiona B para bailar";
            canvasScript.SetActive();
        }
        if (!hasDanced)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                hasDanced = true;
                canvasScript.SetUnactive();
            }
        }
    }
}

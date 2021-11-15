using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    bool pickupBoxes = false;

    bool run = false;

    bool openInventory = false;
    public bool buySomething = false;
    bool getSomeMoney = false;
    bool getHigh = false;
    bool completed = false;

    bool isOpenInventory = false;
    bool goodLuck = false;

    public Text instructions;
    public PlayerHandler player;
    public GameObject menu;
    public MainMenu menuScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerHandler>();
        instructions.text = "Recoge las cajas";
    }

    // Update is called once per frame
    void Update()
    {
        OffOn();
        CheckComplete();
    }

    void CheckComplete()
    {
        if (CheckBoxes()) {
            instructions.text = "Presiona SHIFT para correr";
            if (CheckRun())
            {
                instructions.text = "Presiona TAB para abrir y cerrar tu inventario \n Para consumir algo de él, presiona la tecla correspondiente";
                if (CheckOpenInventory())
                {
                    instructions.text = "Busca a un vendedor (tienen un signo de exclamación en su cabeza), y presiona E para comprar un objeto aleatorio";
                    if (CheckBuy())
                    {
                        instructions.text = "Parece que te has quedado sin dinero, acercate a alguno de los ciudadanos para pedirles limosna o asaltarlos...";
                        if (CheckGetMoney())
                        {
                            if (!goodLuck)
                            {
                                instructions.text = "Sé que debes sentirte un poco solo. Pero si bebes una cerveza o consumes una pastilla, puedes volver a ver algunos de tus amigos";
                            }
                            if (CheckIntoxication())
                            {
                                instructions.text = "Mientras te mantengas en este estado, Cesarín, tu compañero te protegerá\nEstás listo para comenzar. Buena suerte!";
                                completed = true;
                            }
                        }
                    }
                }
            }
        }

        if (completed)
        {
            if (!goodLuck)
            {
                StartCoroutine(WaitForGoodLuck());
            }
        }
    }

    void OffOn()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOpenInventory)
            {
                //Close Inventory
                instructions.enabled = true;
                isOpenInventory = false;

            }
            else
            {
                //Open Inventory
                instructions.enabled = false;
                isOpenInventory = true;
            }
        }
    }

    bool CheckBoxes()
    {
        if (pickupBoxes)
        {
            return true;
        }
        if (player.inventory.GetItemList().Count > 4)
        {
            pickupBoxes = true;
            return true;
        }

        return false;
    }

    bool CheckRun()
    {
        if (run)
        {
            return true;
        } if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            run = true;
            return true;
        }
        return false;
    }

    bool CheckOpenInventory()
    {
        if (openInventory)
        {
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            openInventory = true;
            return true;
        }
        return false;
    }

    bool CheckBuy()
    {
        if (buySomething)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CheckGetMoney()
    {
        if (getSomeMoney)
        {
            return true;
        }
        if (player.money > 0)
        {
            getSomeMoney = true;
            return true;
        }
        return false;
    }

    bool CheckIntoxication()
    {
        if (getHigh)
        {
            return true;
        }
        if (player.isDrunk)
        {
            getHigh = true;
        }
        return false;
    }

    public void PlayNow()
    {
        SceneManager.LoadScene(3);
    }

    IEnumerator WaitForGoodLuck()
    {
        goodLuck = true;
        yield return new WaitForSecondsRealtime(10);
        menu.SetActive(true);
        menuScript.StartGame();
    }

}

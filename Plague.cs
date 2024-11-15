using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Plague : MonoBehaviour
{
    public Button blackPlague;
    public Button redPlague;
    public Button greenPlague;

    private bool isBlackPlagueOnCooldown = false;
    private bool isRedPlagueOnCooldown = false;
    private bool isGreenPlagueOnCooldown = false;

    public float blackPlagueCooldown = 5f;
    public float redPlagueCooldown = 6f;
    public float greenPlagueCooldown = 7f;

    void Start()
    {
        
    }

    void Update()
    {
        //Buttons are not done yet
    }

    //everything bellow doesn't work yet because buttons are not pressed
    public void BlackPlague()
    {
        if(isBlackPlagueOnCooldown)
        {
            Debug.Log("Black plague is on cooldown");
            return;
        }
        Debug.Log("Black plague is casted upon your enemies");
        StartCoroutine(PlagueCooldown(blackPlague,isBlackPlagueOnCooldown, blackPlagueCooldown));
    }

    public void RedPlague()
    {
        if(isRedPlagueOnCooldown)
        {
            Debug.Log("Red plague is on cooldown");
            return;
        }
        Debug.Log("Red plague is casted upon your enemies");
        StartCoroutine(PlagueCooldown(redPlague,isRedPlagueOnCooldown, redPlagueCooldown));
    }

    public void GreenPlague()
    {
        if(isGreenPlagueOnCooldown)
        {
            Debug.Log("Green plague is on cooldown");
            return;
        }
        Debug.Log("Green plague is casted upon your enemies");
        StartCoroutine(PlagueCooldown(greenPlague,isGreenPlagueOnCooldown, greenPlagueCooldown));
    }

    private IEnumerator PlagueCooldown(Button button,bool isOnCooldown, float cooldownDuration)
    {
        isOnCooldown = true;
        button.interactable = false;

        yield return new WaitForSeconds(cooldownDuration);

        isOnCooldown = false;
        button.interactable = true;
    }
}


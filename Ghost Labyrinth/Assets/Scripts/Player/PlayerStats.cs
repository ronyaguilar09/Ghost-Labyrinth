using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public HUDManager hud;
    private Animator anim;
    private PlayerMovement movement;

    void Awake()
    {
        anim = gameObject.transform.Find("Avatar").gameObject.GetComponent<Animator>();
        movement = gameObject.GetComponent<PlayerMovement>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            HUDManager.livesLeft -= 1;
            hud.UpdateUI();
            if (HUDManager.livesLeft <= 0)
            {
                anim.SetTrigger("Die");
                movement.enabled = false;
            }
        } else if (other.gameObject.tag == "Collectible")
        {
            Destroy(other.gameObject);
            Debug.Log("Found");
            HUDManager.collectiblesFound++;
            HUDManager.score += 100;
            hud.UpdateUI();
            if (HUDManager.collectiblesFound >= HUDManager.totalCollectibles)
            {
               // GameManager.AdvanceLevel();
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public HUDManager hud;
    public GameManager manager;
    private Animator anim;
    private PlayerMovement movement;

    void Awake()
    {
        anim = gameObject.transform.Find("Avatar").gameObject.GetComponent<Animator>();
        movement = gameObject.GetComponent<PlayerMovement>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
       /* if (other.gameObject.tag == "Enemy")
        {
            HUDManager.livesLeft -= 1;
            hud.UpdateUI();
            if (HUDManager.livesLeft <= 0)
            {
                anim.SetTrigger("Die");
                movement.enabled = false;
            }
        } else*/ if (other.gameObject.tag == "Collectible")
        {
            GameObject parent = other.transform.parent.gameObject;
            AudioSource audio = parent.GetComponent<AudioSource>();
            audio.Play();
            Debug.Log("Found");
            HUDManager.collectiblesFound++;
            HUDManager.score += 100;
            hud.UpdateUI();
            if (HUDManager.collectiblesFound >= HUDManager.totalCollectibles)
            {
               manager.CompleteLevel();
            }

            other.gameObject.SetActive(false);
            parent.GetComponent<Light>().enabled = false;
            StartCoroutine(DestroyCollectible(parent));
            StopCoroutine(DestroyCollectible(parent));
        }
    }

    IEnumerator DestroyCollectible(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        Destroy(obj);
    }

    public void AttackPlayer()
    {
        HUDManager.livesLeft -= 1;
        hud.UpdateUI();
        if (HUDManager.livesLeft <= 0)
        {
            anim.SetTrigger("Die");
            movement.enabled = false;
            StartCoroutine(DeathWait());
            StopCoroutine(DeathWait());
        }
    }

    IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(4);
        manager.GameOver();
    }

}

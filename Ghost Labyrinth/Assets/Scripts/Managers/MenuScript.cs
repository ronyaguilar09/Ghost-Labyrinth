using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    
    public Animator anim;

    private bool shouldLoadScene = false;
    
    public void StartGame()
    {
        anim.SetBool("StartGame", true);
        shouldLoadScene = true;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            if (Application.isEditor) UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private void Update()
    {
        if (shouldLoadScene && !AnimationIsPlaying())
        {
            SceneManager.LoadScene("Main");
        }
    }

    private bool AnimationIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}

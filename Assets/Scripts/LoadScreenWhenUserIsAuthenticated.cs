using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreenWhenUserIsAuthenticated : MonoBehaviour
{
    [SerializeField] private int sceneLoad;
    void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChange;
    }

    private void HandleAuthStateChange(object sender, EventArgs e)
    {
       if(FirebaseAuth.DefaultInstance.CurrentUser != null)
       {
            SceneManager.LoadScene(sceneLoad);
       }
    }
}

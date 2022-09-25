using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class ButtonLogOut : MonoBehaviour
{
    public void OnCLikLogOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        SceneManager.LoadScene(0);
    }
}

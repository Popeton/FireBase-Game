using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameLabel : MonoBehaviour
{
    [SerializeField] private Text label;


    // Start is called before the first frame update
    void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthChange;
    }

    private void HandleAuthChange(object sender, EventArgs e)
    {
        var currenUser = FirebaseAuth.DefaultInstance.CurrentUser;

        if(currenUser != null)
       
        {
            SetLabelUser(currenUser.UserId,label);
        }
    }

   public void SetLabelUser(string userId,Text labell)
    {
        FirebaseDatabase.DefaultInstance.GetReference("users/" + userId + "/username").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogWarning(task.Exception.ToString());
               
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.Value);

                labell.text = (string)snapshot.Value;
              //  PlayerPrefs.SetString("player", (string)snapshot.Value);
               
                
            }
        });
    }
}

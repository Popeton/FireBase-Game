using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FB_SetScore : MonoBehaviour
{
    [SerializeField] private Text leaderboard;
    DatabaseReference database;
    string userId;
    string username;
   
    void Start()
    {
        database = FirebaseDatabase.DefaultInstance.RootReference;
        userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        //username = PlayerPrefs.GetString("player");
    }

   

    public void WriteNewScore(int score)
    {
        UserData data = new UserData();
       // string actualUsername;

        //actualUsername = ActualPlayerUsername();
        //Debug.Log(actualUsername);
      //  data.username = username;
     
        data.score = int.Parse(score.ToString());
       
        string json = JsonUtility.ToJson(data);

        database.Child("users").Child(userId).SetRawJsonValueAsync(json);

    }
    public void GetUserScore()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users/"+userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogWarning(task.Exception.ToString());
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.Value);
                foreach(var userDoc in(Dictionary<string,object>)snapshot.Value)
                {
                    Debug.Log(userDoc.Key);
                    Debug.Log(userDoc.Value);
                }
            }
        });
    }
    public void GetLeaders()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("score").LimitToLast(3)
                .GetValueAsync().ContinueWithOnMainThread(task => {
                    if (task.IsFaulted)
                     {

                     }
                     else if (task.IsCompleted)
                     {
                         DataSnapshot snapshot = task.Result;
                        string data;
                        foreach (var userDoc in (Dictionary<string, object>)snapshot.Value)
                        {

                            var userObject = (Dictionary<string, object>)userDoc.Value;
                        
                            Debug.Log("Score:" + userObject["score"]);
                            
                            data = userObject["score"].ToString();
                            leaderboard.text = data; 
                    
                            
                         }
                     }
                });

      
    }
}

public class UserData
{
    public int score;
    public string username;
}

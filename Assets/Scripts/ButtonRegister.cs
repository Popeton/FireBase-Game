using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using Firebase.Database;

public class ButtonRegister : MonoBehaviour
{
    [SerializeField] 
    private Button _registrationButton;
    private Coroutine _regitrationCorutine;
    

    public event Action<FirebaseUser> onUserRegistered;
    public event Action<string> onUserRegistrationFailed; 

    private void Reset()
    {
        _registrationButton = GetComponent<Button>();
    }
    void Start()
    {
        _registrationButton.onClick.AddListener(HandleRegistrationButtonClicked);
    }

    private void HandleRegistrationButtonClicked()
    {
        string email = GameObject.Find("InputFieldEmail").GetComponent<InputField>().text;
        string password = GameObject.Find("InputFieldPassword").GetComponent<InputField>().text;
       
        _regitrationCorutine = StartCoroutine(RegisterUser(email, password));
    }

    private IEnumerator RegisterUser(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => registerTask.IsCompleted);

        if(registerTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task {registerTask.Exception} ");
            onUserRegistrationFailed?.Invoke($"failed to register task {registerTask.Exception}");
        }
        else
        {
            Debug.LogWarning($"Susccesfully registered user   {registerTask.Result.Email} ");
            UserData data = new UserData();

            data.username = GameObject.Find("InputFieldUsername").GetComponent<InputField>().text;
            string json = JsonUtility.ToJson(data);

            FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(registerTask.Result.UserId).SetRawJsonValueAsync(json);

            onUserRegistered?.Invoke(registerTask.Result);
            
        }

        _regitrationCorutine = null;
    }

    public void SendPasswordResetEmail()
    {
        var auth = FirebaseAuth.DefaultInstance;
        var user = auth.SignInAnonymouslyAsync();
        string emailAddress = GameObject.Find("InputFieldEmailReset").GetComponent<InputField>().text; ;
        if (user != null)
        {
            auth.SendPasswordResetEmailAsync(emailAddress).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("Password reset email sent successfully.");
            });
        }
    }
    // Update is called once per frame
    
}

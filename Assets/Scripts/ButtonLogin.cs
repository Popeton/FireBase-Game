using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLogin : MonoBehaviour
{

    [SerializeField]
    Button _loginButton;

    //[SerializeField] InputField _emailInputField;
    //[SerializeField] InputField _passwordInputField;

    private Coroutine _loginCoroutine;

    public event Action<FirebaseUser> OnloginSucceded;
    public event Action<string> OnloginFailed;
    void Start()
    {
        _loginButton.onClick.AddListener(HandleLoginButtonClicked);
    }

    private void HandleLoginButtonClicked()
    {
        //if(_loginCoroutine != null)
        //{

            string email = GameObject.Find("InputFieldEmail").GetComponent<InputField>().text;
            string password = GameObject.Find("InputFieldPassword").GetComponent<InputField>().text;
            _loginCoroutine = StartCoroutine(loginCoroutine(email, password));
        //}
    }

    private IEnumerator loginCoroutine(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(()=>loginTask.IsCompleted);

        if(loginTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task {loginTask.Exception} ");
            OnloginFailed?.Invoke($"failed to register task {loginTask.Exception}");
        }
        else
        {
            Debug.Log($"login Succeded with  {loginTask.Result} ");
            OnloginSucceded?.Invoke(loginTask.Result);
            SceneManager.LoadScene(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

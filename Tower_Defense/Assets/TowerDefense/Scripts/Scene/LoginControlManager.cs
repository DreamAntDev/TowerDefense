using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class LoginControlManager : MonoBehaviour {
    Firebase.Auth.FirebaseAuth auth;    
    public string email;
    public string password;
    // Start is called before the first frame update
    void Start()
    { 
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            SignIn();
        }
    }
    // TODO google login
    void SignUp() {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
        if (task.IsCanceled) {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
            return;
        }
        if (task.IsFaulted) {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            return;
        }
        // Firebase user has been created.
        Firebase.Auth.FirebaseUser newUser = task.Result;
        Debug.LogFormat("Firebase user created successfully: {0} ({1})",
            newUser.DisplayName, newUser.UserId);
        });
    }

    void SignIn() {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
        if (task.IsCanceled) {
            Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
            return;
        }
        if (task.IsFaulted) {
            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            return;
        }
        Firebase.Auth.FirebaseUser newUser = task.Result;
        Debug.LogFormat("User signed in successfully: {0}", newUser.UserId);
        Debug.Log(newUser.UserId);
        });
    }

    IEnumerator loginGameServer(string PlatformID) {
        // TODO 구조화 google auth 연결
        string data = "{'platform_id':'" + PlatformID + "','platform':'google','device_id':'" + SystemInfo.deviceUniqueIdentifier + "'}";
        UnityWebRequest webRequest = UnityWebRequest.Post("http://3.36.40.68:8888/login", data);
        webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();
        if(webRequest.isNetworkError || webRequest.isHttpError) {
            Debug.Log(webRequest.error);
        } else {
            Debug.Log(webRequest.downloadHandler.text);
            Debug.Log("login complete! ");
        }
    }
}

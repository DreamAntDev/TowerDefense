using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TowerDefenseAuth;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.UI;

public class User {
    public int id;
    public string platform_id;
    public string platform;
    public string device_id;
}

public class LoginSceneManager : MonoBehaviour
{
    public static int UserID;
    public Firebase.Auth.FirebaseUser FirebaseUser;
    private string email;
    private string password;
    Firebase.Auth.FirebaseAuth firbaseAuth;
    void Start()
    {
         firbaseAuth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
    public void StartGame() {
      login();
      // Goto DashBoard Scene
      SceneManager.LoadSceneAsync("GameScene");
    }
    // social or Guest login
    public void login() {
        // social login or guest login
        if(true) {
          // Guest login
          StartCoroutine(GameServerLogin(SystemInfo.deviceUniqueIdentifier, "guest"));
        } else {
          // Socal Login
          StartCoroutine(GameServerLogin(SystemInfo.deviceUniqueIdentifier, "guest"));
        }
    }
    public void ChangePlatform() {
      GameObject ObjectEmailInputField = GameObject.Find("EmailInputField");
      InputField EmailInputField = ObjectEmailInputField.GetComponent<InputField>();
      GameObject ObjectPasswordInputField = GameObject.Find("PasswordInputField");
      InputField PasswordInputField = ObjectPasswordInputField.GetComponent<InputField>();
      email = EmailInputField.text;
      password = PasswordInputField.text;
      // google SignUp
      SignUp(email, password);
      // TODO get UserID
    }
    public void SocailLogin() {
      GameObject ObjectEmailInputField = GameObject.Find("EmailInputField");
      InputField EmailInputField = ObjectEmailInputField.GetComponent<InputField>();
      GameObject ObjectPasswordInputField = GameObject.Find("PasswordInputField");
      InputField PasswordInputField = ObjectPasswordInputField.GetComponent<InputField>();
      email = EmailInputField.text;
      password = PasswordInputField.text;
      // google SignIn
      SignIn(email, password);
      // TODO get UserID
    }
    private void SignIn(string email, string password) {
            firbaseAuth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return ;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return ;
            }
            FirebaseUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0}", FirebaseUser.UserId);
            });
        }


     public void SignUp(string email, string password) {
            firbaseAuth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                SignIn(email, password);
                // Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                SignIn(email, password);
                // Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            // Firebase user has been created.
            FirebaseUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                FirebaseUser.DisplayName, FirebaseUser.UserId);
            });
        }

    IEnumerator GameServerLogin(string platform_id, string platform) {
            Debug.Log("call get GameId");
            User user = new User();
            user.platform_id = platform_id;
            user.platform = platform;
            user.device_id = SystemInfo.deviceUniqueIdentifier;
            string data = JsonUtility.ToJson(user);
            Debug.Log(data);
            UnityWebRequest webRequest = UnityWebRequest.Post("http://3.36.40.68:8888/login", data);
            webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
            webRequest.SetRequestHeader("Content-Type", "application/json");
            if(webRequest.isNetworkError || webRequest.isHttpError) {
                Debug.Log(webRequest.error);
            } else {
                Debug.Log("login complete! ");
            }
            yield return webRequest.SendWebRequest();
            user = JsonUtility.FromJson<User>(webRequest.downloadHandler.text);
            UserID = user.id;
            Debug.Log(UserID);
        }
}

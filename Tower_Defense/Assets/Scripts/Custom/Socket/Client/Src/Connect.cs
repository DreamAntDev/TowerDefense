using UnityEngine;
using socket.io;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

namespace SocketIO
{

    public class Connect : MonoBehaviour
    {
        
        public static Connect _instance;
        public bool isConnect = false;
        public static Connect Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            if(PlayerPrefs.GetInt("SelectionMenu") < 3){
                if (Instance == null)
                {
                    _instance = this;
                }
                else if (_instance != this)
                {
                    Destroy(gameObject);
                }

                DontDestroyOnLoad(this.gameObject);
            }else{
                Destroy(gameObject);
            }
           
        }

        private string serverUrl = "http://localhost:3000";
        Socket socket;
        private bool isPlay;

        public void Start()
        {
            StartCoroutine(SocketConnect());
            StartCoroutine(SocketState());
        }

        IEnumerator SocketState()
        {
            while (true) {
                yield return new WaitForSeconds(3f);
                isConnect = socket.IsConnected;
            }
        }
        IEnumerator SocketConnect()
        {
            socket = Socket.Connect(serverUrl);
            yield return new WaitForSeconds(1f);
            if (!socket.IsConnected)
            {
                StartCoroutine(ReConnect());
            }
            else
            {
                SocketEvent();
            }
            yield return null;
        }

        IEnumerator ReConnect()
        {
            
            while (!socket.IsSConnected)
            {
                yield return new WaitForSeconds(5f);
                socket = Socket.Connect(serverUrl);
            }
#if UNITY_EDITOR
            Debug.Log("Connect");
            Debug.Log(socket.IsConnected);
            Debug.Log(socket.IsSConnected);
#endif
            SocketEvent();
            yield return null;
        }

        private void SocketEvent()
        {
            socket.On(SystemEvents.connect, () =>
            {
#if UNITY_EDITOR
                Debug.Log("Hello, Socket.io~");
#endif
                socket.Emit("login", "MC");
            });

            socket.On(SystemEvents.reconnect, (int reconnectAttempt) =>
            {
#if UNITY_EDITOR
                Debug.Log("Hello, Again! " + reconnectAttempt);
#endif
            });

            socket.On(SystemEvents.disconnect, () =>
            {
#if UNITY_EDITOR
                Debug.Log("Bye~");
#endif
            });

            //SystemEvent가 아니면 Action에 String Data를 넘겨줘야 함
            socket.On("Play", (string data) =>
            {
#if UNITY_EDITOR
                    Debug.Log("Play");
#endif
            });
            socket.On("Exit", (string data) =>
            {
#if UNITY_EDITOR
                    Debug.Log("Exit");
#endif
            });
            isConnect = true;
        }
        
        private void OnApplicationQuit()
        {
            isConnect = false;
            socket.Off(SystemEvents.connect);
            socket.Off(SystemEvents.reconnect);
            socket.Off(SystemEvents.disconnect);
        }
        
        public Socket GetSocket(){
            return socket;
        }

         public void Message(String key, String msg){
            socket.Emit(key, msg);
        }
    }
}

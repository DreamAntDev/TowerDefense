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
            if (Instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(this.gameObject);
        }

        private string serverUrl = "http://localhost:3000";
        Socket socket;
        private bool isPlay;

        public void Start()
        {
            StartCoroutine(SocketConnect());
            //StartCoroutine(SocketState());
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
            CustomEvent();
            
            isConnect = true;
        }
        
        private void OnApplicationQuit()
        {
            isConnect = false;
            socket.Off(SystemEvents.connect);
            socket.Off(SystemEvents.reconnect);
            socket.Off(SystemEvents.disconnect);
        }
        
        //Server에서 전송 받은 데이터 처리 부분
        private void CustomEvent(){
            //몹 사망 처리
            socket.On("Death", (string data) =>
            {
            #if UNITY_EDITOR
                Debug.Log("Death");
            #endif
            });

            //몹 충돌 처리
            socket.On("Hit", (string data) =>
            {
            #if UNITY_EDITOR
                Debug.Log("Hit");
            #endif
            });

            //Tower Upgrade 및 설치 가능 여부
            socket.On("Tower", (string data) =>{
            #if UNITY_EDITOR
                Debug.Log("Tower");
            #endif
            });

            //실시간 코인 현황
            socket.On("Coin", (string data) =>{
            #if UNITY_EDITOR
                Debug.Log("Coin");
            #endif
            });

            //Last 스테이지 저장
            socket.On("Stage", (string data) =>{
            #if UNITY_EDITOR
                Debug.Log("Stage");
            #endif
            });
        }

        public void Message(String key, String msg){
            socket.Emit(key, msg);
        }
    }
}

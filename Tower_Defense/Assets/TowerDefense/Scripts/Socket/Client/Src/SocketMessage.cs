using UnityEngine;
using socket.io;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

namespace SocketIO
{
    class SocketMessage : MonoBehaviour
    {
        private Connect connect = Connect.Instance;
        
        private void Awake()
        {
            
        }
        
        public void Message(String key, int num){
            #if UNITY_EDITOR
                Debug.Log(String.Format("[{0}] : {1}", key, num));
            #endif
            connect.Message(key, num.ToString());
        }


    }
}

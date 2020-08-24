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
        
        public void Message(int num){
            connect.GetSocket().Emit("Death", num.ToString());
        }
    }
}

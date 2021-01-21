using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControlState
{
    public interface IPlayerControlState
    {
        void Start();
        void Update();
        void End();
    }
}
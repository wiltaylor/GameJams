using System;
using UnityEngine;

namespace Assets.Systems.PlayerManager
{
    public class CameraEventArgs : EventArgs
    {
        public GameObject ObjectToCentreOn;
        public int X;
        public int Y;
    }

}

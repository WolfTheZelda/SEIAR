using UnityEngine;

namespace SEIAR
{
    public class CameraType : MonoBehaviour
    {

        static public Camera GyroCamera;
        public Camera GyroCameraObject;
        
        void Start() {
            GyroCamera = GyroCameraObject;
        }        
    }
}

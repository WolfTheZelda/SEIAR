using EasyAR;
using UnityEngine;

// Thanks EasyAR

namespace SEIAR 
{
    public class DynamicImageTagetBehaviour : ImageTargetBehaviour
    {
        private GameObject UserTarget;
        private GameObject SubGameObject;

        protected override void Awake()
        {

            base.Awake();
            // Definir UserTarget usando seu nome.
            UserTarget = GameObject.Find("UserTarget");
            // Instanciar o UserTarget em outra váriavel.
            SubGameObject = Instantiate(UserTarget);
            SubGameObject.transform.parent = transform;
            SubGameObject.transform.position = new Vector3(0, 0, 0);

        }
    }
}

using EasyAR;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

// Thanks EasyAR

namespace SEIAR
{
    public class ImageTargetManager : MonoBehaviour
    {
        private Dictionary<string, DynamicImageTagetBehaviour> ImageTargetDictionary = new Dictionary<string, DynamicImageTagetBehaviour>();
        private FilesManager PathManager;

        void Start()
        {
            if (!PathManager)
                PathManager = FindObjectOfType<FilesManager>();
        }

        void Update()
        {
            var ImageTargetNameFileDic = PathManager.GetDirectoryNameFileDic();

            foreach (var Go in ImageTargetNameFileDic.Where(Go => !ImageTargetDictionary.ContainsKey(Go.Key)))
            {

                GameObject ImageTarget = new GameObject(Go.Key);
                var behaviour = ImageTarget.AddComponent<DynamicImageTagetBehaviour>();
                behaviour.Name = Go.Key;
                behaviour.Path = Go.Value.Replace(@"\", "/");
                behaviour.Storage = StorageType.Absolute;
                behaviour.Bind(ARBuilder.Instance.ImageTrackerBehaviours[0]);
                ImageTargetDictionary.Add(Go.Key, behaviour);

            }
        }

        public void ClearAllTarget()
        {

            foreach (var Go in ImageTargetDictionary) Destroy(Go.Value.gameObject);
            ImageTargetDictionary = new Dictionary<string, DynamicImageTagetBehaviour>();

        }
    }
}

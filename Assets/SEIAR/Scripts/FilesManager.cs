using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Thanks EasyAR :)

namespace SEIAR
{
    public class FilesManager : MonoBehaviour
    {
        private string MarksDirectory;
        private bool IsWriting;
      
        void Awake()
        {
            MarksDirectory = Application.persistentDataPath;
            Debug.Log("MarkPath:" + Application.persistentDataPath);
        }

        public void StartTakePhoto()
        {
            if (!Directory.Exists(MarksDirectory))
                Directory.CreateDirectory(MarksDirectory);
            if (!IsWriting)
                StartCoroutine(ImageCreate());
        }

        IEnumerator ImageCreate()
        {
            IsWriting = true;
            yield return new WaitForEndOfFrame();

            Texture2D Photo = new Texture2D(Screen.width / 2, Screen.height / 2, TextureFormat.RGB24, false);
            Photo.ReadPixels(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), 0, 0, false);
            Photo.Apply();

            byte[] data = Photo.EncodeToJPG(80);
            DestroyImmediate(Photo);
            Photo = null;

            string photoPath = Path.Combine(MarksDirectory, "Photo" + DateTime.Now.Ticks + UnityEngine.Random.Range(-1.0f, 1.0f) + ".jpg");

            using (FileStream file = File.Open(photoPath, FileMode.Create))
            {
                file.BeginWrite(data, 0, numBytes: data.Length, userCallback: new AsyncCallback(EndWriter), stateObject: file);
            }            
        }

        void EndWriter(IAsyncResult end)
        {
            using (FileStream EndWriter = (FileStream)end.AsyncState)
            {
                EndWriter.EndWrite(end);
                IsWriting = false;
            }
        }

        public Dictionary<string, string> GetDirectoryNameFileDic()
        {
            if (!Directory.Exists(MarksDirectory))
                return new Dictionary<string, string>();
            return GetAllImagesFiles(MarksDirectory);
        }

        private Dictionary<string, string> GetAllImagesFiles(string path)
        {
            Dictionary<string, string> ImageFilesDirectory = new Dictionary<string, string>();
            foreach (var file in Directory.GetFiles(path))
            {
                if (Path.GetExtension(file) == ".jpg" || Path.GetExtension(file) == ".bmp" || Path.GetExtension(file) == ".png")
                    ImageFilesDirectory.Add(Path.GetFileNameWithoutExtension(file), file);
            }
            return ImageFilesDirectory;
        }

        public void ClearTexture()
        {
            Dictionary<string, string> ImageFileDirectory = GetAllImagesFiles(MarksDirectory);
            foreach (var path in ImageFileDirectory)
                File.Delete(path.Value);
        }
    }
}

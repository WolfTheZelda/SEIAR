using UnityEngine;
using UnityEngine.SceneManagement;

namespace SEIAR {
    public class ScenesButton : MonoBehaviour {

        public void SEIARLOAD()
        {
            SceneManager.LoadScene("SEIAR");
        }

        public void FLAPPYCELLLOAD()
        {
            Time.timeScale = ApplicationButton.TimeDefaultBackup;
            SceneManager.LoadScene("FLAPPYCELL");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BakersDozenGames
{
    public class PauseMenuManager : MonoBehaviour
    {
        public Canvas PauseMenuCanvas;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                PauseMenu(true);
            }
        }

        public void PauseMenu(bool enable)
        {
            PauseMenuCanvas.enabled = enable;
        }
        
        public void Resume()
        {
            Time.timeScale = 1;
            PauseMenu(false);
        }
    }
}

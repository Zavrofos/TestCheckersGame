using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PouseMenu : MonoBehaviour
    {
        public bool isPouse = false;
        public void ActivePouse()
        {
            if(isPouse)
            {
                isPouse = false;
                Time.timeScale = 1;
                gameObject.SetActive(false);
            }
            else
            {
                isPouse = true;
                Time.timeScale = 0;
                gameObject.SetActive(true);
            }
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public void WinGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}

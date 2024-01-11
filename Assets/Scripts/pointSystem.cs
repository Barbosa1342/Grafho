using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointSystem : MonoBehaviour
{
    static int points = 0;
    static public int Points => points;

    static public void changePoint(int amount)
    {
        points += amount;
        sceneManager.sceneManagerObj.updateScore();
    }

    static public void restartPoints()
    {
        points = 0;
    }
}

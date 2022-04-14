using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestAnimalImageCheck : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite questOpenImage;
    public Sprite questDoneImage;
    public Image questImage;

    public void QuestImageCheck(string playerPref)

    {
        if (PlayerPrefs.GetInt(playerPref, 0) == 0)
        {
            questImage.sprite = questOpenImage;
        }
        else
        {
            questImage.sprite = questDoneImage;
        }
    }

    public float QuestDoneCheck(string playerPref)
    {
        return PlayerPrefs.GetInt(playerPref, 0);
    }
}

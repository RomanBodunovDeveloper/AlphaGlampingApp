using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterMenuController : MonoBehaviour
{
    public static FilterMenuController instance { set; get; }
    // Start is called before the first frame update
    public GameObject[] placeObjectArr;
    public GameObject[] animalObjectArr;
    public GameObject[] villageObjectArr;
    public GameObject[] questObjectArr;
    public string[] questObjectPlayerPref;
    public BoxCollider[] questColliderArr;
    public GameObject[] roadSignObjectArr;
    public GameObject[] carPathEasyObjectArr;
    public GameObject[] bikePathMiddleObjectArr;
    public GameObject[] bikePathHardObjectArr;
    public GameObject[] walkPathEasyObjectArr;
    public GameObject[] walkPathMiddleObjectArr;
    public GameObject[] walkPathHardObjectArr;

    private bool placeShow;
    private bool animalShow;
    private bool villageeShow;
    private bool questShow;
    private bool roadSignShow;
    private bool carPathEasyShow;
    private bool bikePathMiddleShow;
    private bool bikePathHardShow;
    private bool walkPathEasyShow;
    private bool walkPathMiddleShow;
    private bool walkPathHardShow;
    private bool firstStart;

    public RectTransform panelFilter;
   // public Image buttonImage;
   // public Sprite buttonImageOpen;
   // public Sprite buttonImageClose;
    private bool activeAnim;
    private Vector3 tempvec;
    private Vector3 startMenuFilterposition;
    public float menuFilterSpeed;
    private bool menuOpen;
    public GameObject panelBar;

    private Color tempColor;
    public Image imagePlace;
    public Image imageAnimal;
    public Image imageVillage;
    public Image imageQuest;
    public Image imageRoadSign;
    public Image imageCarPathEasy;
    public Image imageBikePathMiddle;
    public Image imageBikePathHard;
    public Image imageWalkPathEasy;
    public Image imageWalkPathMiddle;
    public Image imageWalkPathHard;

    public RectTransform infoWalkPathEasy;
    public RectTransform infoWalkPathMiddle;
    public RectTransform infoWalkPathHard;
    public RectTransform infoBikeMedium;
    public RectTransform infoBikeHard;
    public RectTransform infoCarEasy;

    public MapPathController carEasyIconController;
    public MapPathController bikeMiddleIconController;
    public MapPathController bikeHardIconController;
    public MapPathController walkEasyIconController;
    public MapPathController walkMiddleIconController;
    public MapPathController walkHardIconController;
    public RectTransform pathInfoButton;
    private bool pathInfoOpen;
    private Vector3 startPathButtonInfoPosition;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        placeShow = true;
        animalShow = true;
        villageeShow = true;
        questShow = true;
        roadSignShow = true;
        carPathEasyShow = false;
        bikePathMiddleShow = false;
        bikePathHardShow = false;
        walkPathEasyShow = false;
        walkPathMiddleShow = false;
        walkPathHardShow = false;
        firstStart = true;

        ChangeShowFilterCarPathEasy(false);
        ChangeShowFilterBikePathMiddle(false);
        ChangeShowFilterBikePathHard(false);
        ChangeShowFilterWalkPathEasy(false);
        ChangeShowFilterWalkPathMiddle(false);
        ChangeShowFilterWalkPathHard(false);

        startMenuFilterposition = panelFilter.localPosition;
        //Debug.Log(startMenuFilterposition);

        ClosePAthInfo();
        pathInfoButton.gameObject.SetActive(false);
        startPathButtonInfoPosition = pathInfoButton.localPosition;
}

    public void ClickPlaceButton()
    {
        ChangeShowFilterPlace(true);
    }

    public void ClickAnimalButton()
    {
        ChangeShowFilterAnimal(true);
    }

    public void ClickVillageButton()
    {
        ChangeShowFilterVillage(true);
    }


    public void ClickQuestButton()
    {
        ChangeShowFilterQuest(true);
    }
    public void ClickRoadSignButton()
    {
        ChangeShowFilterRoadSign(true);
    }

    public void ClickCarPathEasyButton()
    {
        ChangeShowFilterCarPathEasy(true);
    }

    public void ClickBikePathMiddleButton()
    {
        ChangeShowFilterBikePathMiddle(true);
    }

    public void ClickBikePathHardButton()
    {
        ChangeShowFilterBikePathHard(true);
    }

    public void ClickWalkPathEasyButton()
    {
        ChangeShowFilterWalkPathEasy(true);
    }

    public void ClickWalkPathMiddleButton()
    {
        ChangeShowFilterWalkPathMiddle(true);
    }

    public void ClickWalkPathHardButton()
    {
        ChangeShowFilterWalkPathHard(true);
    }


    private void OnEnable()
    {
        if (firstStart)
        {
            ChangeShowFilterPlace(false);
            ChangeShowFilterAnimal(false);
            ChangeShowFilterVillage(false);
            ChangeShowFilterQuest(false);
            ChangeShowFilterRoadSign(false);
            ChangeShowFilterCarPathEasy(false);
            ChangeShowFilterBikePathMiddle(false);
            ChangeShowFilterBikePathHard(false);
            ChangeShowFilterWalkPathEasy(false);
            ChangeShowFilterWalkPathMiddle(false);
            ChangeShowFilterWalkPathHard(false);

            ClosePAthInfo();
            pathInfoButton.gameObject.SetActive(false);
        }

    }

    private void OnDisable()
    {
        panelFilter.localPosition = startMenuFilterposition;
       // buttonImage.sprite = buttonImageClose;
        menuOpen = false;
        ClosePAthInfo();
        pathInfoButton.gameObject.SetActive(false);
    }

    private void ChangeShowFilterPlace(bool clickButton)
    {
        if (clickButton)
        {
            if (placeShow)
            {
                for (int i = 0; i < placeObjectArr.Length; i++)
                {
                    placeObjectArr[i].gameObject.SetActive(false);
                }

                for (int i = 0; i < questObjectArr.Length; i++)
                {
                    questObjectArr[i].gameObject.SetActive(false);
                }

                placeShow = false;
                questShow = false;

                tempColor = imagePlace.color;
                tempColor.a = 0.2f;
                imagePlace.color = tempColor;
                imageQuest.color = tempColor;
            }
            else
            {
                for (int i = 0; i < placeObjectArr.Length; i++)
                {
                    placeObjectArr[i].gameObject.SetActive(true);
                }

                for (int i = 0; i < questObjectArr.Length; i++)
                {
                    if (PlayerPrefs.GetInt($"{questObjectPlayerPref[i]}_gps", 0) == 0)
                    {
                        questObjectArr[i].gameObject.SetActive(true);
                        questColliderArr[i].enabled = true;
                    }
                }

                placeShow = true;
                questShow = true;

                tempColor = imagePlace.color;
                tempColor.a = 1f;
                imagePlace.color = tempColor;
                imageQuest.color = tempColor;
            }
        }
        else
        {
            if (placeShow)
            {
                for (int i = 0; i < placeObjectArr.Length; i++)
                {
                    placeObjectArr[i].gameObject.SetActive(true);
                }

                for (int i = 0; i < questObjectArr.Length; i++)
                {
                    if (PlayerPrefs.GetInt($"{questObjectPlayerPref[i]}_gps", 0) == 0)
                    {
                        questObjectArr[i].gameObject.SetActive(true);
                        questColliderArr[i].enabled = true;
                    }
                }

                questShow = true;

                tempColor = imagePlace.color;
                tempColor.a = 1f;
                imagePlace.color = tempColor;
                imageQuest.color = tempColor;
            }
            else
            {
                for (int i = 0; i < placeObjectArr.Length; i++)
                {
                    placeObjectArr[i].gameObject.SetActive(false);
                }

                for (int i = 0; i < questObjectArr.Length; i++)
                {
                    questObjectArr[i].gameObject.SetActive(false);
                }

                questShow = false;
                tempColor = imagePlace.color;
                tempColor.a = 0.2f;
                imagePlace.color = tempColor;
                imageQuest.color = tempColor;
            }
        }
    }

    private void ChangeShowFilterAnimal(bool clickButton)
    {
        if (clickButton)
        {
            if (animalShow)
            {
                for (int i = 0; i < animalObjectArr.Length; i++)
                {
                    animalObjectArr[i].gameObject.SetActive(false);
                }
                animalShow = false;

                tempColor = imageAnimal.color;
                tempColor.a = 0.2f;
                imageAnimal.color = tempColor;
            }
            else
            {
                for (int i = 0; i < animalObjectArr.Length; i++)
                {
                    animalObjectArr[i].gameObject.SetActive(true);
                }
                animalShow = true;

                tempColor = imageAnimal.color;
                tempColor.a = 1f;
                imageAnimal.color = tempColor;
            }
        }
        else
        {
            if (animalShow)
            {
                for (int i = 0; i < animalObjectArr.Length; i++)
                {
                    animalObjectArr[i].gameObject.SetActive(true);
                }
                tempColor = imageAnimal.color;
                tempColor.a = 1f;
                imageAnimal.color = tempColor;
            }
            else
            {
                for (int i = 0; i < animalObjectArr.Length; i++)
                {
                    animalObjectArr[i].gameObject.SetActive(false);
                }

                tempColor = imageAnimal.color;
                tempColor.a = 0.2f;
                imageAnimal.color = tempColor;
            }
        }
    }

    private void ChangeShowFilterVillage(bool clickButton)
    {
        if (clickButton)
        {
            if (villageeShow)
            {
                for (int i = 0; i < villageObjectArr.Length; i++)
                {
                    villageObjectArr[i].gameObject.SetActive(false);
                }
                villageeShow = false;

                tempColor = imageVillage.color;
                tempColor.a = 0.2f;
                imageVillage.color = tempColor;
            }
            else
            {
                for (int i = 0; i < villageObjectArr.Length; i++)
                {
                    
                   
                    //{
                        villageObjectArr[i].gameObject.SetActive(true);
                    //}
                }
                villageeShow = true;

                tempColor = imageVillage.color;
                tempColor.a = 1f;
                imageVillage.color = tempColor;
            }
        }
        else
        {
            if (villageeShow)
            {
                for (int i = 0; i < villageObjectArr.Length; i++)
                {
                   // if (PlayerPrefs.GetInt($"{questObjectPlayerPref[i]}_gps", 0) == 0)
                    //{
                        villageObjectArr[i].gameObject.SetActive(true);
                    //}
                }

                tempColor = imageVillage.color;
                tempColor.a = 1f;
                imageVillage.color = tempColor;
            }
            else
            {
                for (int i = 0; i < villageObjectArr.Length; i++)
                {
                    villageObjectArr[i].gameObject.SetActive(false);
                }
                tempColor = imageVillage.color;
                tempColor.a = 0.2f;
                imageVillage.color = tempColor;
            }
        }
    }


    private void ChangeShowFilterQuest(bool clickButton)
    {
        if (clickButton)
        {
            if (questShow)
            {
                for (int i = 0; i < questObjectArr.Length; i++)
                {
                    questObjectArr[i].gameObject.SetActive(false);
                    questColliderArr[i].enabled = false;
                }
                questShow = false;

                tempColor = imageQuest.color;
                tempColor.a = 0.2f;
                imageQuest.color = tempColor;
            }
            else
            {
                for (int i = 0; i < questObjectArr.Length; i++)
                {
                    if (PlayerPrefs.GetInt($"{questObjectPlayerPref[i]}_gps", 0) == 0)
                    {
                        questObjectArr[i].gameObject.SetActive(true);
                        questColliderArr[i].enabled = true;
                    }
                        
                }
                questShow = true;

                tempColor = imageQuest.color;
                tempColor.a = 1f;
                imageQuest.color = tempColor;
            }
        }
        else
        {
            if (questShow)
            {
                for (int i = 0; i < questObjectArr.Length; i++)
                {
                    if (PlayerPrefs.GetInt($"{questObjectPlayerPref[i]}_gps", 0) == 0)
                    {
                        questObjectArr[i].gameObject.SetActive(true);
                        questColliderArr[i].enabled = true;
                    }

                }

                tempColor = imageQuest.color;
                tempColor.a = 1f;
                imageQuest.color = tempColor;
            }
            else
            {
                for (int i = 0; i < questObjectArr.Length; i++)
                {
                    questObjectArr[i].gameObject.SetActive(false);
                    questColliderArr[i].enabled = false;
                }
                tempColor = imageQuest.color;
                tempColor.a = 0.2f;
                imageQuest.color = tempColor;
            }
        }
    }

    private void ChangeShowFilterRoadSign(bool clickButton)
    {
        if (clickButton)
        {
            if (roadSignShow)
            {
                for (int i = 0; i < roadSignObjectArr.Length; i++)
                {
                    roadSignObjectArr[i].gameObject.SetActive(false);
                }
                roadSignShow = false;

                tempColor = imageRoadSign.color;
                tempColor.a = 0.2f;
                imageRoadSign.color = tempColor;
            }
            else
            {
                for (int i = 0; i < roadSignObjectArr.Length; i++)
                {
                    roadSignObjectArr[i].gameObject.SetActive(true);
                }
                roadSignShow = true;

                tempColor = imageRoadSign.color;
                tempColor.a = 1f;
                imageRoadSign.color = tempColor;
            }
        }
        else
        {
            if (roadSignShow)
            {
                for (int i = 0; i < roadSignObjectArr.Length; i++)
                {
                    roadSignObjectArr[i].gameObject.SetActive(true);
                }

                tempColor = imageRoadSign.color;
                tempColor.a = 1f;
                imageRoadSign.color = tempColor;
            }
            else
            {
                for (int i = 0; i < roadSignObjectArr.Length; i++)
                {
                    roadSignObjectArr[i].gameObject.SetActive(false);
                }

                tempColor = imageRoadSign.color;
                tempColor.a = 0.2f;
                imageRoadSign.color = tempColor;
            }
        }
    }

    private void ChangeShowFilterCarPathEasy(bool clickButton)
    {
        if (clickButton)
        {
            if (carPathEasyShow)
            {
                for (int i = 0; i < carPathEasyObjectArr.Length; i++)
                {
                    carPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                carPathEasyShow = false;
                tempColor = imageCarPathEasy.color;
                tempColor.a = 0.2f;
                imageCarPathEasy.color = tempColor;
                pathInfoButton.gameObject.SetActive(false);
                ClosePAthInfo();
            }
            else
            {
                for (int i = 0; i < carPathEasyObjectArr.Length; i++)
                {
                    carPathEasyObjectArr[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < roadSignObjectArr.Length; i ++)
                {
                    roadSignObjectArr[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < bikePathMiddleObjectArr.Length; i++)
                {
                    bikePathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < bikePathHardObjectArr.Length; i++)
                {
                    bikePathHardObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathEasyObjectArr.Length; i++)
                {
                    walkPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathMiddleObjectArr.Length; i++)
                {
                    walkPathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathHardObjectArr.Length; i++)
                {
                    walkPathHardObjectArr[i].gameObject.SetActive(false);
                }
                ClosePAthInfo();
                pathInfoButton.gameObject.SetActive(true);
                carPathEasyShow = true;
                roadSignShow = true;
                carEasyIconController.OnVirtualClick();

                tempColor = imageCarPathEasy.color;
                tempColor.a = 1f;
                imageCarPathEasy.color = tempColor;
                imageRoadSign.color = tempColor;
                tempColor.a =0.2f;
                imageBikePathMiddle.color = tempColor;
                imageBikePathHard.color = tempColor;
                imageWalkPathEasy.color = tempColor;
                imageWalkPathMiddle.color = tempColor;
                imageWalkPathHard.color = tempColor;

                bikePathMiddleShow = false;
                bikePathHardShow = false;
                walkPathEasyShow = false;
                walkPathMiddleShow = false;
                walkPathHardShow = false;
            }
        }
        else
        {
            //if (carPathEasyShow)
            //{
            //    for (int i = 0; i < carPathEasyObjectArr.Length; i++)
            //    {
            //        carPathEasyObjectArr[i].gameObject.SetActive(true);
            //    }

            //    tempColor = imageCarPathEasy.color;
            //    tempColor.a = 1f;
            //    imageCarPathEasy.color = tempColor;
            //}
            //else
            //{
                for (int i = 0; i < carPathEasyObjectArr.Length; i++)
                {
                    carPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                carPathEasyShow = false;
                tempColor = imageCarPathEasy.color;
                tempColor.a = 0.2f;
                imageCarPathEasy.color = tempColor;
            //}
        }
    }

    private void ChangeShowFilterBikePathMiddle(bool clickButton)
    {
        if (clickButton)
        {
            if (bikePathMiddleShow)
            {
                for (int i = 0; i < bikePathMiddleObjectArr.Length; i++)
                {
                    bikePathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                bikePathMiddleShow = false;
                tempColor = imageBikePathMiddle.color;
                tempColor.a = 0.2f;
                imageBikePathMiddle.color = tempColor;
                pathInfoButton.gameObject.SetActive(false);
                ClosePAthInfo();
            }
            else
            {
                for (int i = 0; i < bikePathMiddleObjectArr.Length; i++)
                {
                    bikePathMiddleObjectArr[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < bikePathHardObjectArr.Length; i++)
                {
                    bikePathHardObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < carPathEasyObjectArr.Length; i++)
                {
                    carPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathEasyObjectArr.Length; i++)
                {
                    walkPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathMiddleObjectArr.Length; i++)
                {
                    walkPathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathHardObjectArr.Length; i++)
                {
                    walkPathHardObjectArr[i].gameObject.SetActive(false);
                }
                ClosePAthInfo();
                pathInfoButton.gameObject.SetActive(true);
                bikePathMiddleShow = true;
                bikeMiddleIconController.OnVirtualClick();

                tempColor = imageBikePathMiddle.color;
                tempColor.a = 1f;
                imageBikePathMiddle.color = tempColor;
                tempColor.a = 0.2f;
                imageCarPathEasy.color = tempColor;
                imageBikePathHard.color = tempColor;
                imageWalkPathEasy.color = tempColor;
                imageWalkPathMiddle.color = tempColor;
                imageWalkPathHard.color = tempColor;

                carPathEasyShow = false;
                bikePathHardShow = false;
                walkPathEasyShow = false;
                walkPathMiddleShow = false;
                walkPathHardShow = false;
            }
        }
        else
        {
            //if (bikePathMiddleShow)
            //{
            //    for (int i = 0; i < bikePathMiddleObjectArr.Length; i++)
            //    {
            //        bikePathMiddleObjectArr[i].gameObject.SetActive(true);
            //    }

            //    tempColor = imageBikePathMiddle.color;
            //    tempColor.a = 1f;
            //    imageBikePathMiddle.color = tempColor;
            //}
            //else
            //{
                for (int i = 0; i < bikePathMiddleObjectArr.Length; i++)
                {
                    bikePathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                bikePathMiddleShow = false;
                tempColor = imageBikePathMiddle.color;
                tempColor.a = 0.2f;
                imageBikePathMiddle.color = tempColor;
            //}
        }
    }


    private void ChangeShowFilterBikePathHard(bool clickButton)
    {
        if (clickButton)
        {
            if (bikePathHardShow)
            {
                for (int i = 0; i < bikePathHardObjectArr.Length; i++)
                {
                    bikePathHardObjectArr[i].gameObject.SetActive(false);
                }
                bikePathHardShow = false;
                tempColor = imageBikePathHard.color;
                tempColor.a = 0.2f;
                imageBikePathHard.color = tempColor;
                pathInfoButton.gameObject.SetActive(false);
                ClosePAthInfo();
            }
            else
            {
                for (int i = 0; i < bikePathHardObjectArr.Length; i++)
                {
                    bikePathHardObjectArr[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < bikePathMiddleObjectArr.Length; i++)
                {
                    bikePathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < carPathEasyObjectArr.Length; i++)
                {
                    carPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathEasyObjectArr.Length; i++)
                {
                    walkPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathMiddleObjectArr.Length; i++)
                {
                    walkPathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathHardObjectArr.Length; i++)
                {
                    walkPathHardObjectArr[i].gameObject.SetActive(false);
                }
                pathInfoButton.gameObject.SetActive(true);
                bikePathHardShow = true;
                bikeHardIconController.OnVirtualClick();
                ClosePAthInfo();
                tempColor = imageBikePathHard.color;
                tempColor.a = 1f;
                imageBikePathHard.color = tempColor;
                tempColor.a = 0.2f;
                imageCarPathEasy.color = tempColor;
                imageBikePathMiddle.color = tempColor;
                imageWalkPathEasy.color = tempColor;
                imageWalkPathMiddle.color = tempColor;
                imageWalkPathHard.color = tempColor;

                carPathEasyShow = false;
                bikePathMiddleShow = false;
                walkPathEasyShow = false;
                walkPathMiddleShow = false;
                walkPathHardShow = false;
            }
        }
        else
        {
            //if (bikePathHardShow)
            //{
            //    for (int i = 0; i < bikePathHardObjectArr.Length; i++)
            //    {
            //        bikePathHardObjectArr[i].gameObject.SetActive(true);
            //    }

            //    tempColor = imageBikePathHard.color;
            //    tempColor.a = 1f;
            //    imageBikePathHard.color = tempColor;
            //}
            //else
            //{
                for (int i = 0; i < bikePathHardObjectArr.Length; i++)
                {
                    bikePathHardObjectArr[i].gameObject.SetActive(false);
                }
                bikePathHardShow = false;
                tempColor = imageBikePathHard.color;
                tempColor.a = 0.2f;
                imageBikePathHard.color = tempColor;
            //}
        }
    }

    private void ChangeShowFilterWalkPathEasy(bool clickButton)
    {
        if (clickButton)
        {
            if (walkPathEasyShow)
            {
                for (int i = 0; i < walkPathEasyObjectArr.Length; i++)
                {
                    walkPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                walkPathEasyShow = false;
               
                tempColor = imageWalkPathEasy.color;
                tempColor.a = 0.2f;
                imageWalkPathEasy.color = tempColor;
                pathInfoButton.gameObject.SetActive(false);
                ClosePAthInfo();
            }
            else
            {
                for (int i = 0; i < walkPathEasyObjectArr.Length; i++)
                {
                    walkPathEasyObjectArr[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < carPathEasyObjectArr.Length; i++)
                {
                    carPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < bikePathMiddleObjectArr.Length; i++)
                {
                    bikePathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < bikePathHardObjectArr.Length; i++)
                {
                    bikePathHardObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathMiddleObjectArr.Length; i++)
                {
                    walkPathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathHardObjectArr.Length; i++)
                {
                    walkPathHardObjectArr[i].gameObject.SetActive(false);
                }
                pathInfoButton.gameObject.SetActive(true);
                walkPathEasyShow = true;
                walkEasyIconController.OnVirtualClick();
                ClosePAthInfo();
                tempColor = imageWalkPathEasy.color;
                tempColor.a = 1f;
                imageWalkPathEasy.color = tempColor;
                tempColor.a = 0.2f;
                imageCarPathEasy.color = tempColor;
                imageBikePathMiddle.color = tempColor;
                imageBikePathHard.color = tempColor;
                imageWalkPathMiddle.color = tempColor;
                imageWalkPathHard.color = tempColor;


                carPathEasyShow = false;
                bikePathMiddleShow = false;
                bikePathHardShow = false;
                walkPathMiddleShow = false;
                walkPathHardShow = false;


            }
        }
        else
        {
            //if (walkPathEasyShow)
            //{
            //    for (int i = 0; i < walkPathEasyObjectArr.Length; i++)
            //    {
            //        walkPathEasyObjectArr[i].gameObject.SetActive(true);
            //    }

            //    tempColor = imageWalkPathEasy.color;
            //    tempColor.a = 1f;
            //    imageWalkPathEasy.color = tempColor;
            //}
            //else
            //{
                for (int i = 0; i < walkPathEasyObjectArr.Length; i++)
                {
                    walkPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                walkPathEasyShow = false;
                tempColor = imageWalkPathEasy.color;
                tempColor.a = 0.2f;
                imageWalkPathEasy.color = tempColor;
            //}
        }
    }

    private void ChangeShowFilterWalkPathMiddle(bool clickButton)
    {
        if (clickButton)
        {
            if (walkPathMiddleShow)
            {
                for (int i = 0; i < walkPathMiddleObjectArr.Length; i++)
                {
                    walkPathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                walkPathMiddleShow = false;
               
                tempColor = imageWalkPathMiddle.color;
                tempColor.a = 0.2f;
                imageWalkPathMiddle.color = tempColor;
                pathInfoButton.gameObject.SetActive(false);
                ClosePAthInfo();
            }
            else
            {
                for (int i = 0; i < walkPathMiddleObjectArr.Length; i++)
                {
                    walkPathMiddleObjectArr[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < carPathEasyObjectArr.Length; i++)
                {
                    carPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < bikePathMiddleObjectArr.Length; i++)
                {
                    bikePathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < bikePathHardObjectArr.Length; i++)
                {
                    bikePathHardObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathEasyObjectArr.Length; i++)
                {
                    walkPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathHardObjectArr.Length; i++)
                {
                    walkPathHardObjectArr[i].gameObject.SetActive(false);
                }
                pathInfoButton.gameObject.SetActive(true);
                walkPathMiddleShow = true;
                walkMiddleIconController.OnVirtualClick();
                ClosePAthInfo();
                tempColor = imageWalkPathMiddle.color;
                tempColor.a = 1f;
                imageWalkPathMiddle.color = tempColor;
                tempColor.a = 0.2f;
                imageCarPathEasy.color = tempColor;
                imageBikePathMiddle.color = tempColor;
                imageBikePathHard.color = tempColor;
                imageWalkPathEasy.color = tempColor;
                imageWalkPathHard.color = tempColor;


                carPathEasyShow = false;
                bikePathMiddleShow = false;
                bikePathHardShow = false;
                walkPathEasyShow = false;
                walkPathHardShow = false;
            }
        }
        else
        {
            //if (walkPathMiddleShow)
            //{
            //    for (int i = 0; i < walkPathMiddleObjectArr.Length; i++)
            //    {
            //        walkPathMiddleObjectArr[i].gameObject.SetActive(true);
            //    }

            //    tempColor = imageWalkPathMiddle.color;
            //    tempColor.a = 1f;
            //    imageWalkPathMiddle.color = tempColor;
            //}
            //else
            //{
                for (int i = 0; i < walkPathMiddleObjectArr.Length; i++)
                {
                    walkPathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                walkPathMiddleShow = false;
                tempColor = imageWalkPathMiddle.color;
                tempColor.a = 0.2f;
                imageWalkPathMiddle.color = tempColor;
            //}
        }
    }

    private void ChangeShowFilterWalkPathHard(bool clickButton)
    {
        if (clickButton)
        {
            if (walkPathHardShow)
            {
                for (int i = 0; i < walkPathHardObjectArr.Length; i++)
                {
                    walkPathHardObjectArr[i].gameObject.SetActive(false);
                }
                walkPathHardShow = false;
                
                tempColor = imageWalkPathHard.color;
                tempColor.a = 0.2f;
                imageWalkPathHard.color = tempColor;
                pathInfoButton.gameObject.SetActive(false);
                ClosePAthInfo();
            }
            else
            {
                for (int i = 0; i < walkPathHardObjectArr.Length; i++)
                {
                    walkPathHardObjectArr[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < carPathEasyObjectArr.Length; i++)
                {
                    carPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < bikePathMiddleObjectArr.Length; i++)
                {
                    bikePathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < bikePathHardObjectArr.Length; i++)
                {
                    bikePathHardObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathEasyObjectArr.Length; i++)
                {
                    walkPathEasyObjectArr[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < walkPathMiddleObjectArr.Length; i++)
                {
                    walkPathMiddleObjectArr[i].gameObject.SetActive(false);
                }
                pathInfoButton.gameObject.SetActive(true);
                walkPathHardShow = true;
                walkHardIconController.OnVirtualClick();
                ClosePAthInfo();
                tempColor = imageWalkPathHard.color;
                tempColor.a = 1f;
                imageWalkPathHard.color = tempColor;
                tempColor.a = 0.2f;
                imageCarPathEasy.color = tempColor;
                imageBikePathMiddle.color = tempColor;
                imageBikePathHard.color = tempColor;
                imageWalkPathEasy.color = tempColor;
                imageWalkPathMiddle.color = tempColor;


                carPathEasyShow = false;
                bikePathMiddleShow = false;
                bikePathHardShow = false;
                walkPathEasyShow = false;
                walkPathMiddleShow = false;
            }
        }
        else
        {
            //if (walkPathHardShow)
            //{
            //    for (int i = 0; i < walkPathHardObjectArr.Length; i++)
            //    {
            //        walkPathHardObjectArr[i].gameObject.SetActive(true);
            //    }

            //    tempColor = imageWalkPathHard.color;
            //    tempColor.a = 1f;
            //    imageWalkPathHard.color = tempColor;
            //}
            //else
            //{
                for (int i = 0; i < walkPathHardObjectArr.Length; i++)
                {
                    walkPathHardObjectArr[i].gameObject.SetActive(false);
                }
                walkPathHardShow = false;
                tempColor = imageWalkPathHard.color;
                tempColor.a = 0.2f;
                imageWalkPathHard.color = tempColor;
            //}
        }
    }

    private IEnumerator ContentMove(RectTransform contentRect)
    {
        activeAnim = true;
        tempvec = contentRect.localPosition;
       
        if (!menuOpen)
        {
            panelBar.SetActive(true);
            while (contentRect.localPosition.x > 400)
            {

                tempvec.x = Mathf.Lerp(tempvec.x, 399, menuFilterSpeed * Time.deltaTime);

                //tempVec.y -= Time.deltaTime * speed;
                contentRect.localPosition = tempvec;
                yield return null;
            }
            menuOpen = true;

            if (carPathEasyShow || bikePathMiddleShow || bikePathHardShow || walkPathEasyShow || walkPathMiddleShow || walkPathHardShow)
            {
                pathInfoButton.gameObject.SetActive(true);
            }

        }
        else
        {
            pathInfoButton.gameObject.SetActive(false);
            while (contentRect.localPosition.x < startMenuFilterposition.x)
            {

                tempvec.x = Mathf.Lerp(tempvec.x, startMenuFilterposition.x + 1, menuFilterSpeed * Time.deltaTime);

                //tempVec.y -= Time.deltaTime * speed;
                contentRect.localPosition = tempvec;
               // pathInfoButton.localPosition = new Vector3(pathInfoButton.localPosition.x, pathInfoButton.localPosition.y - 20, pathInfoButton.localPosition.z);
                yield return null;
            }

            //contentRect.gameObject.SetActive(false);
            menuOpen = false;
            panelBar.SetActive(false);
           // pathInfoButton.localPosition = startPathButtonInfoPosition;
            
            ClosePAthInfo();
        }



        //clickText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        activeAnim = false;
    }

    public void ClickButtonOpen()
    {
        if (!activeAnim)
        {
            ////if (!menuOpen)
            ////{
            ////    buttonImage.sprite = buttonImageOpen;
            ////}
            ////else
            ////{
            ////    buttonImage.sprite = buttonImageClose;
            ////}
            StartCoroutine(ContentMove(panelFilter));
            
        }

    }

    public void SetPlaceShow(bool val)
    {
        placeShow = val;
    }

    public void ClickInfoPath()
    {
        if (!pathInfoOpen)
        {
            if (carPathEasyShow)
            {
                infoCarEasy.gameObject.SetActive(true);
            }

            if (bikePathMiddleShow)
            {
                infoBikeMedium.gameObject.SetActive(true);
            }

            if (bikePathHardShow)
            {
                infoBikeHard.gameObject.SetActive(true);
            }

            if (walkPathEasyShow)
            {
                infoWalkPathEasy.gameObject.SetActive(true);
            }

            if (walkPathMiddleShow)
            {
                infoWalkPathMiddle.gameObject.SetActive(true);
            }

            if (walkPathHardShow)
            {
                infoWalkPathHard.gameObject.SetActive(true);
            }

            pathInfoOpen = true;

            

        }
        else
        {
            ClosePAthInfo();
        }
        
    }

    private void ClosePAthInfo()
    {
        infoWalkPathEasy.gameObject.SetActive(false);
        infoWalkPathMiddle.gameObject.SetActive(false);
        infoWalkPathHard.gameObject.SetActive(false);
        infoBikeMedium.gameObject.SetActive(false);
        infoBikeHard.gameObject.SetActive(false);
        infoCarEasy.gameObject.SetActive(false);
        pathInfoOpen = false;
        
    }

}

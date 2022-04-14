using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDataBase : MonoBehaviour
{

    public static BookDataBase Instance { set; get; }
    public struct BookRecord
    {
        public Sprite iconImage;
        public string titleText;
        public Sprite descriptionImage;
        public string descriptionText;
        public int[] region;
        public int category; // 1 - animal, 2 - plant, 3 - place
    }

    public Sprite[] iconImageArr; // 1 - animal, 2 - plant, 3+ - place

    public string[] animalTitleTextArr;
    public Sprite[] animalDescriptionImageArr;
    public string[] animalDescriptionTextArr;

    public string[] plantTitleTextArr;
    public Sprite[] plantDescriptionImageArr;
    public string[] plantDescriptionTextArr;

    public string[] placeTitleTextArr;
    public Sprite[] placeDescriptionImageArr;
    public string[] placeDescriptionTextArr;

    //public BookRecord[] bookDataBase;

    public List<BookRecord> book;



    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitilazeBook();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitilazeBook()
    {
        book = new List<BookRecord>();

        for (int i = 0; i < animalTitleTextArr.Length; i++)
        {
            book.Add(new BookRecord() { iconImage = iconImageArr[0], titleText = animalTitleTextArr[i], descriptionImage = animalDescriptionImageArr[i], descriptionText = animalDescriptionTextArr[i] });
        }
    }
}

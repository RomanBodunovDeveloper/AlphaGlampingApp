using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JsonTest : MonoBehaviour
{
    [Serializable]
    public struct BookRecord
    {
        public int iconImageIndex;
        public string titleText;
        public int descriptionImageIndex;
        public string descriptionText;
        public int[] region;
        public int category; // 1 - animal, 2 - plant, 3 - place
    }

    [Serializable]
    public class BookData
    {
        public List<BookRecord> Items = new List<BookRecord>();
    }
    //[Serializable]
    //public struct BookRecord2
    //{
    //    public string iconImageIndex;
    //    public string titleText;

    //}



    //public List<BookRecord> book;
    //public List<BookRecord2> book2;
    public BookData bookData;
    void Start()
    {
        //Load text from a JSON file (Assets/Resources/Text/jsonFile01.json)
        var jsonTextFile = Resources.Load<TextAsset>("Text/BookRecords");
        bookData = new BookData();
        bookData = JsonUtility.FromJson<BookData>(jsonTextFile.text);

        //book = new List<BookRecord>();
        //book.Add(new BookRecord() { iconImageIndex = 1, titleText ="surok", descriptionImageIndex = 2, descriptionText = "he is cool!", region = new int[] { 1, 2, 3 }, category = 1 });
        //book.Add(new BookRecord() { iconImageIndex = 2, titleText = "baklan", descriptionImageIndex = 3, descriptionText = "he is cool too!", region = new int[] { 2, 3, 4 }, category = 2 });

        //book2 = new List<BookRecord2>();
        //book2.Add(new BookRecord2() { iconImageIndex = "1", titleText = "surok" });
        //book2.Add(new BookRecord2() { iconImageIndex = "2", titleText = "baklan"});


        //bookData.Items.Add(new BookRecord() { iconImageIndex = 1, titleText = "surok", descriptionImageIndex = 2, descriptionText = "he is cool!", region = new int[] { 1, 2, 3 }, category = 1 });
        //bookData.Items.Add(new BookRecord() { iconImageIndex = 2, titleText = "baklan", descriptionImageIndex = 3, descriptionText = "he is cool too!", region = new int[] { 2, 3, 4 }, category = 2 });
        //Debug.Log(JsonUtility.ToJson(bookData));
        //Debug.Log(bookData.Items.Count);
        //Debug.Log(bookData.Items[0].descriptionText);
        //Debug.Log(bookData.Items[1].descriptionText);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    //public string SaveToString()
    //{
    //    return JsonUtility.ToJson(bookData);
    //}


}

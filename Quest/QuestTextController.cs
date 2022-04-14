using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTextController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text questText;
    public RectTransform contentRect;
    private Vector3 tempvec;
    private Color tempColor1;
    private Color tempColor2;
    private int animalCount;
    private int placeCount;
    private int venchileCount;
    private int animalBookCount;
    private int placeBookCount;
    private int plantBookCount;
    private int placeGPSCount;
    public Image contentImage;
    private Vector3 startContentPosition;
    public float speed;
    private bool activeFade;
    private Coroutine tempCoroutine;

    public static QuestTextController instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        startContentPosition = contentRect.localPosition;
        contentRect.gameObject.SetActive(false);
        tempColor1 = contentImage.color;
        tempColor2 = questText.color;
        tempCoroutine = null;
    }

    public void AnimalCountUpdate()
    {
        animalCount = PlayerPrefs.GetInt("Орлан белохвост", 0) + PlayerPrefs.GetInt("Сурок Фил", 0) + PlayerPrefs.GetInt("Сурок Филис", 0) + PlayerPrefs.GetInt("Белка", 0) + PlayerPrefs.GetInt("Филин", 0) + 
            PlayerPrefs.GetInt("Дятел", 0) + PlayerPrefs.GetInt("Лиса", 0) + PlayerPrefs.GetInt("Лось", 0) + PlayerPrefs.GetInt("Уж", 0) + PlayerPrefs.GetInt("Ящерица", 0) + PlayerPrefs.GetInt("Ёж", 0) + 
            PlayerPrefs.GetInt("Синица", 0) + PlayerPrefs.GetInt("Гадюка", 0) + PlayerPrefs.GetInt("Кабан", 0) + PlayerPrefs.GetInt("Волк", 0) + PlayerPrefs.GetInt("Баклан Алексей", 0) + 
            PlayerPrefs.GetInt("Баклан Роман", 0) + PlayerPrefs.GetInt("Чайка Лара", 0) + PlayerPrefs.GetInt("Чайка Лариса", 0) + PlayerPrefs.GetInt("Чайка Марта", 0) + PlayerPrefs.GetInt("Лягушка", 0) + 
            PlayerPrefs.GetInt("Селезень", 0) + PlayerPrefs.GetInt("Утка", 0) + PlayerPrefs.GetInt("Аист", 0) + PlayerPrefs.GetInt("Заяц Ральф", 0) + PlayerPrefs.GetInt("Заяц Роджер", 0) + 
            PlayerPrefs.GetInt("Бобер", 0) + PlayerPrefs.GetInt("Ворона Бренна", 0) + PlayerPrefs.GetInt("Ворона Равен", 0) + PlayerPrefs.GetInt("Лошадь Эклипс", 0) + PlayerPrefs.GetInt("Лошадь Сметанка", 0) + 
            PlayerPrefs.GetInt("Лошадь Инцитат", 0) + PlayerPrefs.GetInt("Лошадь Арвайхээр", 0) + PlayerPrefs.GetInt("Скворец", 0) + PlayerPrefs.GetInt("Воробей", 0) + PlayerPrefs.GetInt("Курица", 0) + 
            PlayerPrefs.GetInt("Гусь", 0) + PlayerPrefs.GetInt("Голубь", 0) + PlayerPrefs.GetInt("Корова Ежевичка", 0) + PlayerPrefs.GetInt("Корова Зорька", 0) + PlayerPrefs.GetInt("Корова Ганготри", 0) + 
            PlayerPrefs.GetInt("Корова Ивонна", 0) + PlayerPrefs.GetInt("Корова Снежка", 0) + PlayerPrefs.GetInt("Цапля", 0);
    }

    public void PlaceCountUpdate()
    {
        placeCount = PlayerPrefs.GetInt("Часовня", 0) + PlayerPrefs.GetInt("Гора Ильинка", 0) + PlayerPrefs.GetInt("Памятный крест", 0) + PlayerPrefs.GetInt("Смотровая", 0) + 
            PlayerPrefs.GetInt("Источник", 0) + PlayerPrefs.GetInt("Порт", 0) + PlayerPrefs.GetInt("Водопад", 0) + PlayerPrefs.GetInt("Старое кладбище", 0) + PlayerPrefs.GetInt("Чернава", 0) + 
            PlayerPrefs.GetInt("Красная речка", 0) + PlayerPrefs.GetInt("Эдельвейс", 0) + PlayerPrefs.GetInt("Сенькино", 0) + PlayerPrefs.GetInt("Альфа-глемпинг", 0) + PlayerPrefs.GetInt("Платоновка", 0) + 
            PlayerPrefs.GetInt("Луговской", 0) + PlayerPrefs.GetInt("Подвалье", 0) + PlayerPrefs.GetInt("Музей", 0);
    }

    public void VenchileCountUpdate()
    {
        venchileCount = PlayerPrefs.GetInt("Diamand", 0) + PlayerPrefs.GetInt("Баржа", 0) + PlayerPrefs.GetInt("Москва", 0) + PlayerPrefs.GetInt("Комбайн", 0);
    }

    public void AnimalBookCountUpdate()
    {
        animalBookCount = PlayerPrefs.GetInt("Сурок_book", 0) + PlayerPrefs.GetInt("Филин_book", 0) + PlayerPrefs.GetInt("Орлан-белохвост_book", 0) + PlayerPrefs.GetInt("Лиса_book", 0) + 
            PlayerPrefs.GetInt("Лось_book", 0) + PlayerPrefs.GetInt("Волк_book", 0) + PlayerPrefs.GetInt("Заяц-русак_book", 0) + PlayerPrefs.GetInt("Еж_book", 0) + PlayerPrefs.GetInt("Бобр_book", 0) + 
            PlayerPrefs.GetInt("Кабан_book", 0) + PlayerPrefs.GetInt("Уж_book", 0) + PlayerPrefs.GetInt("Баклан_book", 0) + PlayerPrefs.GetInt("Большой пестрый дятел_book", 0) + 
            PlayerPrefs.GetInt("Летучая мышь_book", 0) + PlayerPrefs.GetInt("Гадюка_book", 0) + PlayerPrefs.GetInt("Белка_book", 0) + PlayerPrefs.GetInt("Прыткая ящерица_book", 0) + 
            PlayerPrefs.GetInt("Синица_book", 0) + PlayerPrefs.GetInt("Чайка_book", 0) + PlayerPrefs.GetInt("Лягушка_book", 0) + PlayerPrefs.GetInt("Аист_book", 0) + PlayerPrefs.GetInt("Скворец_book", 0) + 
            PlayerPrefs.GetInt("Цапля_book", 0);
    }

    public void PlaceBookCountUpdate()
    {
        placeBookCount = PlayerPrefs.GetInt("Порт_book", 0) + PlayerPrefs.GetInt("Часовня пророка Ильи_book", 0) + PlayerPrefs.GetInt("Гора Ильинка_book", 0) + PlayerPrefs.GetInt("Источник_book", 0) + 
            PlayerPrefs.GetInt("Водопад_book", 0) + PlayerPrefs.GetInt("Смотровая_book", 0) + PlayerPrefs.GetInt("Кладбище_book", 0) + PlayerPrefs.GetInt("Крест_book", 0) + PlayerPrefs.GetInt("Чернава_book", 0) + 
            PlayerPrefs.GetInt("Красная речка_book", 0) + PlayerPrefs.GetInt("Музей в Платоновке_book", 0) + PlayerPrefs.GetInt("Село Подвалье_book", 0) + PlayerPrefs.GetInt("Деревня Сенькино_book", 0) + 
            PlayerPrefs.GetInt("Поселение Эдельвейс_book", 0) + PlayerPrefs.GetInt("Село Платоновка_book", 0) + PlayerPrefs.GetInt("Поселок Луговской_book", 0) + PlayerPrefs.GetInt("Альфа-Глэмпинг_book", 0);
    }

    public void PlantBookCountUpdate()
    {
        plantBookCount = PlayerPrefs.GetInt("Тысячелистник_book", 0) + PlayerPrefs.GetInt("Копе́ечник крупноцветко́вый_book", 0) + PlayerPrefs.GetInt("Тонконо́г жестколи́стный_book", 0) + 
            PlayerPrefs.GetInt("Копеечник Гмелина_book", 0) + PlayerPrefs.GetInt("Астрагал Гельма_book", 0) + PlayerPrefs.GetInt("Скабиоза исетская_book", 0) + PlayerPrefs.GetInt("Дре́млик тёмно-кра́сный_book", 0) + 
            PlayerPrefs.GetInt("Исто́д сиби́рский_book", 0) + PlayerPrefs.GetInt("Бурачок ленский_book", 0);
    }

    public void PlaceGPSCountUpdate()
    {
        placeGPSCount = PlayerPrefs.GetInt("Порт_gps", 0) + PlayerPrefs.GetInt("Часовня пророка Ильи_gps", 0) + PlayerPrefs.GetInt("Гора Ильинка_gps", 0) + PlayerPrefs.GetInt("Источник_gps", 0) + 
            PlayerPrefs.GetInt("Водопад_gps", 0) + PlayerPrefs.GetInt("Смотровая_gps", 0) + PlayerPrefs.GetInt("Кладбище_gps", 0) + PlayerPrefs.GetInt("Крест_gps", 0) + PlayerPrefs.GetInt("Чернава_gps", 0) + 
            PlayerPrefs.GetInt("Красная речка_gps", 0) + PlayerPrefs.GetInt("Музей в Платоновке_gps", 0);
    }

    public void SetAnimalText()
    {
        if (tempCoroutine!=null)
        {
            StopCoroutine(tempCoroutine);
            tempCoroutine = null;
            AnimalCountUpdate();
            tempColor1.a = 1;
            tempColor2.a = 1;
            contentImage.color = tempColor1;
            questText.color = tempColor2;
            questText.text = $"Животных обнаружено {animalCount}/44";
            StartCoroutine(RestartContentFade());
        }
        else
        {
            contentRect.gameObject.SetActive(true);
            AnimalCountUpdate();
            questText.text = $"Животных обнаружено {animalCount}/44";
            StartCoroutine(ContentMove());
        }
    }

    public void SetPlaceText()
    {
        if (tempCoroutine != null)
        {
            StopCoroutine(tempCoroutine);
            tempCoroutine = null;
            PlaceCountUpdate();
            tempColor1.a = 1;
            tempColor2.a = 1;
            contentImage.color = tempColor1;
            questText.color = tempColor2;
            questText.text = $"Места обнаружено {placeCount}/17";
            StartCoroutine(RestartContentFade());
        }
        else
        {
            contentRect.gameObject.SetActive(true);
            PlaceCountUpdate();
            questText.text = $"Места обнаружено {placeCount}/17";
            StartCoroutine(ContentMove());
        }
    }

    public void SetVenchileText()
    {
        if (tempCoroutine != null)
        {
            StopCoroutine(tempCoroutine);
            tempCoroutine = null;
            VenchileCountUpdate();
            tempColor1.a = 1;
            tempColor2.a = 1;
            contentImage.color = tempColor1;
            questText.color = tempColor2;
            questText.text = $"Техника обнаружено {venchileCount}/4";
            StartCoroutine(RestartContentFade());
        }
        else
        {
            contentRect.gameObject.SetActive(true);
            VenchileCountUpdate();
            questText.text = $"Техника обнаружено {venchileCount}/4";
            StartCoroutine(ContentMove());
        }
    }

    private IEnumerator ContentMove()
    {
        tempvec = contentRect.localPosition;

        while (contentRect.localPosition.x > -610 - 620 / 2)
        {

            tempvec.x = Mathf.Lerp(tempvec.x, -620 - 620 / 2, speed);

            //tempVec.y -= Time.deltaTime * speed;
            contentRect.localPosition = tempvec;
            yield return null;
        }


        //clickText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        if (tempCoroutine == null)
        {
            tempCoroutine = StartCoroutine(ContentFade());

        }
    }

    private IEnumerator ContentFade()
    {
        activeFade = true;
        
        while (tempColor1.a > 0)
        {
            tempColor1.a -= Time.deltaTime;
            tempColor2.a = tempColor1.a;

            contentImage.color = tempColor1;
            questText.color = tempColor2;
            
            yield return null;
        }
        contentRect.gameObject.SetActive(false);
        tempColor1.a = 1;
        tempColor2.a = 1;
        tempvec = Vector3.zero;
        contentRect.localPosition = startContentPosition;
        contentImage.color = tempColor1;
        questText.color = tempColor2;
        activeFade = false;
        tempCoroutine = null;
    }

    private IEnumerator RestartContentFade()
    {

        yield return new WaitForSeconds(1f);
        //yield return null;
        if (tempCoroutine == null)
        {
            //Debug.Log("restart cor");
            tempColor1.a = 1;
            tempColor2.a = 1;
            contentImage.color = tempColor1;
            questText.color = tempColor2;

            yield return new WaitForSeconds(2f);
            tempCoroutine = StartCoroutine(ContentFade());
            

            

        }
    }








    public void SetAnimalBookText()
    {
        if (tempCoroutine != null)
        {
            StopCoroutine(tempCoroutine);
            tempCoroutine = null;
            AnimalBookCountUpdate();
            //tempColor1.a = 1;
            //tempColor2.a = 1;
            //contentImage.color = tempColor1;
            //questText.color = tempColor2;
            questText.text = $"Животных изучено {animalBookCount}/23";
            StartCoroutine(RestartContentFade());
        }
        else
        {
            contentRect.gameObject.SetActive(true);
            AnimalBookCountUpdate();
            questText.text = $"Животных изучено {animalBookCount}/23";
            StartCoroutine(ContentMove());
        }
    }

    public void SetPlaceBookText()
    {
        if (tempCoroutine != null)
        {
            StopCoroutine(tempCoroutine);
            tempCoroutine = null;
            PlaceBookCountUpdate();
            tempColor1.a = 1;
            tempColor2.a = 1;
            contentImage.color = tempColor1;
            questText.color = tempColor2;
            questText.text = $"Мест изучено {placeBookCount}/17";
            StartCoroutine(RestartContentFade());
        }
        else
        {
            contentRect.gameObject.SetActive(true);
            PlaceBookCountUpdate();
            questText.text = $"Мест изучено {placeBookCount}/17";
            StartCoroutine(ContentMove());
        }
    }

    public void SetPlantBookText()
    {
        if (tempCoroutine != null)
        {
            StopCoroutine(tempCoroutine);
            tempCoroutine = null;
            PlantBookCountUpdate();
            tempColor1.a = 1;
            tempColor2.a = 1;
            contentImage.color = tempColor1;
            questText.color = tempColor2;
            questText.text = $"Растений изучено {plantBookCount}/9";
            StartCoroutine(RestartContentFade());
        }
        else
        {
            contentRect.gameObject.SetActive(true);
            PlantBookCountUpdate();
            questText.text = $"Растений изучено {plantBookCount}/9";
            StartCoroutine(ContentMove());
        }
    }

    public void SetPlaceGPSText(string showText)
    {
        if (tempCoroutine != null)
        {
            StopCoroutine(tempCoroutine);
            tempCoroutine = null;
            PlaceGPSCountUpdate();
            tempColor1.a = 1;
            tempColor2.a = 1;
            contentImage.color = tempColor1;
            questText.color = tempColor2;
            //questText.text = $"Мест посещено {placeGPSCount}/11";
            questText.text = $"{showText} - исследовано!";
            StartCoroutine(RestartContentFade());
        }
        else
        {
            contentRect.gameObject.SetActive(true);
            PlaceGPSCountUpdate();
            //questText.text = $"Мест посещено {placeGPSCount}/11";
            questText.text = $"{showText} - исследовано!";
            StartCoroutine(ContentMove());
        }
    }
}

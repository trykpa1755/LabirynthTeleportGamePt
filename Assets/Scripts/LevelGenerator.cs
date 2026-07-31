using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public float offset = 5f;

    public Material material01;
    public Material material02;

    void GenerateTile(int x, int z)
    {
        //Pobieramy kolor pixela w pozycji x i y
        Color pixelColor = map.GetPixel(x, z);
        //Je¿eli alpha koloru jest równa 0, czyli kolor jest w pe³ni przezroczysty to pomijamy pixel
        if (pixelColor.a == 0)
        {
            return;
        }
        //Przeszukujemy po wszystkich kolorach w naszej tablicy
        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            //Je¿eli któryœ z kolorów w naszej tablicy odpowiada to ustaw prefab
            if (colorMapping.color.Equals(pixelColor))
            {
                //wylicz pozycje na podstawie wspó³rzêdnych pixela
                Vector3 position = new Vector3(x, 0, z) * offset;
                //Utwórz wybrany obiekt w wybranej pozycji
                Instantiate(colorMapping.prefab, position, Quaternion.identity,
                transform);
            }
        }
    }


    public void GenerateLabirynth()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int z = 0; z < map.height; z++)
            {
                GenerateTile(x, z);
            }
        }
        ColorTheChildren();
    }


    public void ColorTheChildren()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Wall")
            {
                if (Random.Range(1, 100) % 3 == 0)
                {
                    child.gameObject.GetComponent<Renderer>().material = material02;
                }
                else
                {
                    child.gameObject.GetComponent<Renderer>().material = material01;
                }
            }

            if (child.childCount > 0)
            {
                foreach (Transform grandchild in child.transform)
                {
                    if (grandchild.tag == "Wall")
                    {
                        if (Random.Range(1, 100) % 3 == 0)
                        {
                            grandchild.gameObject.GetComponent<Renderer>().material =
                            material02;
                        }
                        else
                        {
                            grandchild.gameObject.GetComponent<Renderer>().material =
                            material01;
                        }
                    }
                }
            }
        }
    }


}


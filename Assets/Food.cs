using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    //Food nesnesinin sahnede spawnlanaca�� yerler i�in s�n�r.
    public BoxCollider2D gridArea;

    void Start()
    {
        RandomizePosition();
    }

    void Update()
    {
        
    }


    // Food nesnesinin sahnede spawnlanmas� i�in fonksiyon.
    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    //Oyundaki Snake nesnesi, Food nesnesi ile temas etti�inde RandomizePosition fonksiyonu aktifle�ecek.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Unity'de Snake nesnesinin Tag'ini Player olarak de�i�tirmemiz gerekiyor if blo�unun �al��abilmesi i�in.
        if (other.tag == "Player")
        {
            RandomizePosition();
        }
    }
}

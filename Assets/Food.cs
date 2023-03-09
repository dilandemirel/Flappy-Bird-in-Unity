using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    //Food nesnesinin sahnede spawnlanacaðý yerler için sýnýr.
    public BoxCollider2D gridArea;

    void Start()
    {
        RandomizePosition();
    }

    void Update()
    {
        
    }


    // Food nesnesinin sahnede spawnlanmasý için fonksiyon.
    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    //Oyundaki Snake nesnesi, Food nesnesi ile temas ettiðinde RandomizePosition fonksiyonu aktifleþecek.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Unity'de Snake nesnesinin Tag'ini Player olarak deðiþtirmemiz gerekiyor if bloðunun çalýþabilmesi için.
        if (other.tag == "Player")
        {
            RandomizePosition();
        }
    }
}

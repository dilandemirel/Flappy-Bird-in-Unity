using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Yýlanýn yönünü(saða) ve x,y koordinatlarýnda hareket edebileceðini belirledik.
    private Vector2 direction = Vector2.right;

    // Yeni liste 
    private List<Transform> segments = new List<Transform>();

    // Prefab referansý klonlama için
    public Transform segmentPrefab;

    // Snake nesnesinin baþlangýç boyutu
    public int initialSize = 4;

    void Start()
    {
        //Liste baþlangýcý
        segments = new List<Transform>();
        //Tek segment
        segments.Add(this.transform);
        //Boyutun baþta da restart fonskiyonu gibi çalýþmasý için
        ResetState();
    }

    void Update()
    {
        //Kullanýcýnýn girdiði tuþ komutlarýnýn yýlaný hangi yönde hareket ettireceðini belirledik.
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }

    }

    private void FixedUpdate()
    {
        // Yýlanýn büyüme mantýðý
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y ) + direction.y, 0.0f);
    }

    private void Grow()
    {
        // Klonlama
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);

    }

    private void ResetState()
    {
        for (int i = 1; i< segments.Count; i++)
        {
            // Yok edilecek segmentler
            Destroy(segments[i].gameObject);
        }

        // Liste temizlenir ve head kýsmý tekrar eklenir.
        segments.Clear();
        segments.Add(this.transform);

        // Snake nesnesinin baþlangýç boyutu ve devamý için.
        for (int i = 1; i< this.initialSize; i++)
        {
            segments.Add(Instantiate(this.segmentPrefab));
        }

        // Snake'i baþlangýç konumuna geri getirmek için
        this.transform.position = Vector3.zero;
        // Snake nesnesinin tekrar sað yönlü baþlamasý için
        this.direction = Vector2.right;
    }

    // Snake nesnesi Food nesnesi ile karþýlaþtýðýnda Grow fonksiyonu etkinleþecek.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Unity'de Food nesnesinin Tag'ini Food olarak deðiþtirdik.                               
        if (other.tag == "Food")
        {
            Grow();
        }
        // Snake nesnesi herhangi bir engelle karþýlaþtýðýnda ResetState fonksiyonu etkinleþecek. Obstacle Tag'i Snake nesnesinin head kýsmý hariç kalan parçalarý ve duvarlar.
        else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }
}

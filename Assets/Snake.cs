using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Y�lan�n y�n�n�(sa�a) ve x,y koordinatlar�nda hareket edebilece�ini belirledik.
    private Vector2 direction = Vector2.right;

    // Yeni liste 
    private List<Transform> segments = new List<Transform>();

    // Prefab referans� klonlama i�in
    public Transform segmentPrefab;

    // Snake nesnesinin ba�lang�� boyutu
    public int initialSize = 4;

    void Start()
    {
        //Liste ba�lang�c�
        segments = new List<Transform>();
        //Tek segment
        segments.Add(this.transform);
        //Boyutun ba�ta da restart fonskiyonu gibi �al��mas� i�in
        ResetState();
    }

    void Update()
    {
        //Kullan�c�n�n girdi�i tu� komutlar�n�n y�lan� hangi y�nde hareket ettirece�ini belirledik.
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
        // Y�lan�n b�y�me mant���
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

        // Liste temizlenir ve head k�sm� tekrar eklenir.
        segments.Clear();
        segments.Add(this.transform);

        // Snake nesnesinin ba�lang�� boyutu ve devam� i�in.
        for (int i = 1; i< this.initialSize; i++)
        {
            segments.Add(Instantiate(this.segmentPrefab));
        }

        // Snake'i ba�lang�� konumuna geri getirmek i�in
        this.transform.position = Vector3.zero;
        // Snake nesnesinin tekrar sa� y�nl� ba�lamas� i�in
        this.direction = Vector2.right;
    }

    // Snake nesnesi Food nesnesi ile kar��la�t���nda Grow fonksiyonu etkinle�ecek.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Unity'de Food nesnesinin Tag'ini Food olarak de�i�tirdik.                               
        if (other.tag == "Food")
        {
            Grow();
        }
        // Snake nesnesi herhangi bir engelle kar��la�t���nda ResetState fonksiyonu etkinle�ecek. Obstacle Tag'i Snake nesnesinin head k�sm� hari� kalan par�alar� ve duvarlar.
        else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    private float _livingTimer = 0;

    public float explosionTimer = 3f;
    public float scalingRate = 1f;
    public float shakingRate = 0.1f;
    public float explosionRadius = 500f;

    void Update()
    {
        if (_livingTimer < explosionTimer)
        {
            _livingTimer += scalingRate * Time.deltaTime;
            float shakingValue = Random.Range(-shakingRate, shakingRate);
            gameObject.transform.localScale = new Vector3(0 + _livingTimer + shakingValue, 0 + _livingTimer + shakingValue);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.y * 0.01f);
        }
        else
        {
            GameObject farmer = GameObject.Find("Farmer");
            GameObject dog = GameObject.Find("Dog");
            GameObject player = GameObject.Find("Player");

            float farmerDist = Vector2.Distance(farmer.transform.position, gameObject.transform.position);
            float dogDist = Vector2.Distance(dog.transform.position, gameObject.transform.position);
            float playerDist = Vector2.Distance(player.transform.position, gameObject.transform.position);

            if (farmerDist < explosionRadius)
            {
                farmer.GetComponent<AI>().SetDirty();
            }
            if (dogDist < explosionRadius)
            {
                dog.GetComponent<AI>().SetDirty();
            }
            if (playerDist < explosionRadius)
            {
                if (Random.Range(0, 100) > 50)
                {
                    player.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                else
                { 
                    player.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
            Destroy(gameObject);
        }    
    }
}

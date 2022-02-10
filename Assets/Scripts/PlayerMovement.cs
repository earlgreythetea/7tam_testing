using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float vertAngle = 0.12f;
    public float speed = 10;
    public float touchSensitivity;
    [SerializeField] public Sprite[] playerSprites;
    [SerializeField] public GameObject bombPrefab;

    private SpriteRenderer _renderer;
    private GameObject _bomb;

    void Start()
    {
         _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Touch touch = new Touch();
        float vMovement = 0;
        float hMovement = 0;
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            //Movement
            vMovement = Input.GetAxis("Vertical") * speed;
            hMovement = Input.GetAxis("Horizontal") * speed;
            //Bomb creation
            if (Input.GetKeyDown(KeyCode.Space) && _bomb == null)
            {
                _bomb = Instantiate(bombPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            }
        }
        else if (Input.touchSupported)
        {
            //Movement
            touch = Input.GetTouch(0);
            Vector3 position = touch.position;
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, Camera.main.nearClipPlane));
            if (Mathf.Abs(touchPos.x - gameObject.transform.position.x) >= touchSensitivity)
            {
                if (touchPos.x > gameObject.transform.position.x)
                    hMovement = 1 * speed;
                else
                    hMovement = -1 * speed;
            }
            if (Mathf.Abs(touchPos.y - gameObject.transform.position.y) >= touchSensitivity)
            {
                if (touchPos.y > gameObject.transform.position.y)
                    vMovement = 1 * speed;
                else
                    vMovement = -1 * speed;
            }
            if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                vMovement = 0;
                hMovement = 0;
            }
            //Bomb creation
            if (touch.phase == TouchPhase.Began &&
                Mathf.Abs(touchPos.x - gameObject.transform.position.x) < touchSensitivity &&
                    Mathf.Abs(touchPos.y - gameObject.transform.position.y) < touchSensitivity &&
                    _bomb == null)
            {
                _bomb = Instantiate(bombPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            }

        }
        

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(hMovement + vMovement * vertAngle, vMovement);

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.y * 0.01f);
        if (Mathf.Abs(vMovement + hMovement) > 0)
        {
            if (vMovement > hMovement)
            {
                if (vMovement > 0)
                {
                    _renderer.sprite = playerSprites[3];
                }
                else
                {
                    _renderer.sprite = playerSprites[1];
                }
            }
            else
            {
                if (hMovement > 0)
                {
                    _renderer.sprite = playerSprites[0];
                }
                else
                {
                    _renderer.sprite = playerSprites[2];
                }
            }
        }

    }
}

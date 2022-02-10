using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] public Sprite[] calmSprites;
    [SerializeField] public Sprite[] angrySprites;
    [SerializeField] public Sprite[] dirtySprites;

    public float stunTimer = 2f;
    public float walkingTimer = 3f;
    public float speed = 1f;
    public float depth = 0;

    private int _state; //0 - calm, 1 - angry, 2 - dirty
    private SpriteRenderer _renderer;
    private float _stunTimer;
    private Vector3 _direction;
    private int _spriteIndex;
    private float _walkingTimer = 0;


    private void Start()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        ChooseDirection();
    }
    public void SetCalm()
    {
        _state = 0;
    }
    public void SetAgressive()
    {
        _state = 1;
    }
    public void SetDirty()
    {
        _state = 2;
        _stunTimer = 0;
    }

    void ChooseDirection()
    {
        Vector2[] directions = new Vector2[] { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0.12f, 1), new Vector2(-0.12f, -1) };
        int index = 0;

        index = Random.Range(0, directions.Length);

        _direction = directions[index] * speed;
        _spriteIndex = index;
    }
    void Update()
    {
        Sprite[] targetSpriteArray;
        switch (_state)
        {
            case 0:
            default:
                {
                    targetSpriteArray = calmSprites;
                    break;
                }
            case 1:
                {
                    targetSpriteArray = angrySprites;
                    break;
                }
            case 2:
                {
                    targetSpriteArray = dirtySprites;
                    break;
                }
        }
        _renderer.sprite = targetSpriteArray[_spriteIndex];
        
        if (_state == 0)
        {
            if (_walkingTimer < walkingTimer)
            {
                _walkingTimer += 1 * Time.deltaTime;
                gameObject.GetComponent<Rigidbody2D>().velocity = _direction;
            }
            else
            {
                _walkingTimer = 0;
                ChooseDirection();
            }
        }
        if (_state == 2)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (_stunTimer < stunTimer)
            {
                _stunTimer += 1 * Time.deltaTime;
            }
            else
            {
                SetCalm();
            }
        }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, depth + gameObject.transform.position.y * 0.01f);
    }

}

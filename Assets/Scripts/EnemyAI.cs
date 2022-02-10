using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] public Sprite[] calmSprites;
    [SerializeField] public Sprite[] angrySprites;
    [SerializeField] public Sprite[] dirtySprites;

    public float stunTimer = 2f;
    public float walkingTimer = 3f;
    private int _state; //0 - calm, 1 - angry, 2 - dirty
    private SpriteRenderer _renderer;
    private float _stunTimer;
    private Vector3 _direction;
    private int _spriteIndex;
    private float _walkingTimer = 0;

    // Start is called before the first frame update
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
    // Update is called once per frame
    void ChooseDirection()
    {
        Vector2[] directions = new Vector2[] { new Vector2(50, 0), new Vector2(-50, 0), new Vector2(0, 50), new Vector2(0, -50)};
        Vector3 direction = new Vector3();
        bool go;
        int index = 0;
        do
        {
            go = true;
            index = Random.Range(0, directions.Length);
            direction = directions[index];
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
            if (hit.collider.tag == "Block")
            {
                go = false;
            }
        } while (!go);
        _direction = direction;
        _spriteIndex = index;
    }
    void Update()
    {
        Sprite[] targetSpriteArray;
        switch(_state)
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
            }
            else
            {
                _walkingTimer = 0;
                ChooseDirection();
            }
        }
        if (_state == 2)
        {
            if (_stunTimer < stunTimer)
            {
                _stunTimer += 1 * Time.deltaTime;
            }
            else
            {
                SetCalm();
            }
        }
    }

}

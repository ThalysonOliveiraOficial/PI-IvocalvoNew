using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControle : MonoBehaviour
{
    [SerializeField] GameObject _obj1;
    [SerializeField] SpriteRenderer _spr;
    [SerializeField] List<SpriteRenderer> _listImgs;
    [SerializeField] Sprite _img;
    [SerializeField] Color _cor;

    // Start is called before the first frame update
    void Start()
    {
  
        for (int i = 0; i < _listImgs.Count; i++)
        {
            _listImgs[i].sprite = _img;
            _listImgs[i].color = _cor;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

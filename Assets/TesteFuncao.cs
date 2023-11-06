using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Pool;

public class TesteFuncao : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] SpriteRenderer _spriteRenderer;

    [SerializeField] List<Transform> _pos;
    [SerializeField] int _number;
    [SerializeField] List<GameObject> _inimigoList;
    public void ChamarInimigo()
    {
        GameObject bullet = InimigosPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = new Vector3(bullet.transform.position.x+ _number,bullet.transform.position.y, bullet.transform.position.z);
            _number++;
            _inimigoList.Add(bullet);

            // bullet.transform.position = turret.transform.position;
            // bullet.transform.rotation = turret.transform.rotation;
            bullet.SetActive(true);
         
            _spriteRenderer =bullet.GetComponent<SpriteRenderer>();
            _spriteRenderer.color = Color.red;
            if(_number == _pos.Count)
            {
                _number = 0;
            }

        }
    }

    public void Verde()
    {
        for (int i = 0; i < _inimigoList.Count; i++)
        {
            _inimigoList[i].GetComponent<SpriteRenderer>().color = Color.green;

            if(_inimigoList[i].GetComponent<SpriteRenderer>().color == Color.green)
            {
                _inimigoList[i].GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
        
    }
}

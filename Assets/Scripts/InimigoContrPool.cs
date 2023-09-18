using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoContrPool : MonoBehaviour
{
    public bool _chamarInimigos;
    float _checkTime;
    [SerializeField] float _timeLimit;
    public Transform[] _posIniSpawn;
    public int _numbPosSpawn;

    // Start is called before the first frame update
    void Start()
    {
        _checkTime = _timeLimit;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        _checkTime -= Time.deltaTime;
        if (_checkTime < 0)
        {
             InimigoOn_1();
             _checkTime = _timeLimit;
        }
        
    }

    void InimigoOn_1()
    {
        GameObject bullet = InimigoPool_1.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = _posIniSpawn[0].transform.localPosition;  
            // bullet.transform.rotation = turret.transform.rotation;
            bullet.SetActive(true);
        }
    }
}

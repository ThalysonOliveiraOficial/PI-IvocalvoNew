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

    public GameControl _gameCrtl;

    public List<GameObject> _iniVivoL;
    public List<GameObject> _iniMortoL;

    // Start is called before the first frame update
    void Start()
    {
        _gameCrtl = GetComponent<GameControl>();
        _checkTime = _timeLimit;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        _checkTime -= Time.deltaTime;
        if (_checkTime < 0)
        {
             InimigoOn_1();
             InimigoOn_2();
             InimigoOn_3();
             _checkTime = _timeLimit;
        }
        
    }

    void InimigoOn_1()
    {
        GameObject bullet = IniPool1.SharedInstance.GetPooledObject();
        if (bullet != null)
        {

            _numbPosSpawn = Random.Range(0, _posIniSpawn.Length);
            bullet.transform.localPosition = _posIniSpawn[_numbPosSpawn].transform.position;
            /*
            bullet.transform.SetParent(_posIniSpawn[0]);
            bullet.transform.position = new Vector3(0, 0, 0);
            */

            _iniVivoL.Add(bullet);
            _iniMortoL.Remove(bullet);

            bullet.GetComponent<ControleInimigo>()._iniLife = bullet.GetComponent<ControleInimigo>()._iLifeini;
            bullet.GetComponent<ControleInimigo>()._player = _gameCrtl._player;

            bullet.SetActive(true);

            bullet.GetComponent<HitInimigo>().RestartIni();
        }
    }

    void InimigoOn_2()
    {
        GameObject bullet = IniPool2.SharedInstance.GetPooledObject();
        if (bullet != null)
        {

            _numbPosSpawn = Random.Range(0, _posIniSpawn.Length);
            bullet.transform.localPosition = _posIniSpawn[_numbPosSpawn].transform.position;
            /*
            bullet.transform.SetParent(_posIniSpawn[0]);
            bullet.transform.position = new Vector3(0, 0, 0);
            */

            _iniVivoL.Add(bullet);
            _iniMortoL.Remove(bullet);

            bullet.GetComponent<ControleInimigo>()._iniLife = bullet.GetComponent<ControleInimigo>()._iLifeini;
            bullet.GetComponent<ControleInimigo>()._player = _gameCrtl._player;

            bullet.SetActive(true);

            bullet.GetComponent<HitInimigo>().RestartIni();
        }
    }
    void InimigoOn_3()
    {
        GameObject bullet = IniPool3.SharedInstance.GetPooledObject();
        if (bullet != null)
        {

            _numbPosSpawn = Random.Range(0, _posIniSpawn.Length);
            bullet.transform.localPosition = _posIniSpawn[_numbPosSpawn].transform.position;
            /*
            bullet.transform.SetParent(_posIniSpawn[0]);
            bullet.transform.position = new Vector3(0, 0, 0);
            */

            _iniVivoL.Add(bullet);
            _iniMortoL.Remove(bullet);

            bullet.GetComponent<ControleInimigo>()._iniLife = bullet.GetComponent<ControleInimigo>()._iLifeini;
            bullet.GetComponent<ControleInimigo>()._player = _gameCrtl._player;

            bullet.SetActive(true);

            bullet.GetComponent<HitInimigo>().RestartIni();
        }
    }

}

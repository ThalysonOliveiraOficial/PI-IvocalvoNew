using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]

public class ItemDados : ScriptableObject
{
    [SerializeField] string _nome;
    [SerializeField] int _tipo;
    [SerializeField] Sprite _img;
    
    public string Nome { get { return _nome; } set { _nome = value; } }

    public int Tipo { get { return _tipo; } set { _tipo = value; } }

    public Sprite Img { get { return _img; } set { _img = value; } }

}

﻿namespace libs;

public class GameObject : IGameObject, IMovement, ICloneable
{


    private char _charRepresentation = '#';
    private ConsoleColor _color;

    private int _posX;
    private int _posY;

    private int _prevPosX;
    private int _prevPosY;

    public GameObjectType Type;

    public GameObject() {
        this._posX = 5;
        this._posY = 5;
        this._color = ConsoleColor.Gray;
    }

    public GameObject(int posX, int posY){
        this._posX = posX;
        this._posY = posY;
    }

    public GameObject(int posX, int posY, ConsoleColor color){
        this._posX = posX;
        this._posY = posY;
        this._color = color;
    }

    

    public char CharRepresentation
    {
        get { return _charRepresentation ; }
        set { _charRepresentation = value; }
    }

    public ConsoleColor Color
    {
        get { return _color; }
        set { _color = value; }
    }

    public int PosX
    {
        get { return _posX; }
        set { _posX = value; }
    }

    public int PosY
    {
        get { return _posY; }
        set { _posY = value; }
    }

    public int GetPrevPosY() {
        return _prevPosY;
    }

    public int GetPrevPosX() {
        return _prevPosX;
    }

    public void SetPrevPosX(int pX){
        _prevPosX = pX;
    }

    public void SetPrevPosY(int pY){
        _prevPosY = pY;
    }

    public virtual void Move(int dx, int dy) {
        _prevPosX = _posX;
        _prevPosY = _posY;
        _posX += dx;
        _posY += dy;
    }

    public object Clone()
    {
        return new GameObject(PosX, PosY, Color)
        {
            CharRepresentation = this.CharRepresentation,
            Type = this.Type,
            // Ensure all properties are copied
        };
    }

    // virtual class so i dont have to implement it (e.g in walls)
    public virtual void Interact()
    {
        // do nothing by default
    }

}

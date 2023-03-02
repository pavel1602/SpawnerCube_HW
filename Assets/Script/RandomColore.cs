using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomColore : MonoBehaviour
{
    [SerializeField] private float _recoloringDuration = 2f;
    [SerializeField] private float _pauseDuration = 1f;
    private Color _startColor;
    private Color _nextColor;
    private Renderer _renderer;
    private float _recoloringTime;
    private float _pauseTime;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        GenerateNextColor();
    }

    private void GenerateNextColor()
    {
        _startColor = _renderer.material.color;
        _nextColor = Random.ColorHSV(0f, 1f, 0.8f, 1f,
            1f,1f);
    }
    private void Update()
    {
        if (_pauseTime > 0f)
        {
            _pauseTime -= Time.deltaTime;
            return;
        }
        
        _recoloringTime += Time.deltaTime;
        var progress = _recoloringTime / _recoloringDuration;
        var currentColor = Color.Lerp(_startColor, _nextColor, progress);
        _renderer.material.color = currentColor;

        if (_recoloringTime >= _recoloringDuration)
        {
            _recoloringTime = 0f;
            GenerateNextColor();
            _pauseTime = _pauseDuration;
        }
    }
}

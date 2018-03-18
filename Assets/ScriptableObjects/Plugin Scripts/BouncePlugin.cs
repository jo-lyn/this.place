using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BouncePlugin : BlockPlugin
{
    private const float _amplitudeY = 0.5f;
    private const float _omegaY = 2.0f;
    private float _deltaTime;
    private float _lowerBound;
    private float _initScale = 1.0f;
    private float _targetScale = 0.7f;
    private float _currentScale = 1.0f;
    private float _deltaScale = -0.01f;
    private float _offsetPos;
    private bool isCompressed = false;
    float numFlips = 0;


    public override void OnUpdate ()
    {
        float deltaY = Mathf.Sin(_omegaY * _deltaTime);
        float posY = Mathf.Abs(_amplitudeY * deltaY);

        _offsetPos = (_initScale - _currentScale) / 2;

        _block.transform.localPosition = new Vector3(0, posY, 0);

        //CompressBlock();

        _deltaTime += Time.deltaTime;

        if (_block.transform.localPosition.y < 0.2f)
        {
           // isCompressed = false;
        }
    }

    private void CompressBlock()
    {
        _currentScale += _deltaScale;

        if (_currentScale > _initScale || _currentScale < _targetScale)
        {
            _deltaScale *= -1;
            
            numFlips++;
            if (numFlips == 2)
            {
                isCompressed = true;
                numFlips = 0;
            }
        }

        if (!isCompressed)
        {
            _block.transform.localScale = new Vector3(1.0f, _currentScale, 1.0f);
        }

    }
}

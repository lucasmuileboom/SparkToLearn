﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ObjectSpawner : MonoBehaviour
{
    public static void Instantiate(GameObject spawnObject)
    {
        Instantiate<GameObject>(spawnObject);
    }

    //Spawns a object that hovers around the ground as a highlight
    public static IEnumerator HighlightObjectOnRaycastHit(GameObject orientation, GameObject spawnObject, Func<bool> RotateLeft, Func<bool> RotateRight, float rotateSpeed, Func<GameObject,bool> breakCondition, LayerMask mask)
    {
        Quaternion _hitRotation;
        GameObject _highlight = null;
        float _rotated = 0;
        yield return new WaitForEndOfFrame();

        while (true)
        {
            //Raycast setup
            RaycastHit hit;
            Ray ray = new Ray(orientation.transform.position, orientation.transform.forward);

            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                _highlight = (_highlight == null) ? Instantiate<GameObject>(spawnObject) : _highlight;
                //Rotates the object so it alligns with the surface
                _hitRotation = Quaternion.Euler(hit.normal.x, hit.normal.y, hit.normal.z) * hit.collider.transform.rotation;
                
                _highlight.transform.SetPositionAndRotation(hit.point, _hitRotation);

                _highlight.transform.RotateAround(_highlight.transform.position,_highlight.transform.up, _rotated);

                //If the left rotation key is pressed and not the right rotation key: rotate left
                if (RotateLeft() && !RotateRight())
                {
                    _rotated -= rotateSpeed;
                }
                //If the right rotation key is pressed and not the left rotation key: rotate right
                if (RotateRight() && !RotateLeft())
                {
                    _rotated += rotateSpeed;
                }
            }
            yield return new WaitForEndOfFrame();

            //If the stop condition is met, then stop coroutine
            if (breakCondition(_highlight))
            {
                break;
            }
        }
    }

    //Rotates the object instance
    public static IEnumerator RotateObject(GameObject instance, Func<bool> RotateLeft, Func<bool> RotateRight, float rotateSpeed, Func<GameObject,bool> breakCondition)
    {
        while (true)
        {
            //If the left rotation key is pressed and not the right rotation key: rotate left
            if (RotateLeft() && !RotateRight())
            {
                instance.transform.RotateAround(instance.transform.position,instance.transform.up,-rotateSpeed);
            }
            //If the right rotation key is pressed and not the left rotation key: rotate right
            if (RotateRight() && !RotateLeft())
            {
                instance.transform.RotateAround(instance.transform.position, instance.transform.up, rotateSpeed);
            }

            yield return new WaitForEndOfFrame();

            //If the stop condition is met, then stop coroutine
            if (breakCondition(instance))
            {
                break;
            }
        }
        Destroy(instance);
    }
}

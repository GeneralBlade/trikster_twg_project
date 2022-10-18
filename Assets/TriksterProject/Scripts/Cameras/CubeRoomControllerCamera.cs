using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRoomControllerCamera : MonoBehaviour
{

    public GameObject camera;
    public float deltaRotateCamera = 0.5f;
    public float[] _finishRotatePosesCamera = { 0f, 90f, 180f, -90f};
    public int _startRotateIndexCamera = 0;

    public GameObject[] _finishRotateGameObjectsCamera;
    public bool isGoToGameObjects = false;

    /* protected bool isRotateCamera = false;
    protected int _finishRotateIndexCamera; */
    public bool isRotateCamera = false;
    public int _finishRotateIndexCamera;
    // protected string rotateDirection = "right";
    public string rotateDirection = "right";


    protected float _countStep = 0f;
    public float _needRotate = 0f;
    public float _maxNeedRotate = 90f;
    protected Vector3 _endFinishRotateEulerAnglesCamera = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        if (!isGoToGameObjects)
        {
            camera.transform.Rotate(
                camera.transform.rotation.x,
                _finishRotatePosesCamera[_startRotateIndexCamera],
                camera.transform.rotation.z
            );
            _finishRotateIndexCamera = _startRotateIndexCamera;
        }
        else
        {
            GameObject target = _finishRotateGameObjectsCamera[_startRotateIndexCamera];
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, target.transform.rotation, 1f);
            _finishRotateIndexCamera = _startRotateIndexCamera;
        }
    }


        // Update is called once per frame
    void Update()
    {
        if (isRotateCamera)
        {
            if (!isGoToGameObjects)
            {
                isRotateCamera = _RotateCamera(rotateDirection, camera);
            }
            else
            {
                isRotateCamera = _QuertRotateCamera(rotateDirection, camera);
            }
                
        }
    }

    public bool RotateCamera(string direction)
    {
        switch (isGoToGameObjects)
        {
            case false:
                return _RotateCamera(direction);

            case true:
                return _QuertRotateCamera(direction);
        }
        
    }

    public bool RightRotateCamera()
    {
        return RotateCamera("right");
    }

    public bool LeftRotateCamera()
    {
        return RotateCamera("left");
    }


    protected bool _RotateCamera(string direction)
    {
        return _RotateCamera(direction, camera);
    }

    protected bool _RotateCamera(string direction, GameObject camera)
    {

        if (!isRotateCamera)
        {
            // _finishRotatePosesCamera
            // _finishRotateIndexCamera


            switch (direction)
            {
                case "right":
                    _finishRotateIndexCamera += 1;
                    break;

                case "left":
                    _finishRotateIndexCamera -= 1;
                    break;

                   
            }

            if (_finishRotateIndexCamera < 0)
            {
                _finishRotateIndexCamera = _finishRotatePosesCamera.Length - 1;
            }
            if (_finishRotateIndexCamera >= _finishRotatePosesCamera.Length)
            {
                _finishRotateIndexCamera = 0;
            }


            /* GameObject newGameObject = new GameObject();

            newGameObject.transform.LookAt(_finishRotateGameObjectsCamera[_finishRotateIndexCamera].transform, Vector3.up);

            switch (direction)
            {
                case "right":
                    _maxNeedRotate = Mathf.Abs(newGameObject.transform.eulerAngles.y - camera.transform.eulerAngles.y);
                    break;

                case "left":
                    _maxNeedRotate = Mathf.Abs(camera.transform.eulerAngles.y - newGameObject.transform.eulerAngles.y);
                    break;

            } */
            

            /* switch (direction)
            {
                case "right":
                    _finishRotateIndexCamera = 1;
                    break;

                case "left":
                    _finishRotateIndexCamera = 3;
                    break; 
            } */

            _needRotate = _maxNeedRotate;

            rotateDirection = direction;


            float endFinishRotatePosCamera = 0f;

            switch (direction)
            {
                case "right":
                    endFinishRotatePosCamera = camera.transform.eulerAngles.y + _needRotate;
                    break;

                case "left":
                    endFinishRotatePosCamera = camera.transform.eulerAngles.y - _needRotate;
                    break;
            }

            _endFinishRotateEulerAnglesCamera = new Vector3(
                camera.transform.eulerAngles.x,
                endFinishRotatePosCamera,
                camera.transform.eulerAngles.z
            );


            isRotateCamera = true;
        }


        // float finishRotatePosCamera = _finishRotatePosesCamera[_finishRotateIndexCamera];
        float finishRotatePosCamera = _needRotate;

        switch (direction)
        {
            case "right":
                break;

            case "left":
                finishRotatePosCamera *= -1;
                break;
        }
        

        float deltaTimeRotateCamera = deltaRotateCamera * Time.deltaTime;


        float farRotateCamera = camera.transform.rotation.y * camera.transform.rotation.y - finishRotatePosCamera * finishRotatePosCamera;
        // farRotateCamera *= farRotateCamera;
        
        float dubleDeltaTimeRotateCamera = deltaRotateCamera * 2f;
        dubleDeltaTimeRotateCamera *= dubleDeltaTimeRotateCamera;

        /* float farRotateCamera = Mathf.Abs(Mathf.Abs(camera.transform.rotation.y) - Mathf.Abs(finishRotatePosCamera));
        float dubleDeltaTimeRotateCamera = deltaRotateCamera * 2f; */

        // if (farRotateCamera <= dubleDeltaTimeRotateCamera || farRotateCamera >= -1f * dubleDeltaTimeRotateCamera)
        // if (farRotateCamera <= dubleDeltaTimeRotateCamera)
        if (_needRotate <= 0f)
        {
            /* camera.transform.Rotate(
                    camera.transform.rotation.x,
                    _finishRotatePosesCamera[_finishRotateIndexCamera],
                    camera.transform.rotation.z,
                    Space.World
                   ); */

            /* Vector3 currentEulerAngles = new Vector3(
                camera.transform.localEulerAngles.x,
                camera.transform.localEulerAngles.y - finishRotatePosCamera,
                camera.transform.localEulerAngles.z
            ); */

            camera.transform.eulerAngles = _endFinishRotateEulerAnglesCamera;

            isRotateCamera = false;
            

            /* if (_countStep >= 90f / deltaTimeRotateCamera) // 90f / deltaRotateCamera)
            {
                _countStep = 0f;

                camera.transform.Rotate(
                        camera.transform.rotation.x,
                        _finishRotatePosesCamera[_finishRotateIndexCamera],
                        camera.transform.rotation.z,
                        Space.World
                    );

                isRotateCamera = false;
            }

            _countStep += Time.deltaTime; */

            /* switch (direction)
            {
                case "right":
                    _DeltaRotateCamera(deltaTimeRotateCamera, camera);
                    break;

                case "left":
                    deltaTimeRotateCamera *= -1;
                    _DeltaRotateCamera(deltaTimeRotateCamera, camera);
                    break;
            } */

            
        }
        else 
        {
            switch (direction)
            {
                case "right":
                    _needRotate -= Mathf.Abs(deltaTimeRotateCamera);
                    _DeltaRotateCamera(deltaTimeRotateCamera, camera);
                    break;

                case "left":
                    _needRotate -= Mathf.Abs(deltaTimeRotateCamera);
                    deltaTimeRotateCamera *= -1;
                    _DeltaRotateCamera(deltaTimeRotateCamera, camera);
                    break;
            }
        }

        return isRotateCamera;
    }

    protected void _DeltaRotateCamera(float deltaRotate, GameObject camera)
    {


        /* camera.transform.Rotate(
                camera.transform.rotation.x,
                camera.transform.rotation.y + deltaRotate,
                camera.transform.rotation.z,
                Space.Self
            ); */

        Vector3 currentEulerAngles = new Vector3(
                camera.transform.localEulerAngles.x,
                camera.transform.localEulerAngles.y + deltaRotate,
                camera.transform.localEulerAngles.z
            );
        camera.transform.localEulerAngles = currentEulerAngles; 


        /* Transform from = camera.transform;
        Transform to =  

        timeCount = _timeCount;
        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, timeCount);
        timeCount = timeCount + Time.deltaTime; */

        /* Quaternion rotation = Quaternion.Euler(
                camera.transform.rotation.x,
                camera.transform.rotation.y + deltaRotate,
                camera.transform.rotation.z
            );

        camera.transform.rotation = rotation; */
    }


    protected bool _QuertRotateCamera(string direction)
    {
        return _QuertRotateCamera(direction, camera);
    }

    protected bool _QuertRotateCamera(string direction, GameObject camera)
    {
        if (!isRotateCamera)
        {
            // _finishRotatePosesCamera
            // _finishRotateIndexCamera

            {
                switch (direction)
                {
                    case "right":
                        _finishRotateIndexCamera += 1;
                        break;

                    case "left":
                        _finishRotateIndexCamera -= 1;
                        break;

                        /* case default:
                            break;*/
                }

                if (_finishRotateIndexCamera < 0)
                {
                    _finishRotateIndexCamera = _finishRotateGameObjectsCamera.Length - 1;
                }
                if (_finishRotateIndexCamera >= _finishRotateGameObjectsCamera.Length)
                {
                    _finishRotateIndexCamera = 0;
                }
            }

            rotateDirection = direction;

            isRotateCamera = true;
        }

        float finishRotatePosCamera = _finishRotateGameObjectsCamera[_finishRotateIndexCamera].transform.rotation.y;

        float deltaTimeRotateCamera = deltaRotateCamera * Time.deltaTime;


        float farRotateCamera = camera.transform.rotation.y * camera.transform.rotation.y - finishRotatePosCamera * finishRotatePosCamera;
        // farRotateCamera *= farRotateCamera;

        float dubleDeltaTimeRotateCamera = deltaTimeRotateCamera * 2f;
        dubleDeltaTimeRotateCamera *= dubleDeltaTimeRotateCamera;


        // if (farRotateCamera <= dubleDeltaTimeRotateCamera || farRotateCamera >= -1f * dubleDeltaTimeRotateCamera)
        if (farRotateCamera <= dubleDeltaTimeRotateCamera)
        {
            /*camera.transform.Rotate(
                camera.transform.rotation.x,
                _finishRotatePosesCamera[_finishRotateIndexCamera],
                camera.transform.rotation.z
            ); */

            /* camera.transform.rotation = Quaternion.Slerp(
                    camera.transform.rotation, 
                    _finishRotateGameObjectsCamera[_finishRotateIndexCamera].transform.rotation, 
                    1f
                ); */

            isRotateCamera = false;
        }
        else
        {
            /* switch (direction)
            {
                case "right":
                    _DeltaRotateCamera(deltaTimeRotateCamera, camera);
                    break;

                case "left":
                    deltaTimeRotateCamera *= -1;
                    _DeltaRotateCamera(deltaTimeRotateCamera, camera);
                    break;
            } */

            _DeltaQuaterRotateCamera(deltaTimeRotateCamera, camera, _finishRotateGameObjectsCamera[_finishRotateIndexCamera]);
        }

        return isRotateCamera;
    }

    protected void _DeltaQuaterRotateCamera(float deltaRotate, GameObject camera, GameObject toGameObject)
    { 
        camera.transform.rotation = Quaternion.RotateTowards(camera.transform.rotation, toGameObject.transform.rotation, deltaRotate); 
    }
}

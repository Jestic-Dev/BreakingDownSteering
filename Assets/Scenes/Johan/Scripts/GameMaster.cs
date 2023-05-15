using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMaster : MonoBehaviour
{
    public ObstacleManager obstacleManager;

    enum GameStates
    {
        TestSetup,
        Testing,
        TestEnd
    }

    enum ControllerChoice
    {
        LeftController,
        RightController,
    }

    [SerializeField]
    private GameStates gameState;

    [SerializeField]
    private ControllerChoice controllerChoice;

    [System.Serializable]
    private class GameControllers
    {
        public Transform left;
        public Transform right;

        public Material activeMat;
        public Material inactiveMat;
    }
    [SerializeField]
    GameControllers gameControllers = new GameControllers();

    [System.Serializable]
    private class ControllerInputs
    {
        public InputActionProperty leftHandPrimary;
        public InputActionProperty rightHandPrimary;

        public InputActionProperty leftHandRotation;
        public InputActionProperty rightHandRotation;

        public InputActionProperty leftHandGrip;
        public InputActionProperty leftHandTrigger;

        public InputActionProperty rightHandGrip;
        public InputActionProperty rightHandTrigger;
    }
    [SerializeField]
    ControllerInputs controllerInputs = new ControllerInputs();

    [SerializeField]
    TestCondition testCondition = null;


    public float movementBounds;
    public float deadzoneAngle;

    public static int speedModifier;

    private void Start()
    {
        speedModifier = 1;
        gameState = GameStates.TestSetup;
        controllerChoice = ControllerChoice.RightController;
    }

    float handRotation = 0;

    private void Update()
    {
        switch (gameState)
        {
            case GameStates.TestSetup:

                if (controllerInputs.leftHandPrimary.action.ReadValue<float>() > 0.9f)
                { 
                    controllerChoice = ControllerChoice.LeftController;

                    MeshRenderer leftMesh = gameControllers.left.Find("Controller_Base").GetComponent<MeshRenderer>();
                    leftMesh.material = gameControllers.activeMat;

                    MeshRenderer rightMesh = gameControllers.right.Find("Controller_Base").GetComponent<MeshRenderer>();
                    rightMesh.material = gameControllers.inactiveMat;
                }
                if (controllerInputs.rightHandPrimary.action.ReadValue<float>() > 0.9f)
                { 
                    controllerChoice = ControllerChoice.RightController;

                    MeshRenderer leftMesh = gameControllers.left.Find("Controller_Base").GetComponent<MeshRenderer>();
                    leftMesh.material = gameControllers.inactiveMat;

                    MeshRenderer rightMesh = gameControllers.right.Find("Controller_Base").GetComponent<MeshRenderer>();
                    rightMesh.material = gameControllers.activeMat;
                }

                if ((controllerChoice == ControllerChoice.LeftController &&
                    controllerInputs.leftHandGrip.action.ReadValue<float>() > 0.9 &&
                    controllerInputs.leftHandTrigger.action.ReadValue<float>() > 0.9)
                    ||
                    (controllerChoice == ControllerChoice.RightController &&
                    controllerInputs.rightHandGrip.action.ReadValue<float>() > 0.9 &&
                    controllerInputs.rightHandTrigger.action.ReadValue<float>() > 0.9))
                {
                    obstacleManager.StartSpawnSequence();
                    gameState = GameStates.Testing;
                }

                UpdateHandRotation();
                testCondition.UpdateCondition(handRotation, movementBounds, deadzoneAngle);
                break;

            case GameStates.Testing:
                UpdateHandRotation();
                testCondition.UpdateCondition(handRotation, movementBounds, deadzoneAngle);
                break;

            case GameStates.TestEnd: 
                break;

            default:
                break;
        }
    }

    private void UpdateHandRotation()
    {
        if (controllerChoice == ControllerChoice.LeftController)
        {
            ProcessLeftHand();
        }

        if (controllerChoice == ControllerChoice.RightController)
        {
            ProcessRightHand();
        }
    }

    private void ProcessLeftHand()
    {
        Quaternion leftQuaternion = controllerInputs.leftHandRotation.action.ReadValue<Quaternion>();
        float leftRotationZ = leftQuaternion.eulerAngles.z;

        if (leftRotationZ > 180)
        {
            leftRotationZ += -360;
        }

        handRotation = leftRotationZ;
    }

    private void ProcessRightHand()
    {
        Quaternion rightQuaternion = controllerInputs.rightHandRotation.action.ReadValue<Quaternion>();
        float rightRotationZ = rightQuaternion.eulerAngles.z;

        if (rightRotationZ > 180)
        {
            rightRotationZ += -360;
        }

        handRotation = rightRotationZ;
    }
}

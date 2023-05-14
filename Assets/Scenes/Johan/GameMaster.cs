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
        TestMovement,
        TestMapping,
        TestMovementFlipped,
        TestMappingFlipped,
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
    private class ControllerInputs
    {
        public InputActionProperty leftHandPrimary;
        public InputActionProperty rightHandPrimary;

        public InputActionProperty leftHandRotation;
        public InputActionProperty rightHandRotation;
    }
    [SerializeField]
    ControllerInputs controllerInputs = new ControllerInputs();

    [SerializeField]
    TestCondition currentCondition = null;

    [SerializeField]
    List<TestCondition> conditions = new List<TestCondition>();

    public float movementBounds;
    public float deadzoneAngle;

    public static int speedModifier;

    private void Start()
    {
        speedModifier = 1;
        gameState = GameStates.TestMovement;
        controllerChoice = ControllerChoice.RightController;
        currentCondition = conditions[0];
    }

    float handRotation = 0;

    private void Update()
    {
        if(controllerInputs.leftHandPrimary.action.ReadValue<float>() > 0.9f)
        {
            controllerChoice = ControllerChoice.LeftController;
        }

        if (controllerInputs.rightHandPrimary.action.ReadValue<float>() > 0.9f)
        {
            controllerChoice = ControllerChoice.RightController;
            obstacleManager.StartSpawnSequence();
        }

        Debug.Log(gameState);

        switch (gameState)
        {
            case GameStates.TestSetup:
                break;

            case GameStates.TestMovement:
            case GameStates.TestMovementFlipped:
            case GameStates.TestMapping:
            case GameStates.TestMappingFlipped:
                UpdateHandRotation();
                currentCondition.UpdateCondition(handRotation, movementBounds, deadzoneAngle);
                break;

            case GameStates.TestEnd: 
                break;
                default: break;
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

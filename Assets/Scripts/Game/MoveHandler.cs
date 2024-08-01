using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public enum Direction {
    Up, Down, Left, Right
}

public class MoveHandler : MonoBehaviour, IDragHandler, IEndDragHandler {

    [SerializeField] GameObject[] _cornerPointers;
    [SerializeField] Direction[] _directions;
    [SerializeField] Player _player;

    public void OnEndDrag(PointerEventData eventData) {
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        Direction dir = GetDragDirection(dragVectorDirection);

        _player.Move(dir);
    }

    private Direction GetDragDirection(Vector3 dragVector) {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        Direction draggedDir;
        if (positiveX > positiveY) {
            draggedDir = (dragVector.x > 0) ? Direction.Right : Direction.Left;
        } else {
            draggedDir = (dragVector.y > 0) ? Direction.Up : Direction.Down;
        }
        return draggedDir;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            _player.Move(Direction.Up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            _player.Move(Direction.Down);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            _player.Move(Direction.Left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            _player.Move(Direction.Right);
        }

    }

    public void OnDrag(PointerEventData eventData) {}
}

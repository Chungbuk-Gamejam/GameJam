﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionUtils
{
    private static PlayerController _playerController;

    // PlayerController 인스턴스를 설정하는 메서드
    public static void Initialize(PlayerController playerController)
    {
        _playerController = playerController;
    }
    // 플레이어의 현재 방향을 체크해주는 함수
    public static bool CheckYAxisDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
                return _playerController.movement.y == 1;
            case Direction.DOWN:
                return _playerController.movement.y == -1;
            default:
                return false;
        }
    }

    public static bool CheckXAxisDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.RIGHT:
                return _playerController.movement.x == 1;
            case Direction.LEFT:
                return _playerController.movement.x == -1;
            default:
                return false;
        }
    }
}

﻿using OES.RobotWars.Enums;
using OES.RobotWars.Models;

namespace OES.RobotWars.Interfaces
{
  public interface IRobot
  {
    Guid Id { get; }
    Coordinate Position { get; set; }
    Orientation Orientation { get; set; }

    void Do(IEnumerable<RobotInstruction> instructions);
    void RotateLeft();
    void RotateRight();
    void Move();
  }
}

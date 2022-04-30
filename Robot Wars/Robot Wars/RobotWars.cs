using Microsoft.Extensions.DependencyInjection;
using OES.RobotWars.Interfaces;
using OES.RobotWars.Models;
using OES.RobotWars.Services;

namespace OES.RobotWars
{
  public static class RobotWars
  {
    public static IRobotWarsService? CreateService()
    {
      var services = new ServiceCollection()
        .AddSingleton<IArena, Arena>()
        .AddSingleton<IRobotMoveValidationService, RobotMoveValidationService>()
        .AddSingleton<ITextInputParserService, TextInputParserService>()
        .AddSingleton<IRobotWarsService, RobotWarsService>();

      var serviceProvider = services.BuildServiceProvider();
      return serviceProvider.GetService<IRobotWarsService>();
    }

  }
}

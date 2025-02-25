using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agario.Project.Game.Units
{
    public static class Units
    {
        public static byte[] foodImage = new byte[0];
        public static byte[] enemyImage = new byte[0];
        static Units()
        {
            string foodPath = Path.Combine("Assets", "foodImage.png");
            string enemyPath = Path.Combine("Assets", "enemyImage.png");
            if (File.Exists(foodPath))
            {
                foodImage = File.ReadAllBytes(foodPath);
            }
            else
            {
                Console.WriteLine($"Файл {foodPath} не найден.");
                foodImage = new byte[1];
            }
            if (File.Exists(enemyPath))
                enemyImage = File.ReadAllBytes(enemyPath);
            else
            {
                Console.WriteLine($"Файл {enemyPath} не найден.");
                enemyImage = new byte[1];
            }
        }
    }
}

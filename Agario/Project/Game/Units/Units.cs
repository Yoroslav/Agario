using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agario.Project.Game.Units
{
    public static class Units
    {
        public static byte[] foodImage;
        public static byte[] enemyImage;
        static Units()
        {
            string foodPath = Path.Combine("Assets", "foodimage.png");
            string enemyPath = Path.Combine("Assets", "enemyimage.png");
            if (File.Exists(foodPath))
                foodImage = File.ReadAllBytes(foodPath);
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

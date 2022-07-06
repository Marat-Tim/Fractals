using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    /// <summary>
    /// Класс для работы с углами.
    /// </summary>
    static class Angles 
    {
        /// <summary>
        /// Переводит угол из градусов в радианы.
        /// </summary>
        /// <param name="angle">Угол в градусах.</param>
        /// <returns>Угол в радианах.</returns>
        static public double FromDegToRad(double angle)
        {
            return angle * Math.PI / 180;
        }
    }
}

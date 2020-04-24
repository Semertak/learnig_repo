using System.Linq;

namespace Engine
{
    public static class Extensions
    {
        /// <summary>
        ///     Отцентрировать текст в ячейки заданной ширины.
        /// </summary>
        /// <param name="s">Текст.</param>
        /// <param name="desiredLength">Ширина ячейки.</param>
        /// <returns>Текст.</returns>
        public static string Center(this string s, int desiredLength)
        {
            if (s.Length >= desiredLength) return s;
            var firstpad = (s.Length + desiredLength) / 2;
            return s.PadLeft(firstpad).PadRight(desiredLength);
        }

        /// <summary>
        ///     Преобразовать цвет в строку и взять первые 2 буквы.
        /// </summary>
        /// <param name="c">Цвет.</param>
        /// <returns>Обрезаный строковой эквивалент цвета.</returns>
        public static string ShortFormat(this Color c)
        {
            return c.ToString().Take(2).Aggregate("", (acc, x) => acc + x);
        }
    }
}
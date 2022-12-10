namespace Core.Extensions
{
    public static class CustomLinqExtentions
    {
        /// <summary>
        /// Returns a string enum value
        /// </summary>
        /// <param name="enumValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetEnumStringValue<T>(this T enumValue) where T : Enum
        {
            return Enum.GetName(typeof(T), enumValue);
        }
        /// <summary>
        /// Returns true if string is null or empty or white space
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrWhiteSpace(this string value) => string.IsNullOrEmpty(value?.Trim());
    }
}
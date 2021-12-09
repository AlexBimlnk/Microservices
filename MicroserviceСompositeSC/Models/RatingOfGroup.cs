using System.Text.Json.Serialization;

namespace MicroserviceСompositeSC.Models
{
    /// <summary>
    /// Модель данных для рейтинга группы.
    /// </summary>
    public class RatingOfGroup
    {
        /// <summary>
        /// Возвращает или задаёт имя группы.
        /// </summary>
        [JsonPropertyName("groupName")]
        public string GroupName { get; set; }

        /// <summary>
        /// Возвращает или задаёт средний рейтинг группы.
        /// </summary>
        [JsonPropertyName("averageRating")]
        public double AverageRating { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Povorot.DAL.Models
{
    public class BaseRecord
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDateTime { get; set; }
        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime ModifiedDateTime { get; set; }
        /// <summary>
        /// кто создал
        /// </summary>
        public long CreatedUserId { get; set; }
        public User CreatedUser { get; set; }
        /// <summary>
        /// кто изменил
        /// </summary>
        public long? ModifiedUserId { get; set; }
        public User ModifiedUser { get; set; }
    }
}
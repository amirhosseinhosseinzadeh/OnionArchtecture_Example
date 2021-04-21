using System;

namespace Onion.Domain.Bases
{
    public class BaseEntity
    {
        /// <summary>
        /// Do not fill manually the value is auto generate
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Do not fill manually the value is auto generate
        /// </summary>
        public DateTime LastModifiedOnUtc { get; set; }
        /// <summary>
        /// Do not fill manually the value is auto generate
        /// </summary>
        public string LastModifierId { get; set; }
        /// <summary>
        /// Do not fill manually the value is auto generate
        /// </summary>
        public bool IsUpdated { get; set; }
    }   
}
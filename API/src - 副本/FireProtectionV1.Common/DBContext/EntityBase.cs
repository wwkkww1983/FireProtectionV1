using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace FireProtectionV1.Common.DBContext
{
    /// <summary>
    /// 所有实体的父类
    /// </summary>
    public class EntityBase : Entity, IHasCreationTime, ISoftDelete
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 软删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}

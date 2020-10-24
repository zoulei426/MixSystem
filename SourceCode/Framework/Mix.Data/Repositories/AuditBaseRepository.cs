using FreeSql;
using Mix.Core;
using Mix.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mix.Data.Repositories
{
    /// <summary>
    /// 审计仓储
    /// </summary>
    public class AuditBaseRepository<TEntity> : AuditBaseRepository<TEntity, Guid>, IAuditBaseRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="currentUser"></param>
        public AuditBaseRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }
    }

    /// <summary>
    /// 审计仓储：实现如果实体类
    /// 继承了ICreateAduitEntity  则自动增加创建时间/人信息
    /// 继承了IUpdateAuditEntity，更新时，修改更新时间/人
    /// 继承了ISoftDeleteAduitEntity，删除时，自动改成软删除。仅注入此仓储或继承此仓储的实现才能实现如上功能。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class AuditBaseRepository<TEntity, TKey> : DefaultRepository<TEntity, TKey>, IAuditBaseRepository<TEntity, TKey>
        where TEntity : class, new()
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        protected readonly ICurrentUser CurrentUser;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="currentUser"></param>
        public AuditBaseRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager?.Orm, unitOfWorkManager)
        {
            CurrentUser = currentUser;
        }

        /// <summary>
        /// 写入创建/修改审计
        /// </summary>
        /// <param name="entity"></param>
        private void BeforeInsert(TEntity entity)
        {
            // 创建Id
            if (entity is IEntity en) en.Id = Guid.NewGuid();

            // 创建Creater信息
            if (entity is not ICreateAduitEntity createAduitEntity) return;
            createAduitEntity.CreateTime = DateTime.Now;
            if (createAduitEntity.CreaterId.Equals(Guid.Empty) && CurrentUser.ID != null)
            {
                createAduitEntity.CreaterId = CurrentUser.ID ?? Guid.Empty;
            }

            // 创建updater信息
            if (entity is not IUpdateAuditEntity updateAuditEntity) return;
            updateAuditEntity.ModityTime = DateTime.Now;
            updateAuditEntity.ModifierId = CurrentUser.ID;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override TEntity Insert(TEntity entity)
        {
            BeforeInsert(entity);
            return base.Insert(entity);
        }

        /// <summary>
        /// 异步插入一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            BeforeInsert(entity);
            return base.InsertAsync(entity);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public override List<TEntity> Insert(IEnumerable<TEntity> entitys)
        {
            foreach (TEntity entity in entitys)
            {
                BeforeInsert(entity);
            }

            return base.Insert(entitys);
        }

        /// <summary>
        /// 异步批量插入数据
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public override Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entitys)
        {
            foreach (TEntity entity in entitys)
            {
                BeforeInsert(entity);
            }
            return base.InsertAsync(entitys);
        }

        /// <summary>
        /// 写入修改审计
        /// </summary>
        /// <param name="entity"></param>
        private void BeforeUpdate(TEntity entity)
        {
            if (entity is not IUpdateAuditEntity e) return;
            e.ModityTime = DateTime.Now;
            e.ModifierId = CurrentUser.ID;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public new int Update(TEntity entity)
        {
            BeforeUpdate(entity);
            return base.Update(entity);
        }

        /// <summary>
        /// 异步更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public new Task<int> UpdateAsync(TEntity entity)
        {
            BeforeUpdate(entity);
            return base.UpdateAsync(entity);
        }

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public override int Update(IEnumerable<TEntity> entitys)
        {
            foreach (var entity in entitys)
            {
                this.BeforeUpdate(entity);
            }
            return base.Update(entitys);
        }

        /// <summary>
        /// 异步批量更新数据
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public override Task<int> UpdateAsync(IEnumerable<TEntity> entitys)
        {
            foreach (var entity in entitys)
            {
                BeforeUpdate(entity);
            }
            return base.UpdateAsync(entitys);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override int Delete(TEntity entity)
        {
            if (entity is IDeleteAduitEntity)
            {
                return Orm.Update<TEntity>(entity)
                           .Set(a => (a as IDeleteAduitEntity).IsDeleted, true)
                           .Set(a => (a as IDeleteAduitEntity).DeleterId, CurrentUser.ID)
                           .Set(a => (a as IDeleteAduitEntity).DeleteTime, DateTime.Now)
                           .ExecuteAffrows();
            }

            return base.Delete(entity);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public override int Delete(IEnumerable<TEntity> entitys)
        {
            if (entitys.Any())
            {
                Attach(entitys);
                foreach (TEntity x1 in entitys)
                {
                    if (x1 is IDeleteAduitEntity softDelete)
                    {
                        softDelete.DeleterId = CurrentUser.ID;
                        softDelete.DeleteTime = DateTime.Now;
                        softDelete.IsDeleted = true;
                    }
                }

                return Update(entitys);
            }

            return base.Delete(entitys);
        }

        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<int> DeleteAsync(TKey id)
        {
            TEntity entity = await base.GetAsync(id);
            if (entity is IDeleteAduitEntity)
            {
                return Orm.Update<TEntity>(entity)
                           .Set(a => (a as IDeleteAduitEntity).IsDeleted, true)
                           .Set(a => (a as IDeleteAduitEntity).DeleterId, CurrentUser.ID)
                           .Set(a => (a as IDeleteAduitEntity).DeleteTime, DateTime.Now)
                           .ExecuteAffrows();
            }

            return await base.DeleteAsync(id);
        }

        /// <summary>
        /// 异步批量删除数据
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public override Task<int> DeleteAsync(IEnumerable<TEntity> entitys)
        {
            if (entitys.Any())
            {
                Attach(entitys);
                foreach (TEntity x1 in entitys)
                {
                    if (x1 is IDeleteAduitEntity softDelete)
                    {
                        softDelete.DeleterId = CurrentUser.ID;
                        softDelete.DeleteTime = DateTime.Now;
                        softDelete.IsDeleted = true;
                    }
                }
                return UpdateAsync(entitys);
            }
            return base.DeleteAsync(entitys);
        }

        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override async Task<int> DeleteAsync(TEntity entity)
        {
            if (entity is IDeleteAduitEntity)
            {
                return await Orm.Update<TEntity>(entity)
                    .Set(a => (a as IDeleteAduitEntity).IsDeleted, true)
                    .Set(a => (a as IDeleteAduitEntity).DeleterId, CurrentUser.ID)
                    .Set(a => (a as IDeleteAduitEntity).DeleteTime, DateTime.Now)
                    .ExecuteAffrowsAsync();
            }

            return base.Delete(entity);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (typeof(IDeleteAduitEntity).IsAssignableFrom(typeof(TEntity)))
            {
                List<TEntity> items = Orm.Select<TEntity>().Where(predicate).ToList();
                if (items.Count == 0)
                {
                    return 0;
                }
                return Orm.Update<TEntity>(items)
                    .Set(a => (a as IDeleteAduitEntity).IsDeleted, true)
                    .Set(a => (a as IDeleteAduitEntity).DeleterId, CurrentUser.ID)
                    .Set(a => (a as IDeleteAduitEntity).DeleteTime, DateTime.Now)
                    .ExecuteAffrows();
            }

            return base.Delete(predicate);
        }

        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (typeof(IDeleteAduitEntity).IsAssignableFrom(typeof(TEntity)))
            {
                List<TEntity> items = Orm.Select<TEntity>().Where(predicate).ToList();
                if (items.Count == 0)
                {
                    return 0;
                }
                return await Orm.Update<TEntity>(items)
                     .Set(a => (a as IDeleteAduitEntity).IsDeleted, true)
                     .Set(a => (a as IDeleteAduitEntity).DeleterId, CurrentUser.ID)
                     .Set(a => (a as IDeleteAduitEntity).DeleteTime, DateTime.Now)
                     .ExecuteAffrowsAsync();
            }

            return await base.DeleteAsync(predicate);
        }

        /// <summary>
        /// 插入或更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            BeforeInsert(entity);
            BeforeUpdate(entity);
            await base.InsertOrUpdateAsync(entity);
            return entity;
        }
    }
}
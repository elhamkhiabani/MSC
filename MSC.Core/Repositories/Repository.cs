using AutoMapper;
using MSC.Core.CRUD;
using MSC.Core.Presentations.Base;
using MSC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Repositories
{
    public class Repository<T, V> : IRepository<T, V> where T : class, IEntity
    {
        private readonly ICRUD<T> _crud;
        private readonly IMapper _map;

        public Repository(ICRUD<T> crud, IMapper map)
        {
            _crud = crud;
            _map = map;
        }

        public MessageViewModel Add(T entity, int creatorId = 0)
        {
            MessageViewModel result = new MessageViewModel();
            try
            {
                entity.CreatorID = creatorId;
                entity.CreationDateTime = DateTime.Now;
                entity.ModifierID = creatorId;
                entity.ModifierDateTime = DateTime.Now;
                _crud.Add(entity);
                _crud.Save();
                result = new MessageViewModel
                {
                    ID = entity.ID,
                    Message = "ADD Success",
                    Status = "Success",
                    Value = ""
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new MessageViewModel
                {
                    ID = -1000,
                    Message = ex.Message,
                    Status = "Error",
                    Value = ""
                };
                return result;
            }
        }


        public MessageViewModel Delete(int id, int creatorId=0, bool hardDelete = false)
        {
            MessageViewModel result = new MessageViewModel();
            try
            {
                var exist = _crud.GetByID(id);

                if (exist == null)
                {
                    result = new MessageViewModel
                    {
                        ID = -1000,
                        Message = "Not Found",
                        Status = "Error",
                        Value = ""
                    };
                    return result;
                }

                exist.ModifierDateTime = DateTime.Now;
                exist.ModifierID = creatorId;
                if (hardDelete)
                {
                    _crud.Remove(exist);
                    _crud.Save();
                    result = new MessageViewModel
                    {
                        ID = exist.ID,
                        Message = "Hard Delete Success",
                        Status = "Success",
                        Value = ""
                    };
                    return result;
                }

                _crud.Remove(exist);
                _crud.Save();
                result = new MessageViewModel
                {
                    ID = exist.ID,
                    Message = "Soft Delete Success",
                    Status = "Success",
                    Value = ""
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new MessageViewModel
                {
                    ID = -1000,
                    Message = ex.Message,
                    Status = "Error",
                    Value = ""
                };
                return result;
            }
        }


        public ResultViewModel<V> GetByID(int id)
        {
            ResultViewModel<V> result = new ResultViewModel<V>();
            try
            {
                var exist = _crud.GetByID(id);
                if (exist == null)
                {
                    result.Message = new MessageViewModel
                    {
                        ID = -1000,
                        Message = "NOT Found",
                        Status = "Warning",
                        Value = ""
                    };
                    return result;
                }

                result.Result = _map.Map<V>(exist);
                result.Message = new MessageViewModel
                {
                    ID = exist.ID,
                    Message = "GetById Success",
                    Status = "Success",
                    Value = ""
                };
                return result;
            }
            catch (Exception ex)
            {
                result.Message = new MessageViewModel
                {
                    ID = -1000,
                    Message = ex.Message,
                    Status = "Error",
                    Value = ""
                };
                return result;
            }
        }


        public MessageViewModel Update(T entity)
        {
            MessageViewModel result = new MessageViewModel();
            try
            {
                var exist = _crud.GetByID(entity.ID);
                if (exist==null)
                {
                    result = new MessageViewModel
                    {
                        ID = -1000,
                        Message = "NOT Found",
                        Status = "Warning",
                        Value = ""
                    };
                    return result;
                }

                _crud.Update(entity);
                _crud.Save();
                result = new MessageViewModel
                {
                    ID = exist.ID,
                    Message = "Update Success",
                    Status = "Success",
                    Value = ""
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new MessageViewModel
                {
                    ID = -1000,
                    Message = ex.Message,
                    Status = "Error",
                    Value = ""
                };
                return result;
            }
        }

        public ResultViewModel<V> GetAll(bool isActive,Expression<Func<T, bool>>? predicate)
        {
            ResultViewModel<V> result = new ResultViewModel<V>();

            try
            {

                var list = _crud.GetAll(isActive).Where(predicate);
                result.List = _map.Map<List<V>>(list);
                result.Message = new MessageViewModel
                {
                    ID = 0,
                    Message = "GetAll Success",
                    Status = "Success",
                    Value = ""
                };
                return result;
            }
            catch (Exception ex)
            {
                result.Message = new MessageViewModel
                {
                    ID = -1000,
                    Message = ex.Message,
                    Status = "Error",
                    Value = ""
                };
                return result;
            }
        }

    }
}

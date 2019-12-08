﻿using DataAccessLayer;
using Entity;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class CommendService : Service, ISave<Commend>, ISearch<Commend>, IUpdate, IGetAllData<Commend>
    {
        private readonly CommendRepository _commendRepository;

        public CommendService()
        {
            _commendRepository = new CommendRepository(dbConnection);
        }

        public bool Save(Commend data)
        {
            try
            {
                dbConnection.Open();
                return _commendRepository.Save(data);
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public Commend Search(string primaryKey)
        {
            try
            {
                dbConnection.Open();
                return _commendRepository.Search(primaryKey);
            }
            catch (System.Exception)
            {
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public bool Update(string primarykey, string columToModify, object newValue)
        {
            try
            {
                dbConnection.Open();
                return _commendRepository.Update(primarykey, columToModify, newValue);
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public IList<Commend> GetAllData()
        {
            try
            {
                dbConnection.Open();
                return _commendRepository.GetAllData();
            }
            catch (System.Exception)
            {
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

    }
}

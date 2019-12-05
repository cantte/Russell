﻿using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class RouteRepository : Repository, ISave<Route>, ISearch<Route>, IUpdate, IDelete, IMap<Route>
    {
        static readonly string[] ROUTE_FIELDS = { "@route_code", "@origin_city", "@destination_city", "@cost" };

        public RouteRepository(DbConnection connection) : base(connection)
        {

        }

        public bool Save(Route data)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "INSER INTO routes(route_code, origin_city, destination_city, cost)" +
                                      "VALUES(@route_code, @origin_city, @destination_city, @cost)";

                MapCommandParameters(command, ROUTE_FIELDS,
                    new object[]
                    {
                        data.Code,
                        data.OriginCity,
                        data.DestinationCity,
                        data.Cost
                    });

                command.ExecuteNonQuery();
                return true;
            }
        }

        public Route Search(string primaryKey)
        {
            using (var command = dbConnection.CreateCommand())
            {
                return null;
            }
        }

        public Route Map(DbDataReader dbDataReader)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(string primarykey, string columToModify, object newValue)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = $"UPDATE routes SET {columToModify} = @newValue WHERE route_code = @primaryKey";

                command.Parameters.Add(CreateDbParameter(command, "@newValue", newValue));
                command.Parameters.Add(CreateDbParameter(command, "@primaryKey", primarykey));

                return command.ExecuteNonQuery() > 0; 
            }
        }

        public bool Delete(string primaryKey)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "DELETE routes WHERE route_code = @primaryKey";

                command.Parameters.Add(CreateDbParameter(command, "@primaryKey", primaryKey));

                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
